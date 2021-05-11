using Newtonsoft.Json;

namespace Trustwave.Common.Models.Base
{
    /// <summary>
    /// Base Response Class.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Success.
        /// </summary>
        [JsonProperty("success")]
        public bool Success { get; set; }

        /// <summary>
        /// Error message if any.
        /// </summary>
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }
    }
}