import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ListarEventos } from '../models/evento/ListarEventos';
import { CadastraEvento } from '../models/evento/CadastraEvento';
import { DadosEventos } from '../models/evento/DadosEventos';

@Injectable({
  providedIn: 'root'
})
export class EventoService {


  baseURL = 'https://localhost:7128/';

  constructor(private http: HttpClient) { }

  public get() : Observable<ListarEventos>{
    return this.http.get<ListarEventos>(`${this.baseURL}Listar`)
  }

  public post(cadastraEvento: CadastraEvento) {
    return this.http.post(`${this.baseURL}Criar-Evento`, cadastraEvento)
  }

  public getById(id: string) : Observable<ListarEventos>{
    return this.http.get<ListarEventos>(`${this.baseURL}Evento/${id}`)
  }

  public put(evento: DadosEventos) {
    return this.http.put(`${this.baseURL}Alterar-Evento`, evento)
  }
}
