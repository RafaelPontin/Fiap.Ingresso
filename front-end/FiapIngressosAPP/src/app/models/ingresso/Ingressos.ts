import { DadosEventos } from "../evento/DadosEventos";

export interface Ingressos{
  id: string;
  eventoId: string;
  total: string;
  disponiveis: number;
  preco: number;
  dataInicio : Date
  dataFim : Date
  ativo: boolean;
  evento : DadosEventos | null;
}
