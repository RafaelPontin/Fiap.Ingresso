import { DadosIngresso } from "./dadosIngresso";
import { Ingressos } from "./ingressos";

export interface ListarIngressos {
  "data": DadosIngresso[] | DadosIngresso | Ingressos[] | Ingressos,
  "title": null,
  "status": number,
  "erros": []
}
