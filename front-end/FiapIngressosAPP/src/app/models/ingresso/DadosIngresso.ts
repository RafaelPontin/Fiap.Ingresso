import { DadosEventos } from "../evento/DadosEventos";

export interface DadosIngresso{
  id: string;
  eventoId: string;
  usuarioId: string;
  dataVenda: Date;
  evento : DadosEventos | null;
}
