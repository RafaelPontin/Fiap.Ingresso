using System.ComponentModel.DataAnnotations;

namespace Fiap.Ingresso.Evento.API.DTOs
{
    public class CadastraEventoDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "DataInicio é obrigatório.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "DataFim é obrigatório.")]
        public DateTime DataFim { get; set; }

        [Required(ErrorMessage = "DataEvento é obrigatório.")]
        public DateTime DataEvento { get; set; }

        [Required(ErrorMessage = "PublicoMaximo é obrigatório.")]
        public int PublicoMaximo { get; set; }

        [Required(ErrorMessage = "Ativo é obrigatório.")]
        public int Ativo { get; set; }

        [Required(ErrorMessage = "Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Numero é obrigatório.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Cidade é obrigatório.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Bairro é obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cep é obrigatório.")]
        public string Cep { get; set; }
        public string? Descricao { get; set; }
        public string? SiteEvento { get; set; }

        [Required(ErrorMessage = "Valor é obrigatório.")]
        public decimal Valor { get; set; }

    }
}
