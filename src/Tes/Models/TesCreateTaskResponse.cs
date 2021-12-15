﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

/*
 * Task Execution Service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * OpenAPI spec version: 0.3.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Tes.Models
{
    /// <summary>
    /// CreateTaskResponse describes a response from the CreateTask endpoint.
    /// </summary>
    [DataContract]
    public partial class TesCreateTaskResponse : IEquatable<TesCreateTaskResponse>
    {
        /// <summary>
        /// Task identifier assigned by the server.
        /// </summary>
        /// <value>Task identifier assigned by the server.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
            => new StringBuilder()
                .Append("class TesCreateTaskResponse {\n")
                .Append("  Id: ").Append(Id).Append('\n')
                .Append("}\n")
                .ToString();

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
            => JsonConvert.SerializeObject(this, Formatting.Indented);

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
            => obj switch
            {
                var x when x is null => false,
                var x when ReferenceEquals(this, x) => true,
                _ => obj.GetType() == GetType() && Equals((TesCreateTaskResponse)obj),
            };

        /// <summary>
        /// Returns true if TesCreateTaskResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of TesCreateTaskResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TesCreateTaskResponse other)
            => other switch
            {
                var x when x is null => false,
                var x when ReferenceEquals(this, x) => true,
                _ =>
                    (
                        Id == other.Id ||
                        Id != null &&
                        Id.Equals(other.Id)
                    ),
            };

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                if (Id != null)
                {
                    hashCode = hashCode * 59 + Id.GetHashCode();
                }

                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(TesCreateTaskResponse left, TesCreateTaskResponse right)
            => Equals(left, right);

        public static bool operator !=(TesCreateTaskResponse left, TesCreateTaskResponse right)
            => !Equals(left, right);

#pragma warning restore 1591
        #endregion Operators
    }
}
