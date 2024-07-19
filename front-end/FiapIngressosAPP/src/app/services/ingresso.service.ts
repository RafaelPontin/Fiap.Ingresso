import { NovoIngresso } from '../models/ingresso/covoIngresso';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompraIngresso } from '../models/ingresso/compraIngresso';
import { Observable } from 'rxjs';
import { ListarIngressos } from '../models/ingresso/listarIngressos';

@Injectable({
  providedIn: 'root'
})
export class IngressoService {

  baseUrlIngresso: string = "https://localhost:8067/";

  constructor(private http: HttpClient) { }

  public postCompraIngresso(body: CompraIngresso, ingressoId: string){
    return this.http.post(`${this.baseUrlIngresso}Comprar/${ingressoId}`, body);
  }

  public getEventoValido(idEvento: string) : Observable<ListarIngressos>{
    return this.http.get<ListarIngressos>(`${this.baseUrlIngresso}Obter-Por-Evento/${idEvento}`);
  }

  public getHistoricoIngressoPorUsuario() : Observable<ListarIngressos>{
    return this.http.get<ListarIngressos>(`${this.baseUrlIngresso}Obter-Histórico-Por-Usuario`);
  }

  public getDisponiveis() : Observable<ListarIngressos>{
    return this.http.get<ListarIngressos>(`${this.baseUrlIngresso}Obter-Disponiveis`);
  }

  public CadastrarIngresso(body: NovoIngresso){
    return this.http.post(`${this.baseUrlIngresso}Cadastrar`, body);
  }

}
