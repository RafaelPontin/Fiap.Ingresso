import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../services/account.service';

export const AuthGuard = () => {
  const userService = inject(AccountService);
  const router = inject(Router);

  if(userService.estaLogado()) {
    return true;
  } else {
    router.navigate(['user/login']);
    return false;
  }
}
