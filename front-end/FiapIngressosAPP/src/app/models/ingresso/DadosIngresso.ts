export interface DadosIngresso{
  id: string;
  eventoId: string;
  usuarioId: string;
  dataVenda: Date;
  nomeEvento: string | null;
  dataEvento: Date | null;
}
