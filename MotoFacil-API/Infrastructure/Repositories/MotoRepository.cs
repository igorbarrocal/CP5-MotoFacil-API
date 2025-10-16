using MongoDB.Driver;
using MotoFacilAPI.Domain.Entities;
using MotoFacilAPI.Domain.Repositories;
using MotoFacilAPI.Infrastructure.Data;

namespace MotoFacilAPI.Infrastructure.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly MongoDbContext _ctx;
        public MotoRepository(MongoDbContext ctx) => _ctx = ctx;

        public async Task<Moto?> GetByIdAsync(string id) =>
            await _ctx.Motos.Find(m => m.Id == id).FirstOrDefaultAsync();

        public async Task<List<Moto>> ListAsync() =>
            await _ctx.Motos.Find(_ => true).ToListAsync();

        public async Task AddAsync(Moto moto) =>
            await _ctx.Motos.InsertOneAsync(moto);

        public async Task UpdateAsync(Moto moto) =>
            await _ctx.Motos.ReplaceOneAsync(m => m.Id == moto.Id, moto);

        public async Task DeleteAsync(string id) =>
            await _ctx.Motos.DeleteOneAsync(m => m.Id == id);
    }
}