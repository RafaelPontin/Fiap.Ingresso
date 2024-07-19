import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListarEventos } from '../models/evento/listarEventos';
import { CadastraEvento } from '../models/evento/cadastraEvento';
import { DadosEventos } from '../models/evento/dadosEventos';

@Injectable({
  providedIn: 'root'
})
export class EventoService {


  baseUrlEvento = 'https://localhost:8066/';

  constructor(private http: HttpClient) { }

  public get() : Observable<ListarEventos>{
    return this.http.get<ListarEventos>(`${this.baseUrlEvento}Listar`)
  }

  public post(cadastraEvento: CadastraEvento) : Observable<ListarEventos> {
    return this.http.post<ListarEventos>(`${this.baseUrlEvento}Criar-Evento`, cadastraEvento)
  }

  public getById(id: string) : Observable<ListarEventos>{
    return this.http.get<ListarEventos>(`${this.baseUrlEvento}Evento/${id}`)
  }

  public put(evento: DadosEventos) {
    return this.http.put(`${this.baseUrlEvento}Alterar-Evento`, evento)
  }
}
