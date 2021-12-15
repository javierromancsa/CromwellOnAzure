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
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

namespace Tes.Models
{
    /// <summary>
    /// ServiceInfo describes information about the service, such as storage details, resource availability, and other documentation.
    /// </summary>
    [DataContract]
    public partial class TesServiceInfo : IEquatable<TesServiceInfo>
    {
        /// <summary>
        /// Returns the name of the service, e.g. \&quot;ohsu-compbio-funnel\&quot;.
        /// </summary>
        /// <value>Returns the name of the service, e.g. \&quot;ohsu-compbio-funnel\&quot;.</value>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Returns a documentation string, e.g. \&quot;Hey, we&#39;re OHSU Comp. Bio!\&quot;.
        /// </summary>
        /// <value>Returns a documentation string, e.g. \&quot;Hey, we&#39;re OHSU Comp. Bio!\&quot;.</value>
        [DataMember(Name = "doc")]
        public string Doc { get; set; }

        /// <summary>
        /// Lists some, but not necessarily all, storage locations supported by the service.  Must be in a valid URL format. e.g.  file:///path/to/local/funnel-storage s3://ohsu-compbio-funnel/storage etc.
        /// </summary>
        /// <value>Lists some, but not necessarily all, storage locations supported by the service.  Must be in a valid URL format. e.g.  file:///path/to/local/funnel-storage s3://ohsu-compbio-funnel/storage etc.</value>
        [DataMember(Name = "storage")]
        public List<string> Storage { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
            => new StringBuilder()
                .Append("class TesServiceInfo {\n")
                .Append("  Name: ").Append(Name).Append('\n')
                .Append("  Doc: ").Append(Doc).Append('\n')
                .Append("  Storage: ").Append(Storage).Append('\n')
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
                _ => obj.GetType() == GetType() && Equals((TesServiceInfo)obj),
            };

        /// <summary>
        /// Returns true if TesServiceInfo instances are equal
        /// </summary>
        /// <param name="other">Instance of TesServiceInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TesServiceInfo other)
            => other switch
            {
                var x when x is null => false,
                var x when ReferenceEquals(this, x) => true,
                _ =>
                (
                    Name == other.Name ||
                    Name != null &&
                    Name.Equals(other.Name)
                ) &&
                (
                    Doc == other.Doc ||
                    Doc != null &&
                    Doc.Equals(other.Doc)
                ) &&
                (
                    Storage == other.Storage ||
                    Storage != null &&
                    Storage.SequenceEqual(other.Storage)
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
                if (Name != null)
                {
                    hashCode = hashCode * 59 + Name.GetHashCode();
                }

                if (Doc != null)
                {
                    hashCode = hashCode * 59 + Doc.GetHashCode();
                }

                if (Storage != null)
                {
                    hashCode = hashCode * 59 + Storage.GetHashCode();
                }

                return hashCode;
            }
        }

        #region Operators
#pragma warning disable 1591

        public static bool operator ==(TesServiceInfo left, TesServiceInfo right)
            => Equals(left, right);

        public static bool operator !=(TesServiceInfo left, TesServiceInfo right)
            => !Equals(left, right);

#pragma warning restore 1591
        #endregion Operators
    }
}
