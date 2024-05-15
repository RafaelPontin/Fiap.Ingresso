import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { NavComponent } from './shared/nav/nav.component';
import { LoginComponent } from './components/user/login/login.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { JwtInterceptor } from './interceptors/jwt.interceptor';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { PagamentoComponent } from './components/pagamento/pagamento.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { NovoEventoComponent } from './components/eventos/novo-evento/novo-evento.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IngressosComponent } from './components/user/ingressos/ingressos.component';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    NavComponent,
    LoginComponent,
    EventosComponent,
    EventoListaComponent,
    NovoEventoComponent,
    RegistrationComponent,
    PerfilComponent,
    NovoEventoComponent,
    PagamentoComponent,
    IngressosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 4000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    BrowserAnimationsModule,
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
