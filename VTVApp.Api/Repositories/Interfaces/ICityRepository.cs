using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface ICityRepository
    {
        /// <summary>
        /// Retrieves a list of all cities.
        /// </summary>
        /// <returns>A collection of CityDto objects.</returns>
        Task<IEnumerable<CityDetailsDto>> GetAllCitiesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a specific city by its identifier.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city.</param>
        /// <returns>The CityDto if found; otherwise, null.</returns>
        Task<CityDetailsDto?> GetCityByIdAsync(Guid cityId, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list of all cities for a given province Id.
        /// </summary>
        /// <param name="provinceId">The unique identifier of the province.</param>
        /// <returns>A collection of CityDto objects.</returns>
        Task<IEnumerable<CityDetailsDto>> GetCitiesByProvinceId(int provinceId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new city to the repository.
        /// </summary>
        /// <param name="city">The CityDto containing the details of the city to add.</param>
        /// <returns>The operation result including the created city's details.</returns>
        Task<CityOperationResultDto> AddCityAsync(CityDetailsDto city, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the details of an existing city.
        /// </summary>
        /// <param name="city">The CityDto containing the updated details of the city.</param>
        /// <returns>The operation result including the updated city's details.</returns>
        Task<CityOperationResultDto> UpdateCityAsync(CityDetailsDto city, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a city from the repository.
        /// </summary>
        /// <param name="cityId">The unique identifier of the city to delete.</param>
        /// <returns>The operation result indicating the success or failure of the deletion.</returns>
        Task<CityOperationResultDto> DeleteCityAsync(Guid cityId, CancellationToken cancellationToken);
    }


}
