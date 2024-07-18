using CompanyClaimsApi.Controllers;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Contracts;

namespace CompanyClaimsApi.Tests
{
    public class CompanyControllerTests
    {
        private Mock<ICompanyService> _companyServiceMock;
        private CompanyController _controller;

        [SetUp]
        public void Setup()
        {
            _companyServiceMock = new Mock<ICompanyService>();
            _controller = new CompanyController(_companyServiceMock.Object);
        }

        [Test]
        public async Task GetCompanyById_ShouldReturnCompany()
        {
            var companyId = 1;
            var companyDto = new CompanyDto { Id = companyId, Name = "Company1" };
            _companyServiceMock.Setup(x => x.GetCompanyById(companyId)).ReturnsAsync(companyDto);

            var result = await _controller.GetCompanyById(companyId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>()); 
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); 

            var returnedCompany = okResult.Value as CompanyDto; 
            Assert.That(returnedCompany, Is.Not.Null);
            Assert.That(returnedCompany.Id, Is.EqualTo(companyId)); 
            Assert.That(returnedCompany.Name, Is.EqualTo("Company1"));
        }


        [Test]
        public async Task GetClaimsByCompanyId_ReturnsClaims()
        {
            var companyId = 1;
            var pageNumber = 1;
            var pageSize = 10;
            var claims = new List<ClaimDto>
            {
                new ClaimDto { UCR = "UCR1", CompanyId = 1 },
                new ClaimDto { UCR = "UCR2", CompanyId = 1 }
            };
            _companyServiceMock.Setup(x => x.GetClaimsByCompanyId(companyId, pageNumber, pageSize)).ReturnsAsync(claims);

            var result = await _controller.GetClaimsByCompanyId(companyId, pageNumber, pageSize);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.InstanceOf<OkObjectResult>()); 
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200)); 

            var returnedClaims = okResult.Value as List<ClaimDto>; 
            Assert.That(returnedClaims, Is.Not.Null);
            Assert.That(returnedClaims.Count, Is.EqualTo(claims.Count)); 
            Assert.That(returnedClaims[0].UCR, Is.EqualTo(claims[0].UCR)); 
            Assert.That(returnedClaims[1].UCR, Is.EqualTo(claims[1].UCR));
        }

    }
}
