using System;
using System.Threading.Tasks;
using AutoMapper;
using Data.Contract;
using Entities.Dto;
using Entities.Models;
using Service.Contracts;

namespace Service
{
    public class ClaimService : IClaimService
    {
        private readonly IDataLoader _dataLoader;
        private readonly IMapper _mapper;

        public ClaimService(IDataLoader dataLoader, IMapper mapper)
        {
            _dataLoader = dataLoader;
            _mapper = mapper;
        }

        public async Task<ClaimDto> GetClaim(string ucr)
        {
            var claim = await _dataLoader.GetClaimByUCR(ucr);
            if (claim == null) return null;

            var claimDto = _mapper.Map<ClaimDto>(claim);
            claimDto.ClaimAgeInDays = await GetClaimAgeInDays(claim.ClaimDate);
            return claimDto;
        }

        public async Task UpdateClaim(ClaimDto claimDto)
        {
            var claim = _mapper.Map<Claim>(claimDto);
            await _dataLoader.UpdateClaim(claim);
        }

        public async Task<int> GetClaimAgeInDays(DateTime claimDate)
        {
            return await Task.FromResult((DateTime.Now - claimDate).Days);
        }
    }
}
