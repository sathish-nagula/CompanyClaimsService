using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyClaimsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDto>> GetCompanyById(int id)
    {
        var companyDto = await _companyService.GetCompanyById(id);
        if (companyDto == null) return NotFound();
        return Ok(companyDto);
    }

    [HttpGet("{id}/claims")]
    public async Task<ActionResult<List<ClaimDto>>> GetClaimsByCompanyId(int id, int pageNumber = 1, int pageSize = 10)
    {
        var claims = await _companyService.GetClaimsByCompanyId(id, pageNumber, pageSize);
        return Ok(claims);
    }

    [HttpGet("{id}/insuranceStatus")]
    public async Task<ActionResult<bool>> IsInsuranceActive(int id)
    {
        var isActive = await _companyService.IsInsuranceActive(id);
        return Ok(isActive);
    }
}
