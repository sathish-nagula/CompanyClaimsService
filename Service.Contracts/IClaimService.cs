using Entities.Dto;

namespace Service.Contracts;

public interface IClaimService
{
    Task<ClaimDto> GetClaim(string ucr);
    Task UpdateClaim(ClaimDto claim);
}
