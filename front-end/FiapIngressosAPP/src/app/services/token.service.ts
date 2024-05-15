import { Injectable } from '@angular/core';
import { User } from '../models/user/user';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  salvarToken(user: User) {
    return localStorage.setItem("user", JSON.stringify(user) )
  }

  excluirToken() {
    localStorage.removeItem("user")
  }

  retornarUser(): User | null {
    const userString: string | null = localStorage.getItem('user');
    if (userString) {
      try {
        const user: User = JSON.parse(userString);
        return user;
      } catch (error) {
        console.error('Erro ao analisar o usuário do localStorage:', error);
        return null;
      }
    } else {
      return null;
    }
  }

  possuiToken() {
    return !!this.retornarUser();
  }


}
