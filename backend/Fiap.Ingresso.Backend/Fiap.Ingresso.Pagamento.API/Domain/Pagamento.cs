using Fiap.Ingresso.Pagamento.API.Domain.Enum;

namespace Fiap.Ingresso.Pagamento.API.Domain
{
    public class Pagamento
    {
        public Guid Id { get; set; }
        public Guid? IngressoId { get; set; }

        public EPagamento TipoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }

        //cartao 
        public string? NumeroCartao { get; set; }
        public string? NomeCartao { get; set;}
        public string? VencimentoCartao { get; set; }
        public string? CodigoVerificador { get; set; }

        //boleto
        public string? LinhaDigitavel { get; set; }

        public DateTime DataPagamento { get; set; }
        public EPagamentoValido PagamentoValido { get; set; }

        public List<string> Erros { get;}

        public Pagamento()
        {
            Erros = new List<string>();
        }

        public void AdicionarPagamentoCartao(Guid ingressoId, decimal valorPagamento, string numeroCartao, string nomeCartao, string vencimento, string codigoVerificador)
        {
            SetIngresso(ingressoId);
            TipoPagamento = EPagamento.Cartao;
            SetValorPagamento(valorPagamento);
            SetCartao(numeroCartao, nomeCartao, vencimento, codigoVerificador);
            DataPagamento = DateTime.Now;
            GeraId();
            ValidaPagamento();
        }

        public void AdicionarPagamentoBoleto(Guid ingressoId, decimal valorPagamento)
        {
            SetIngresso(ingressoId);
            TipoPagamento = EPagamento.Boleto;
            SetValorPagamento(valorPagamento);
            DataPagamento = DateTime.Now;
            GeraId();
            if (!Erros.Any()) GerarLinhaDigitavel();
            ValidaPagamento();
        }

        public void GeraId()
        {
            if (!Erros.Any())
            {
                Id = Guid.NewGuid();
            }
        }

        public void GerarLinhaDigitavel()
        {
            string valorAjustado = ValorPagamento.ToString().Replace('.',' ').PadLeft(10);
            string dataAjustada = DataPagamento.ToString("ddMMyyyy");
            LinhaDigitavel = $"001 9 050095 401448 1606  9 0680935031 4 {dataAjustada}{valorAjustado}";
        }

        public void SetValorPagamento(decimal valorPagamento)
        {
            if(valorPagamento <= 0)
            {
                ValorPagamento = default;
                Erros.Add($"Valor do pagamento {valorPagamento} invalido");
            }else
            {
                ValorPagamento = valorPagamento;
            }
        }

        public void ValidaPagamento()
        {
            if (Erros.Any())
                PagamentoValido = EPagamentoValido.Invalido;
            else
                PagamentoValido = EPagamentoValido.Valido;
        }

        public void SetCartao(string numeroCartao, string nomeCartao, string vencimentoCartao, string codigoVerificador)
        {
            if (!string.IsNullOrWhiteSpace(numeroCartao))
            {
                if (string.IsNullOrWhiteSpace(nomeCartao) || string.IsNullOrWhiteSpace(vencimentoCartao) || string.IsNullOrWhiteSpace(codigoVerificador))
                {
                    Erros.Add("Dados do cartao invalido");
                    NomeCartao = default;
                    VencimentoCartao = default;
                    CodigoVerificador = default;
                }
                else
                {
                    NomeCartao = nomeCartao;
                    VencimentoCartao = vencimentoCartao;
                    CodigoVerificador = codigoVerificador;
                    NumeroCartao = numeroCartao;
                }
            }
            else
            {
                Erros.Add("Informação de cartão invalida");
                NumeroCartao = string.Empty;
            }
        }

        public void SetIngresso(Guid? ingressoId)
        {
            if(ingressoId == null)
            {
                Erros.Add("Ingresso invalido");
                IngressoId = null;
            }
            else
            {
                IngressoId = ingressoId;
            }
        }

    }
}
