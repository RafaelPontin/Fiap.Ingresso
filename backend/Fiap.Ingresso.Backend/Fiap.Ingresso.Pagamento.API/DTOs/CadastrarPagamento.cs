using Fiap.Ingresso.Pagamento.API.Domain;
using Fiap.Ingresso.Pagamento.API.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Ingresso.Pagamento.API.DTOs
{
    public record CadastrarPagamento
    {
        [Required(ErrorMessage = "IngressoId é obrigatório.")]
        public Guid IngressoId { get; set; }

        [Required(ErrorMessage = "TipoPagamento é obrigatório.")]
        public EPagamento TipoPagamento { get; set; }

        [Required(ErrorMessage = "ValorPagamento é obrigatório.")]
        public decimal ValorPagamento { get; set; }

        public string? NumeroCartao { get; set; }
        public string? NomeCartao { get; set; }
        public string? VencimentoCartao { get; set; }
        public string? CodigoVerificador { get; set; }


        public Domain.Pagamento ConvertToPagamento()
        {
            Domain.Pagamento pagamento = new Domain.Pagamento();
            if(TipoPagamento == EPagamento.Cartao)
                pagamento.AdicionarPagamentoCartao(IngressoId, ValorPagamento, NumeroCartao, NomeCartao, VencimentoCartao, CodigoVerificador);
            else
                pagamento.AdicionarPagamentoBoleto(IngressoId, ValorPagamento);

            return pagamento;
        }
    }
}
