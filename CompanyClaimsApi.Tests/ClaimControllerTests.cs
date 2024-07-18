using CompanyClaimsApi.Controllers;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;

namespace CompanyClaimsApi.Tests
{
    public class ClaimControllerTests
    {
        private Mock<IClaimService> _claimServiceMock;
        private ClaimController _controller;

        [SetUp]
        public void Setup()
        {
            _claimServiceMock = new Mock<IClaimService>();
            _controller = new ClaimController(_claimServiceMock.Object);
        }

        [Test]
        public async Task GetClaimByUCR_ShouldReturnClaim_WhenClaimExists()
        {
            var ucr = "UCR1";
            var claimDto = new ClaimDto { UCR = ucr, CompanyId = 1 };
            _claimServiceMock.Setup(x => x.GetClaim(ucr)).ReturnsAsync(claimDto);

            var result = await _controller.GetClaimByUCR(ucr);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>()); 
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); 
            Assert.That(okResult.Value, Is.EqualTo(claimDto)); 
        }

        [Test]
        public async Task GetClaimByUCR_ShouldReturnNotFound_WhenClaimDoesNotExist()
        {
            var ucr = "UCR1";
            _claimServiceMock.Setup(x => x.GetClaim(ucr)).ReturnsAsync((ClaimDto)null);

            var result = await _controller.GetClaimByUCR(ucr);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>()); 
            var notFoundResult = result.Result as NotFoundResult;
            Assert.That(notFoundResult, Is.Not.Null);
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        }

        [Test]
        public async Task UpdateClaim_ShouldReturnNoContent()
        {
            var claimDto = new ClaimDto { UCR = "UCR1", CompanyId = 1 };
            _claimServiceMock.Setup(x => x.UpdateClaim(claimDto)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateClaim(claimDto);

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<NoContentResult>()); 
            var noContentResult = result as NoContentResult;
            Assert.That(noContentResult, Is.Not.Null);
            Assert.That(noContentResult.StatusCode, Is.EqualTo(204)); 
        }
    }
}
