﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Trustwave.Api.Services.Interfaces;
using Trustwave.Common.Models;

namespace Trustwave.Api.Services
{
    /// <summary>
    /// Subdomain Service.
    /// </summary>
    public class SubdomainService : ISubdomainService
    {
        private readonly ILogger<SubdomainService> _logger;

        /// <summary>
        /// Subdomain Service Constructor.
        /// </summary>
        /// <param name="logger">Logger</param>
        public SubdomainService(ILogger<SubdomainService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// List all possible subdomains for a specific domain name
        /// </summary>
        /// <param name="domainName">Domain name.</param>
        /// <returns>Return all possible subdomains which contains only third-level domains with no more than 2 alphanumeric characters.</returns>
        public async Task<EnumerateResponse> EnumerateAsync(string domainName)
        {
            _logger?.LogDebug("'{0}' has been invoked.", nameof(EnumerateAsync));

            try
            {
                #region Validations

                if (string.IsNullOrWhiteSpace(domainName))
                {
                    throw new Exception("Domain name is not provided.");
                }

                #endregion

                List<string> combinations = FindCombinations();
                List<string> subdomains = new List<string>();

                foreach (string combination in combinations)
                {
                    string subdomain = $"{combination}.{domainName}";
                    bool exists = PingDomain(subdomain);

                    if (exists)
                    {
                        subdomains.Add(subdomain);
                    }
                }

                return await Task.Run(() => new EnumerateResponse()
                {
                    Subdomains = subdomains,
                    Success = true
                });
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(EnumerateAsync), ex);

                return await Task.Run(() => new EnumerateResponse()
                {
                    Subdomains = null,
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Find all combinations.
        /// </summary>
        /// <param name="size">Size of the combination</param>
        /// <returns>Return a list of all possible combinations.</returns>
        private static List<string> FindCombinations(int size = 2)
        {
            List<string> result = new List<string>();

            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            IEnumerable<string> items = alphabet.Select(x => x.ToString());

            for (int i = 0; i < size - 1; i++)
            {
                items = items.SelectMany(x => alphabet, (x, y) => x + y);
            }

            foreach (string item in items)
            {
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// Pings the specified domain name.
        /// </summary>
        /// <param name="domainName">Domain name.</param>
        /// <returns>Return true if it exists, otherwise false.</returns>
        private static bool PingDomain(string domainName)
        {
            // TODO:
            // I don't believe pinging a domain name will tell you anything relevant in a reliable way.
            // A domain name can be registered but not connected to a server.
            // A server can be fully operational but be configured not to respond to ping requests.

            try
            {
                Ping ping = new Ping();
                PingReply result = ping.Send(domainName);

                return result.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Find all IP address for specific subdomains.
        /// </summary>
        /// <param name="subdomains">Subdomains.</param>
        /// <returns>Return a list of IP addresses for each of the subdomains.</returns>
        public async Task<IpAddressesResponse> FindIpAddressesAsync(List<string> subdomains)
        {
            _logger?.LogDebug("'{0}' has been invoked.", nameof(FindIpAddressesAsync));

            try
            {
                #region Validations

                if (subdomains == null || !subdomains.Any())
                {
                    throw new Exception("Subdomains are not provided.");
                }

                #endregion

                List<Result> results = new List<Result>();

                foreach (string subdomain in subdomains)
                {
                    List<string> ipAddresses = FindIpAddressBySubdomain(subdomain);

                    results.Add(new Result
                    {
                        Subdomain = subdomain,
                        IpAddresses = ipAddresses
                    });
                }

                return await Task.Run(() => new IpAddressesResponse()
                {
                    Results = results,
                    Success = true,
                });
            }
            catch (Exception ex)
            {
                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(FindIpAddressesAsync), ex);

                return await Task.Run(() => new IpAddressesResponse()
                {
                    Results = null,
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }
        }

        /// <summary>
        /// Find all IP address by subdomain.
        /// </summary>
        /// <param name="subdomain">Subdomain.</param>
        /// <returns>Return a list of IP addresses for the subdomain.</returns>
        private static List<string> FindIpAddressBySubdomain(string subdomain)
        {
            List<string> result = new List<string>();

            IPAddress[] ipAddresses = Dns.GetHostAddresses(subdomain);

            foreach (IPAddress ipAddress in ipAddresses)
            {
                result.Add(ipAddress.ToString());
            }

            return result;
        }
    }
}