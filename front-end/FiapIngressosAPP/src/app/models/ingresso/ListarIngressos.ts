import { DadosIngresso } from "./DadosIngresso"
import { Ingressos } from "./Ingressos"


export interface ListarIngressos {
  "data": DadosIngresso[] | DadosIngresso | Ingressos[] | Ingressos,
  "title": null,
  "status": number,
  "erros": []
}
