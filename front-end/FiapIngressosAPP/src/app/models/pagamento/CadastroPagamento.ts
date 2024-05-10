export interface CadastroPagamento
{
  IngressoId : string;
  TipoPagamento: number;
  ValorPagamento: number;
  NumeroCartao?: string;
  NomeCartao?: string;
  VencimentoCartao?: string;
  CodigoVerificador?: string;
}
