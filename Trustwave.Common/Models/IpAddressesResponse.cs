using Newtonsoft.Json;
using System.Collections.Generic;
using Trustwave.Common.Models.Base;

namespace Trustwave.Common.Models
{
    /// <summary>
    /// Search Response Class.
    /// </summary>
    public class IpAddressesResponse : BaseResponse
    {
        /// <summary>
        /// Results.
        /// </summary>
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        /// <summary>
        /// Subdomain.
        /// </summary>
        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        /// <summary>
        /// IP Addresses.
        /// </summary>
        [JsonProperty("ipAddresses")]
        public List<string> IpAddresses { get; set; }
    }
}