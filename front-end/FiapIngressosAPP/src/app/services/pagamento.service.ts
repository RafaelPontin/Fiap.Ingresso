import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastroPagamento } from '../models/pagamento/cadastroPagamento';


@Injectable({
  providedIn: 'root'
})

export class PagamentoService
{
  baseUrlPagamento: string = "http://localhost:8068/"

  constructor(private http: HttpClient) {

  }

  public postCadastraPagamento(body: CadastroPagamento){
    return this.http.post(`${this.baseUrlPagamento}Cadastra-Pagamento`, body);
  }

  public getLinhaDigitavel(idPagamento: string){
    return this.http.get(`${this.baseUrlPagamento}GetLinhaDigitavel?id=${idPagamento}`);
  }


}
