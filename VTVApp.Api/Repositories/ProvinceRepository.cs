using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Provinces;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly VTVDataContext _dbContext;
        private readonly IMapper _mapper;

        public ProvinceRepository(VTVDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken)
        {
            var provinces = await _dbContext.Provinces.OrderBy(p => p.Name).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ProvinceDto>>(provinces);
        }

        public async Task<ProvinceDto?> GetProvinceByIdAsync(int provinceId, CancellationToken cancellationToken)
        {
            var province = await _dbContext.Provinces.FirstOrDefaultAsync(p => p.Id == provinceId, cancellationToken);

            return _mapper.Map<ProvinceDto?>(province);
        }

        public async Task<OperationResultDto> AddProvinceAsync(ProvinceCreateDto createProvinceDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto> UpdateProvinceAsync(ProvinceDto provinceDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResultDto> DeleteProvinceAsync(Guid provinceId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
