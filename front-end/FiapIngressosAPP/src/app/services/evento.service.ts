import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastraEvento } from '../models/evento/CadastraEvento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'https://localhost:7128/';

  constructor(private http: HttpClient) { }

  public get() {
    return this.http.get(`${this.baseURL}Listar`)
  }

  public post(cadastraEvento: CadastraEvento) {
    return this.http.post(`${this.baseURL}Criar-Evento`, cadastraEvento)
  }

}
