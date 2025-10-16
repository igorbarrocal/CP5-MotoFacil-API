using MongoDB.Driver;
using MotoFacilAPI.Domain.Entities;

namespace MotoFacilAPI.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string dbName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(dbName);
        }

        public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");
        public IMongoCollection<Moto> Motos => _database.GetCollection<Moto>("Motos");
        public IMongoCollection<Servico> Servicos => _database.GetCollection<Servico>("Servicos");
    }
}