using MotoFacilAPI.Application.Dtos;

namespace MotoFacilAPI.Application.Interfaces
{
    public interface IServicoService
    {
        Task<List<ServicoDto>> ListAsync();
        Task<ServicoDto?> GetByIdAsync(string id);
        Task<ServicoDto> CreateAsync(ServicoDto dto);
        Task<bool> UpdateAsync(string id, ServicoDto dto);
        Task<bool> DeleteAsync(string id);
    }
}