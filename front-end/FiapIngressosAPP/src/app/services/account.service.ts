import { LoginUser } from './../models/user/loginUser';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastroUser } from '../models/user/cadastroUser';
import { ReplaySubject, tap } from 'rxjs';
import { RetornoUser } from '../models/user/retornoUser';
import { TokenService } from './token.service';
import { User } from '../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSubject = new ReplaySubject<User | null>(1);
  user$ = this.userSubject.asObservable();


  baseURL = 'https://localhost:8065/';

  constructor(private http: HttpClient, private tokenService: TokenService) {
    if(this.tokenService.possuiToken()) {
      this.recuperaUser();
    }
  }

  public registration(cadastroUser: CadastroUser) {
    return this.http.post(`${this.baseURL}Criar-Usuario`, cadastroUser)
  }

  public login(model: LoginUser) {
      return this.http.post<RetornoUser>(this.baseURL+'Login', model).pipe(tap((response) => {
      const user = response.data || '';
      if(user.accessToken !== null) {
        this.tokenService.salvarToken(user as User);
        this.userSubject.next(user as User);
      }
    }));
  }

  estaLogado() {
    return this.tokenService.possuiToken();
  }

  private recuperaUser() {
     const user = this.tokenService.retornarUser();
     this.userSubject.next(user as User);
  }

  logout() {
    this.tokenService.excluirToken();
    this.userSubject.next(null);
  }

}
