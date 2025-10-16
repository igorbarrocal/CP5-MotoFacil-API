using System.ComponentModel.DataAnnotations;

namespace MotoFacilAPI.Application.Dtos
{
    /// <summary>
    /// Dados do usuário
    /// </summary>
    public class UsuarioDto
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new();
    }
}