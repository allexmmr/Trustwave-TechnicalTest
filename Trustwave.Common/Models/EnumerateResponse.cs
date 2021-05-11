using Newtonsoft.Json;
using System.Collections.Generic;
using Trustwave.Common.Models.Base;

namespace Trustwave.Common.Models
{
    /// <summary>
    /// Enumerate Response Class.
    /// </summary>
    public class EnumerateResponse : BaseResponse
    {
        /// <summary>
        /// Subdomains.
        /// </summary>
        [JsonProperty("subdomains")]
        public List<string> Subdomains { get; set; }
    }
}