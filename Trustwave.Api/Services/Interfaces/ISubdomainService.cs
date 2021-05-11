using System.Collections.Generic;
using System.Threading.Tasks;
using Trustwave.Common.Models;

namespace Trustwave.Api.Services.Interfaces
{
    /// <summary>
    /// Interface for Subdomain Service
    /// </summary>
    public interface ISubdomainService
    {
        /// <summary>
        /// List all possible subdomains for a specific domain name
        /// </summary>
        /// <param name="domainName">Domain name.</param>
        /// <returns>Return all possible subdomains which contains only third-level domains with no more than 2 alphanumeric characters.</returns>
        Task<EnumerateResponse> EnumerateAsync(string domainName);

        /// <summary>
        /// Find all IP address for specific subdomains.
        /// </summary>
        /// <param name="subdomains">Subdomains.</param>
        /// <returns>Return a list of IP addresses for each of the subdomains.</returns>
        Task<IpAddressesResponse> FindIpAddressesAsync(List<string> subdomains);
    }
}