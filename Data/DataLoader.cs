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

        _companies = Enumerable.Range(1, 1000).Select(i => new Company
        {
            Id = i,
            Name = $"Company {i}",
            Address1 = $"Address 1-{i}",
            Address2 = $"Address 2-{i}",
            Address3 = $"Address 3-{i}",
            Postcode = $"PC{i}",
            Country = $"Country {i}",
            Active = i % 2 == 0,
            InsuranceEndDate = i % 2 == 0 ? DateTime.Now.AddYears(1) : DateTime.Now.AddMonths(-1)
        }).ToList();

        _claims= Enumerable.Range(1, 1000).Select(i => new Claim
        {
            UCR = $"UCR{i}",
            CompanyId = (i % 20) + 1,
            ClaimDate = DateTime.Now.AddDays(-i),
            LossDate = DateTime.Now.AddDays(-i - 10),
            AssuredName = $"Assured {i}",
            IncurredLoss = 1000 + i * 10,
            Closed = i % 2 == 0
        }).ToList();
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
