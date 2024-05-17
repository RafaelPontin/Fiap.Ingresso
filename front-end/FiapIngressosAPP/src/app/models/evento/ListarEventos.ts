import { DadosEventos } from "./DadosEventos"

export interface ListarEventos {
  "data": DadosEventos[] | DadosEventos | string,
  "title": null,
  "status": number,
  "erros": []
}
