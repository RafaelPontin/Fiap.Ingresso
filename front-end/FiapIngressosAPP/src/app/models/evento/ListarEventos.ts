import { DadosEventos } from "./dadosEventos"

export interface ListarEventos {
  "data": DadosEventos[] | DadosEventos | string,
  "title": null,
  "status": number,
  "erros": []
}
