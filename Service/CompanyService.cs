using AutoMapper;
using Data.Contract;
using Entities.Dto;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IDataLoader _dataLoader;
        private readonly IMapper _mapper;

        public CompanyService(IDataLoader dataLoader, IMapper mapper)
        {
            _dataLoader = dataLoader;
            _mapper = mapper;
        }

        public async Task<CompanyDto> GetCompanyById(int id)
        {
            var company = await _dataLoader.GetCompanyById(id);
            if (company == null) return null;

            var companyDto = _mapper.Map<CompanyDto>(company);
            companyDto.HasActivePolicy = company.Active && company.InsuranceEndDate > DateTime.Now;
            return companyDto;
        }

        public async Task<List<ClaimDto>> GetClaimsByCompanyId(int companyId, int pageNumber, int pageSize)
        {
            var claims = await _dataLoader.GetClaimsByCompanyId(companyId, pageNumber, pageSize);
            var claimDtos = _mapper.Map<List<ClaimDto>>(claims);
            claimDtos.ForEach(c => c.ClaimAgeInDays = (DateTime.Now - claims.Find(cl => cl.UCR == c.UCR).ClaimDate).Days);
            return claimDtos;
        }

        public async Task UpdateClaim(ClaimDto claimDto)
        {
            var claim = _mapper.Map<Claim>(claimDto);
            await _dataLoader.UpdateClaim(claim);
        }
    }
}
