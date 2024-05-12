import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastroPagamento } from '../models/pagamento/CadastroPagamento';
import { CompraIngresso } from '../models/Ingresso/CompraIngresso';


@Injectable({
  providedIn: 'root'
})

export class PagamentoService
{

  baseUrlIngresso: string = "https://localhost:7186/";
  baseUrlPagamento: string = "https://localhost:7154/"

  constructor(private http: HttpClient) {

  }

  public postCompraIngresso(body: CompraIngresso, ingressoId: string){
    return this.http.post(`${this.baseUrlIngresso}Comprar/${ingressoId}`, body);
  }

  public getEventoValido(idEvento: string){
    return this.http.get(`${this.baseUrlIngresso}Obter-Por-Evento/${idEvento}`);
  }

  public postCadastraPagamento(body: CadastroPagamento){
    return this.http.post(`${this.baseUrlPagamento}Cadastra-Pagamento`, body);
  }

  public getLinhaDigitavel(idPagamento: string){
    return this.http.get(`${this.baseUrlPagamento}GetLinhaDigitavel?id=${idPagamento}`);
  }


}
