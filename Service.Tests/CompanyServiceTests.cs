using AutoMapper;
using Data.Contract;
using Entities.Dto;
using Entities.Models;
using Moq;
using Service.Contracts;

namespace Service.Tests
{
    public class CompanyServiceTests
    {
        private Mock<IDataLoader> _dataLoaderMock;
        private IMapper _mapper;
        private ICompanyService _companyService;

        [SetUp]
        public void Setup()
        {
            _dataLoaderMock = new Mock<IDataLoader>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<Claim, ClaimDto>();
            });
            _mapper = mapperConfig.CreateMapper();
            _companyService = new CompanyService(_dataLoaderMock.Object, _mapper);
        }

        [Test]
        public async Task GetCompanyById_ShouldReturnCompany_WhenCompanyExists()
        {
            var companyId = 1;
            var company = new Company { Id = companyId, Name = "Company1", Active = true, InsuranceEndDate = DateTime.Now.AddMonths(1) };
            _dataLoaderMock.Setup(x => x.GetCompanyById(companyId)).ReturnsAsync(company);

            var result = await _companyService.GetCompanyById(companyId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(companyId));
            Assert.That(result.Name, Is.EqualTo("Company1"));
            Assert.That(result.HasActivePolicy, Is.True);
        }

        [Test]
        public async Task IsInsuranceActive_ShouldReturnTrue_WhenInsuranceIsActive()
        {
            var companyId = 1;
            var company = new Company { Id = companyId, Active = true, InsuranceEndDate = DateTime.Now.AddMonths(1) };
            _dataLoaderMock.Setup(x => x.GetCompanyById(companyId)).ReturnsAsync(company);

            var result = await _companyService.IsInsuranceActive(companyId);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task GetClaimsByCompanyId_ShouldReturn_WhenClaimExists()
        {
            var companyId = 1;
            var claims = new List<Claim>
            {
                new Claim { UCR = "UCR1", CompanyId = 1, ClaimDate = DateTime.Now.AddDays(-15) },
                new Claim { UCR = "UCR2", CompanyId = 1, ClaimDate = DateTime.Now.AddDays(-20) }
            };
            _dataLoaderMock.Setup(x => x.GetClaimsByCompanyId(companyId)).ReturnsAsync(claims);

            var result = await _companyService.GetClaimsByCompanyId(companyId, 1, 10);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].ClaimAgeInDays, Is.EqualTo(15));
            Assert.That(result[1].ClaimAgeInDays, Is.EqualTo(20));
        }
    }
}
