using Entities.Dto;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<CompanyDto> GetCompanyById(int id);
    Task<List<ClaimDto>> GetClaimsByCompanyId(int companyId, int pageNumber, int pageSize);
}

