using AutoMapper;
using Data.Contract;
using Entities.Dto;
using Entities.Models;
using Moq;

namespace Service.Tests;

public class ClaimServiceTests
{
    private Mock<IDataLoader> _dataLoaderMock;
    private IMapper _mapper;
    private ClaimService _claimService;

    [SetUp]
    public void Setup()
    {
        _dataLoaderMock = new Mock<IDataLoader>();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Claim, ClaimDto>().ReverseMap();
        });
        _mapper = mapperConfig.CreateMapper();

        _claimService = new ClaimService(_dataLoaderMock.Object, _mapper);
    }

    [Test]
    public async Task GetClaim_ShouldReturnClaim_WhenClaimExists()
    {
        var ucr = "UCR123";
        var claim = new Claim { UCR = ucr, CompanyId = 1, ClaimDate = DateTime.Now.AddDays(-25) };
        _dataLoaderMock.Setup(x => x.GetClaimByUCR(ucr)).ReturnsAsync(claim);

        var result = await _claimService.GetClaim(ucr);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.UCR, Is.EqualTo(ucr));
        Assert.That(result.ClaimAgeInDays, Is.EqualTo(25));
    }

    [Test]
    public async Task UpdateClaim_ShouldUpdateClaimSuccessfully()
    {
        var claimDto = new ClaimDto { UCR = "UCR123", CompanyId = 1 };
        _dataLoaderMock.Setup(x => x.UpdateClaim(It.IsAny<Claim>())).Returns(Task.CompletedTask);

        await _claimService.UpdateClaim(claimDto);

        _dataLoaderMock.Verify(x => x.UpdateClaim(It.Is<Claim>(c => c.UCR == claimDto.UCR)), Times.Once);
    }

    [Test]
    public async Task GetClaimAgeInDays_ShouldReturnCorrectAge()
    {
        var claimDate = DateTime.Now.AddDays(-25);

        var result = await _claimService.GetClaimAgeInDays(claimDate);

        Assert.That(result, Is.EqualTo(25));
    }
}
