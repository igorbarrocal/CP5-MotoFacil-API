using MotoFacilAPI.Domain.Entities;

namespace MotoFacilAPI.Domain.Repositories
{
    public interface IServicoRepository
    {
        Task<Servico?> GetByIdAsync(string id);
        Task<List<Servico>> ListAsync();
        Task AddAsync(Servico servico);
        Task UpdateAsync(Servico servico);
        Task DeleteAsync(string id);
    }
}