using Entities.Dto;

namespace Service.Contracts;

public interface IClaimService
{
    Task<ClaimDto> GetClaim(string ucr);
    void UpdateClaim(ClaimDto claim);
    int GetClaimAgeInDays(DateTime claimDate);
}
