import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenService } from '../services/token.service';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private tokenService: TokenService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if(this.tokenService.possuiToken()) {
      const user = this.tokenService.retornarUser();
      request = request.clone({
        setHeaders: {
          'Authorization': `Bearer ${user?.accessToken}`
        }
      })
    }
    return next.handle(request);
  }
}
