﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tes.Extensions;
using Tes.Models;
using Tes.Repository;

namespace TesApi.Web
{
    /// <summary>
    /// Background service to delete active Batch jobs that have no tasks as the result of job creation exception.
    /// </summary>
    public class DeleteOrphanedBatchJobsHostedService : BackgroundService
    {
        private static readonly TimeSpan runInterval = TimeSpan.FromHours(1);
        private static readonly TimeSpan minJobAge = TimeSpan.FromHours(1);

        private readonly IRepository<TesTask> repository;
        private readonly IAzureProxy azureProxy;
        private readonly ILogger<DeleteOrphanedBatchJobsHostedService> logger;
        private readonly bool isDisabled;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration">The configuration instance settings</param>
        /// <param name="azureProxy">Azure Proxy</param>
        /// <param name="repository">The main TES task database repository</param>
        /// <param name="logger">The logger instance</param>
        public DeleteOrphanedBatchJobsHostedService(IConfiguration configuration, IAzureProxy azureProxy, IRepository<TesTask> repository, ILogger<DeleteOrphanedBatchJobsHostedService> logger)
        {
            this.repository = repository;
            this.azureProxy = azureProxy;
            this.logger = logger;
            this.isDisabled = !configuration.GetValue("BatchAutopool", false);
        }

        /// <inheritdoc />
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            if (isDisabled)
            {
                return Task.CompletedTask;
            }

            return base.StartAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Orphaned job cleanup service started");

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await DeleteOrphanedJobsAsync(cancellationToken);
                }
                catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex) when (!(ex is OperationCanceledException && cancellationToken.IsCancellationRequested))
                {
                    logger.LogError(ex, ex.Message);
                }

                try
                {
                    await Task.Delay(runInterval, cancellationToken);
                }
                catch (TaskCanceledException)
                { }
            }
        }

        private async Task DeleteOrphanedJobsAsync(CancellationToken cancellationToken)
        {
            var jobsToDelete = await azureProxy.ListOrphanedJobsToDeleteAsync(minJobAge, cancellationToken);

            foreach (var jobId in jobsToDelete)
            {
                var tesTaskId = jobId.Split(new[] { '-' })[0];

                TesTask tesTask = null;

                if (await repository.TryGetItemAsync(tesTaskId, item => tesTask = item)) // TODO: Add CancellationToken to IRepository and add unit tests
                {
                    if (tesTask.State == TesState.COMPLETEEnum ||
                        tesTask.State == TesState.EXECUTORERROREnum ||
                        tesTask.State == TesState.SYSTEMERROREnum ||
                        tesTask.State == TesState.CANCELEDEnum ||
                        tesTask.State == TesState.UNKNOWNEnum)
                    {
                        await azureProxy.DeleteBatchJobAsync(tesTaskId, cancellationToken);
                        logger.LogInformation($"Deleted orphaned Batch Job '{jobId}'");
                    }
                    else
                    {
                        logger.LogWarning($"Not deleting orphaned Batch Job '{jobId}' because the corresponding TES task '{tesTaskId}' is in '{tesTask.State}' state.");
                    }
                }
                else
                {
                    logger.LogError($"Not deleting orphaned Batch Job '{jobId}' because the corresponding TES task '{tesTaskId}' was not found. Investigate and delete the job manually.");
                }
            }
        }
    }
}
