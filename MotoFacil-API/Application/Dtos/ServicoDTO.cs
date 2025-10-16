using System.ComponentModel.DataAnnotations;

namespace MotoFacilAPI.Application.Dtos
{
    /// <summary>
    /// Dados do serviço realizado em uma moto
    /// </summary>
    public class ServicoDto
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [Required]
        public string MotoId { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new();
    }
}