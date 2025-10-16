using System.ComponentModel.DataAnnotations;
using MotoFacilAPI.Domain.Enums;

namespace MotoFacilAPI.Application.Dtos
{
    public class MotoDto
    {
        public string Id { get; set; } = string.Empty;

        [Required]
        public string Placa { get; set; } = string.Empty;

        [Required]
        [EnumDataType(typeof(ModeloMoto), ErrorMessage = "Modelos válidos: MottuSport, MottuE, MottuPop")]
        public ModeloMoto Modelo { get; set; }

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        public List<LinkDto> Links { get; set; } = new();
    }
}