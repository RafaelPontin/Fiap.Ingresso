import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from '@angular/platform-browser';
import { ToastrModule } from "ngx-toastr";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { EventoListaComponent } from "./components/eventos/evento-lista/evento-lista.component";
import { EventosComponent } from "./components/eventos/eventos.component";
import { NovoEventoComponent } from "./components/eventos/novo-evento/novo-evento.component";
import { IngressosDisponiveisComponent } from "./components/ingressos/ingressos-disponiveis/ingressos-disponiveis.component";
import { IngressosComponent } from "./components/ingressos/ingressos.component";
import { PagamentoComponent } from "./components/pagamento/pagamento.component";
import { LoginComponent } from "./components/user/login/login.component";
import { MeusIngressosComponent } from "./components/user/meus-ingressos/meus-ingressos.component";
import { RegistrationComponent } from "./components/user/registration/registration.component";
import { UserComponent } from "./components/user/user.component";
import { JwtInterceptor } from "./interceptors/jwt.interceptor";
import { NavComponent } from "./shared/nav/nav.component";


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
    NovoEventoComponent,
    PagamentoComponent,
    IngressosDisponiveisComponent,
    MeusIngressosComponent,
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
