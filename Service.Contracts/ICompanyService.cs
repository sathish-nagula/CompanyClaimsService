using Entities.Dto;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<bool> IsInsuranceActive(int companyId);
    Task<CompanyDto> GetCompanyById(int id);
    Task<List<ClaimDto>> GetClaimsByCompanyId(int companyId, int pageNumber, int pageSize);
}

