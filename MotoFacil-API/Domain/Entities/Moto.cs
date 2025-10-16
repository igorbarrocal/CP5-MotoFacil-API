using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MotoFacilAPI.Domain.Enums;

namespace MotoFacilAPI.Domain.Entities
{
    public class Moto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Placa { get; private set; } = string.Empty;
        public ModeloMoto Modelo { get; private set; }
        public string UsuarioId { get; private set; } = string.Empty;

        public List<Servico> Servicos { get; private set; } = new();

        private Moto() { }

        public Moto(string placa, ModeloMoto modelo, string usuarioId)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new ArgumentException("Placa é obrigatória.", nameof(placa));

            Placa = placa.Trim().ToUpper();
            Modelo = modelo;
            UsuarioId = usuarioId;
        }

        public void AtualizarPlaca(string novaPlaca)
        {
            if (string.IsNullOrWhiteSpace(novaPlaca))
                throw new ArgumentException("Placa é obrigatória.", nameof(novaPlaca));
            Placa = novaPlaca.Trim().ToUpper();
        }

        public void AtualizarModelo(ModeloMoto novoModelo)
        {
            Modelo = novoModelo;
        }
    }
}