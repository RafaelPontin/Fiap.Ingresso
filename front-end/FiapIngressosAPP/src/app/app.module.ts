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
import { RegistrationComponent } from './components/user/registration/registration.component';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    NavComponent,
    LoginComponent,
    EventosComponent,
    EventoListaComponent,
    NovoEventoComponent,
    RegistrationComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
