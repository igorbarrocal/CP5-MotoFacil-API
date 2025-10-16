using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MotoFacilAPI.Domain.Entities
{
    /// <summary>
    /// Entidade rica: Serviço realizado em uma moto
    /// </summary>
    public class Servico
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Descricao { get; private set; } = string.Empty;
        public DateTime Data { get; private set; } = DateTime.UtcNow;
        public string UsuarioId { get; private set; } = string.Empty;
        public string MotoId { get; private set; } = string.Empty;

        private Servico() { }

        public Servico(string descricao, DateTime data, string usuarioId, string motoId)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));
            if (data == default) data = DateTime.UtcNow;

            Descricao = descricao.Trim();
            Data = data;
            UsuarioId = usuarioId;
            MotoId = motoId;
        }

        /// <summary>
        /// Reagenda a data do serviço (regra de negócio: não pode ser passado distante)
        /// </summary>
        public void Reagendar(DateTime novaData)
        {
            if (novaData < DateTime.UtcNow.AddDays(-1))
                throw new ArgumentException("Não é permitido agendar no passado distante.");
            Data = novaData;
        }
    }
}