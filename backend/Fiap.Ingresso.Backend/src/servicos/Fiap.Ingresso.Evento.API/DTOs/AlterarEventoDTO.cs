using System.ComponentModel.DataAnnotations;

namespace Fiap.Ingresso.Evento.API.DTOs;

public class AlterarEventoDTO : CadastraEventoDto
{
    [Required(ErrorMessage = "Id é obrigatório.")]
    public Guid Id { get; set; }

}
