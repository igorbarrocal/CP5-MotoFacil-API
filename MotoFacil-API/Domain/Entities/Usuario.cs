using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MotoFacilAPI.Domain.ValueObjects;

namespace MotoFacilAPI.Domain.Entities
{
    /// <summary>
    /// Agregado Raiz: Usuário
    /// </summary>
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string Nome { get; private set; } = string.Empty;
        public Email Email { get; private set; } = default!;

        // Não usar List<Moto> em MongoDB - apenas Ids nas entidades filhas

        private Usuario() { }

        public Usuario(string nome, Email email)
        {
            AlterarNome(nome);
            Email = email;
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório.", nameof(nome));
            Nome = nome.Trim();
        }

        public void AlterarEmail(Email novoEmail)
        {
            Email = novoEmail ?? throw new ArgumentNullException(nameof(novoEmail), "Email obrigatório");
        }
    }
}