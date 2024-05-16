import { DadosIngresso } from "./DadosIngresso"

export interface ListarIngressos {
  "data": DadosIngresso[] | DadosIngresso,
  "title": null,
  "status": number,
  "erros": []
}
