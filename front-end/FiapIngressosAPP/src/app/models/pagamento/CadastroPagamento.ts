export interface CadastroPagamento
{
  ingressoId : string;
  tipoPagamento: number;
  valorPagamento: number;
  numeroCartao?: string;
  nomeCartao?: string;
  vencimentoCartao?: string;
  codigoVerificador?: string;
}
