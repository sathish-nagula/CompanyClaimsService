using Data.Contract;
using Entities.Models;

namespace Data;

public class DataLoader : IDataLoader
{
    private List<Company> _companies;
    private List<Claim> _claims;

    public DataLoader()
    {
        _companies = new List<Company>();
        _claims = new List<Claim>();
        for (int i = 1; i <= 100; i++)
        {
            _companies.Add(new Company
            {
                Id = i,
                Name = $"Company {i}",
                Address1 = $"Address1 {i}",
                Address2 = $"Address2 {i}",
                Address3 = $"Address3 {i}",
                Postcode = $"Postcode {i}",
                Country = $"Country {i}",
                Active = i % 2 == 0,
                InsuranceEndDate = DateTime.Now.AddYears(1).AddDays(i)
            });

            _claims.Add(new Claim
            {
                UCR = $"UCR{i}",
                CompanyId = i,
                ClaimDate = DateTime.Now.AddDays(-i),
                LossDate = DateTime.Now.AddDays(-i * 2),
                AssuredName = $"Assured Name {i}",
                IncurredLoss = i * 1000,
                Closed = i % 2 == 0
            });
        }
    }

    public Task<List<Company>> GetCompanies()
    {
        return Task.FromResult(_companies);
    }

    public Task<List<Claim>> GetClaims()
    {
        return Task.FromResult(_claims);
    }

    public Task<Company> GetCompanyById(int id)
    {
        return Task.FromResult(_companies.FirstOrDefault(c => c.Id == id));
    }

    public Task<List<Claim>> GetClaimsByCompanyId(int companyId)
    {
        return Task.FromResult(_claims.Where(c => c.CompanyId == companyId).ToList());
    }

    public Task<Claim> GetClaimByUCR(string ucr)
    {
        return Task.FromResult(_claims.FirstOrDefault(c => c.UCR == ucr));
    }

    public Task UpdateClaim(Claim claim)
    {
        var existingClaim = _claims.FirstOrDefault(c => c.UCR == claim.UCR);
        if (existingClaim != null)
        {
            existingClaim.ClaimDate = claim.ClaimDate;
            existingClaim.LossDate = claim.LossDate;
            existingClaim.AssuredName = claim.AssuredName;
            existingClaim.IncurredLoss = claim.IncurredLoss;
            existingClaim.Closed = claim.Closed;
        }
        return Task.CompletedTask;
    }

}
