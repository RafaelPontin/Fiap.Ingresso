import { jwtDecode } from 'jwt-decode';
import { LoginUser } from './../models/user/loginUser';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastroUser } from '../models/user/cadastroUser';
import { BehaviorSubject, Observable, ReplaySubject, map, take, tap } from 'rxjs';
import { RetornoUser } from '../models/user/retornoUser';
import { TokenService } from './token.service';
import { DecodificaJwt } from '../models/jwt/decodificaJwt';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSubject = new ReplaySubject<DecodificaJwt | null>(1);
  user$ = this.userSubject.asObservable();

  baseURL = 'https://localhost:7142/';

  constructor(private http: HttpClient, private tokenService: TokenService) {
    if(this.tokenService.possuiToken()) {
      this.decodificarJWT();
    }
   }

  public registration(cadastroUser: CadastroUser) {
    return this.http.post(`${this.baseURL}Criar-Usuario`, cadastroUser)
  }

  public login(model: LoginUser) {
      return this.http.post<RetornoUser>(this.baseURL+'Login', model).pipe(tap((response) => {
      const authToken = response.data || '';
      this.tokenService.salvarToken(authToken);
      this.decodificarJWT();
    }));
  }

  estaLogado() {
    return this.tokenService.possuiToken();
  }

  private decodificarJWT() {
    const token = this.tokenService.retornarToken();
    const user = jwtDecode(token) as DecodificaJwt;
    this.userSubject.next(user);
    console.log("decodificarjwt"+ user);
  }

  retornarUser() {
    return this.userSubject.asObservable();
  }

  // buscarCadastro(): Observable<User> {
  //   return this.http.get<User>(`${this.baseURL}/Buscar-Usuario`);
  // }

  // editarCadastro(pessoaUsuaria: User): Observable<User> {
  //   return this.http.patch<User>(`${this.baseURL}/Alterar-Usuario`, pessoaUsuaria);
  // }

  logout() {
    this.tokenService.excluirToken();
    this.userSubject.next(null);
  }

}
