using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
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
        public async Task EnumerateAsync_NotNull()
        {
            // Act
            EnumerateResponse actual = await _subdomainService.EnumerateAsync("yahoo.com");

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
        }

        [Fact]
        public async Task FindIpAddressesAsync_NotNull()
        {
            // Act
            IpAddressesResponse actual = await _subdomainService.FindIpAddressesAsync( new List<string> { "yahoo.com" });

            // Assert
            Assert.NotNull(actual);
            Assert.True(actual.Success);
        }
    }
}