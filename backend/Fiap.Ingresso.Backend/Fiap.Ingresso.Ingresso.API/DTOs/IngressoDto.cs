namespace Fiap.Ingresso.Ingresso.API.DTOs;

public record IngressoDto
{ 
    public Guid UsuarioId { get; set; }
    public int Quantidade { get; set; }
}
