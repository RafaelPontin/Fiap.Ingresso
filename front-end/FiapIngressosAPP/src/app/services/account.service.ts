import { LoginUser } from './../models/user/loginUser';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CadastroUser } from '../models/user/cadastroUser';
import { Observable, ReplaySubject, map, take } from 'rxjs';
import { User } from '../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  public currentUser$ = this.currentUserSource.asObservable();

  baseURL = 'https://localhost:7128/';

  constructor(private http: HttpClient) { }

  public post(cadastroUser: CadastroUser) {
    return this.http.post(`${this.baseURL}Criar-Usuario`, cadastroUser)
  }

  public login(model: any):Observable<void>{
    return this.http.post<User>(this.baseURL+'login', model).pipe(take(1),map((response : User)=> {
      const user = response;
      if(user){
        this.setCurrentUser(user)
      }
    }));
  }

  logout():void{
    localStorage.removeItem('user');
    this.currentUserSource.next(null as any);
    this.currentUserSource.complete();
  }

  public setCurrentUser(user:User):void{
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user)
  }

}
