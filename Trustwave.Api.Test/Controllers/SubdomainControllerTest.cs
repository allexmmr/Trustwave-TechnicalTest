using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trustwave.Api.Controllers;
using Trustwave.Api.Services.Interfaces;
using Trustwave.Common.Models;
using Xunit;

namespace Trustwave.Api.Test.Controllers
{
    public class SubdomainControllerTest
    {
        [Fact]
        public async Task EnumerateAsync_ReturnsSuccess()
        {
            // Arrange
            Mock<ILogger<SubdomainController>> mockLogger = new Mock<ILogger<SubdomainController>>();
            Mock<ISubdomainService> mockSubdomainService = new Mock<ISubdomainService>();

            const string domainName = "yahoo.com";

            EnumerateResponse response = new EnumerateResponse
            {
                Subdomains = new List<string>
                {
                    "aa.yahoo.com"
                },
                Success = true
            };

            mockSubdomainService.Setup(q => q.EnumerateAsync(domainName)).Returns(Task.FromResult(response));

            SubdomainController controller = new SubdomainController(mockLogger.Object, mockSubdomainService.Object);

            // Act
            IActionResult result = await controller.EnumerateAsync(domainName);

            ObjectResult okObjectResult = result as ObjectResult;

            // Assert
            EnumerateResponse actual = (EnumerateResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.True(okObjectResult is OkObjectResult);
            Assert.IsType<EnumerateResponse>(okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.True(actual.Success);
        }

        [Fact]
        public async Task FindIpAddressesAsync_ReturnsSuccess()
        {
            // Arrange
            Mock<ILogger<SubdomainController>> mockLogger = new Mock<ILogger<SubdomainController>>();
            Mock<ISubdomainService> mockSubdomainService = new Mock<ISubdomainService>();

            List<string> subdomains = new List<string> { "ar.yahoo.com" };

            IpAddressesResponse response = new IpAddressesResponse
            {
                Results = new List<Result>
                {
                    new Result { Subdomain = "ar.yahoo.com", IpAddresses = new List<string> { "106.10.248.150" } }
                },
                Success = true
            };

            mockSubdomainService.Setup(q => q.FindIpAddressesAsync(subdomains)).Returns(Task.FromResult(response));

            SubdomainController controller = new SubdomainController(mockLogger.Object, mockSubdomainService.Object);

            // Act
            IActionResult result = await controller.FindIpAddressesAsync(subdomains);

            ObjectResult okObjectResult = result as ObjectResult;

            // Assert
            IpAddressesResponse actual = (IpAddressesResponse)okObjectResult.Value;

            Assert.NotNull(okObjectResult);
            Assert.True(okObjectResult is OkObjectResult);
            Assert.IsType<IpAddressesResponse>(okObjectResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
            Assert.True(actual.Success);
        }
    }
}