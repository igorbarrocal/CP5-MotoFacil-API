using MongoDB.Driver;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Data;

namespace MotoFacilAPI.Infrastructure.Repositories
{
    public class ServicoRepository : IServicoRepository
    {
        private readonly MongoDbContext _ctx;
        public ServicoRepository(MongoDbContext ctx) => _ctx = ctx;

        public async Task<Servico?> GetByIdAsync(string id) =>
            await _ctx.Servicos.Find(s => s.Id == id).FirstOrDefaultAsync();

        public async Task<List<Servico>> ListAsync() =>
            await _ctx.Servicos.Find(_ => true).ToListAsync();

        public async Task AddAsync(Servico servico) =>
            await _ctx.Servicos.InsertOneAsync(servico);

        public async Task UpdateAsync(Servico servico) =>
            await _ctx.Servicos.ReplaceOneAsync(s => s.Id == servico.Id, servico);

        public async Task DeleteAsync(string id) =>
            await _ctx.Servicos.DeleteOneAsync(s => s.Id == id);
    }
}