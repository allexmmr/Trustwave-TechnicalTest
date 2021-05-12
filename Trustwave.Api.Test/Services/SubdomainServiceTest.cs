using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trustwave.Api.Services;
using Trustwave.Common.Models;
using Xunit;

namespace Trustwave.Api.Test.Services
{
    public class SubdomainServiceTest
    {
        private readonly SubdomainService _subdomainService;

        public SubdomainServiceTest()
        {
            // Arrange
            Mock<ILogger<SubdomainService>> mockLogger = new Mock<ILogger<SubdomainService>>();
            _subdomainService = new SubdomainService(mockLogger.Object);
        }

        [Fact]
        public async Task EnumerateAsync_ReturnsSuccess()
        {
            // Act
            EnumerateResponse actual = await _subdomainService.EnumerateAsync("yahoo.com");

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
        }

        [Fact]
        public async Task EnumerateAsync_ReturnsSuccessAndArSubdomainAsFirstOrDefault()
        {
            // Act
            EnumerateResponse actual = await _subdomainService.EnumerateAsync("yahoo.com");

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.Equal("ar.yahoo.com", actual.Subdomains.FirstOrDefault());
        }

        [Fact]
        public async Task EnumerateAsync_ReturnsNoSubdomains()
        {
            // Act
            EnumerateResponse actual = await _subdomainService.EnumerateAsync("yahoooo.com");

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.True(actual.Subdomains?.Count == 0);
        }

        [Fact]
        public async Task FindIpAddressesAsync_ReturnsSuccess()
        {
            // Act
            IpAddressesResponse actual = await _subdomainService.FindIpAddressesAsync(new List<string> { "ar.yahoo.com" });

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
        }

        [Fact]
        public async Task FindIpAddressesAsync_ReturnsSuccessAndIpForArSubdomain()
        {
            // Act
            IpAddressesResponse actual = await _subdomainService.FindIpAddressesAsync(new List<string> { "ar.yahoo.com" });
            IpAddressesResponse expected = await Task.Run(() => new IpAddressesResponse()
            {
                Results = new List<Result>
                {
                    new Result { Subdomain = "ar.yahoo.com", IpAddresses = new List<string> { "106.10.248.150" } }
                },
                Success = true
            });

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.Equal(expected.Results?.FirstOrDefault().Subdomain, actual.Results?.FirstOrDefault().Subdomain);
            Assert.Equal(expected.Results?.FirstOrDefault().IpAddresses, actual.Results?.FirstOrDefault().IpAddresses);
        }

        [Fact]
        public async Task FindIpAddressesAsync_ReturnsNoIpAddresses()
        {
            // Act
            IpAddressesResponse actual = await _subdomainService.FindIpAddressesAsync(new List<string> { "aa.yahoo.com" });

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
            Assert.True(actual.Results?.FirstOrDefault().IpAddresses == null);
        }
    }
}