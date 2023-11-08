using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Provinces;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface IProvinceRepository
    {
        /// <summary>
        /// Retrieves a list of all provinces.
        /// </summary>
        /// <returns>A list of ProvinceListDto objects.</returns>
        Task<IEnumerable<ProvinceDto>> GetAllProvincesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves details for a single province by ID.
        /// </summary>
        /// <param name="provinceId">The ID of the province.</param>
        /// <returns>A ProvinceDetailDto object.</returns>
        Task<ProvinceDto?> GetProvinceByIdAsync(int provinceId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new province to the repository.
        /// </summary>
        /// <param name="createProvinceDto">The DTO containing the details of the province to be added.</param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<OperationResultDto> AddProvinceAsync(ProvinceCreateDto createProvinceDto, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing province.
        /// </summary>
        /// <param name="provinceDto">The DTO containing the updated details of the province.</param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<OperationResultDto> UpdateProvinceAsync(ProvinceDto provinceDto, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a province from the repository.
        /// </summary>
        /// <param name="provinceId">The ID of the province to be removed.</param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<OperationResultDto> DeleteProvinceAsync(Guid provinceId, CancellationToken cancellationToken);
    }

}
