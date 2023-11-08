using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly VTVDataContext _dbContext;
        private readonly IMapper _mapper;

        public CityRepository(VTVDataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDetailsDto>> GetAllCitiesAsync(CancellationToken cancellationToken)
        {
            var cities = await _dbContext.Cities.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CityDetailsDto>>(cities);
        }

        public async Task<CityDetailsDto?> GetCityByIdAsync(Guid cityId, CancellationToken cancellationToken)
        {
            var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == cityId, cancellationToken);

            return _mapper.Map<CityDetailsDto?>(city);
        }

        public async Task<IEnumerable<CityDetailsDto>> GetCitiesByProvinceId(int provinceId, CancellationToken cancellationToken)
        {
            var cities = await _dbContext.Cities
                .Include(x => x.Province)
                .Where(c => c.ProvinceId == provinceId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<CityDetailsDto>>(cities);
        }

        public async Task<CityOperationResultDto> AddCityAsync(CityDetailsDto city, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CityOperationResultDto> UpdateCityAsync(CityDetailsDto city, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<CityOperationResultDto> DeleteCityAsync(Guid cityId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
