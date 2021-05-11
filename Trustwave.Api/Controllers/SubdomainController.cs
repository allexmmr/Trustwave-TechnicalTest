using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trustwave.Api.Services.Interfaces;
using Trustwave.Common.Models;

namespace Trustwave.Api.Controllers
{
    /// <summary>
    /// Subdomain Controller
    /// </summary>
    [Route("subdomain")]
    [ApiController]
    public class SubdomainController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ISubdomainService _subdomainService;

        /// <summary>
        /// Subdomain Controller Constructor
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="subdomainService">Subdomain Service</param>
        public SubdomainController(ILogger<SubdomainController> logger, ISubdomainService subdomainService)
        {
            _logger = logger;
            _subdomainService = subdomainService;
        }

        // GET
        // subdomain/enumerate/{domainName}

        /// <summary>
        /// List all possible subdomains for a specific domain name.
        /// </summary>
        /// <param name="domainName">Domain name.</param>
        /// <returns>Return all possible subdomains which contains only third-level domains with no more than 2 alphanumeric characters.</returns>
        [HttpGet("enumerate")]
        public async Task<IActionResult> EnumerateAsync([FromBody] string domainName)
        {
            _logger?.LogDebug("'{0}' has been invoked.", nameof(EnumerateAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                EnumerateResponse response = await _subdomainService.EnumerateAsync(domainName);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(EnumerateAsync), ex);

                return StatusCode(StatusCodes.Status500InternalServerError, new IpAddressesResponse { ErrorMessage = ex.Message });
            }
        }

        // POST
        // subdomain/findipaddresses

        /// <summary>
        /// Find all IP address for specific subdomains.
        /// </summary>
        /// <param name="subdomains">Subdomains.</param>
        /// <returns>Return a list of IP addresses for each of the subdomains.</returns>
        [HttpPost("findipaddresses")]
        public async Task<IActionResult> FindIpAddressesAsync([FromBody] List<string> subdomains)
        {
            _logger?.LogDebug("'{0}' has been invoked.", nameof(FindIpAddressesAsync));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                IpAddressesResponse response = await _subdomainService.FindIpAddressesAsync(subdomains);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(FindIpAddressesAsync), ex);

                return StatusCode(StatusCodes.Status500InternalServerError, new IpAddressesResponse { ErrorMessage = ex.Message });
            }
        }
    }
}