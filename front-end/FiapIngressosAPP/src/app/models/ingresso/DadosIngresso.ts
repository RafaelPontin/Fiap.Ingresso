import { DadosEventos } from "../evento/dadosEventos";

export interface DadosIngresso{
  id: string;
  eventoId: string;
  usuarioId: string;
  dataVenda: Date;
  evento : DadosEventos | null;
}
