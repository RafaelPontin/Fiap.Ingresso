import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './components/user/user.component';
import { NavComponent } from './shared/nav/nav.component';
import { LoginComponent } from './components/user/login/login.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { HttpClientModule } from '@angular/common/http';
import { NovoEventoComponent } from './components/eventos/novo-evento/novo-evento.component';
import { FormsModule } from '@angular/forms';
import { PagamentoComponent } from './components/pagamento/pagamento.component';
import {  NgxMaskModule } from 'ngx-mask';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    NavComponent,
    LoginComponent,
    EventosComponent,
    EventoListaComponent,
    NovoEventoComponent,
    PagamentoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgxMaskModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
