import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CompraIngresso } from '../models/ingresso/CompraIngresso';
import { Observable } from 'rxjs';
import { ListarIngressos } from '../models/ingresso/ListarIngressos';

@Injectable({
  providedIn: 'root'
})
export class IngressoService {

  baseUrlIngresso: string = "https://localhost:7186/";

  constructor(private http: HttpClient) { }

  public postCompraIngresso(body: CompraIngresso, ingressoId: string){
    return this.http.post(`${this.baseUrlIngresso}Comprar/${ingressoId}`, body);
  }

  public getEventoValido(idEvento: string){
    return this.http.get(`${this.baseUrlIngresso}Obter-Por-Evento/${idEvento}`);
  }

  public getHistoricoIngressoPorUsuario() : Observable<ListarIngressos>{
    return this.http.get<ListarIngressos>(`${this.baseUrlIngresso}Obter-Histórico-Por-Usuario`);
  }

}
