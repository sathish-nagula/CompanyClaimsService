using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace CompanyClaimsApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimController : ControllerBase
{
    private readonly IClaimService _claimService;

    public ClaimController(IClaimService claimService)
    {
        _claimService = claimService;
    }

    [HttpGet("{ucr}")]
    public async Task<ActionResult<ClaimDto>> GetClaimByUCR(string ucr)
    {
        var claimDto = await _claimService.GetClaim(ucr);
        if (claimDto == null) return NotFound();
        return Ok(claimDto);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateClaim(ClaimDto claimDto)
    {
        await _claimService.UpdateClaim(claimDto);
        return NoContent();
    }

}
