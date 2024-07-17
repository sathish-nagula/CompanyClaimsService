using Entities.Models;

namespace Data.Contract
{
    public interface IDataLoader
    {
        Task<List<Company>> GetCompanies();
        Task<List<Claim>> GetClaims();
        Task<Company> GetCompanyById(int id);
        Task<List<Claim>> GetClaimsByCompanyId(int companyId);
        Task<Claim> GetClaimByUCR(string ucr);
        Task UpdateClaim(Claim claim);
    }
}
