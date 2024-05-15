import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user/login/login.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { NovoEventoComponent } from './components/eventos/novo-evento/novo-evento.component';
import { EventosComponent } from './components/eventos/eventos.component';
import { AuthGuard } from './guard/auth.guard';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { PagamentoComponent } from './components/pagamento/pagamento.component';
import { IngressosComponent } from './components/user/ingressos/ingressos.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
     path: '',
     runGuardsAndResolvers: 'always',
     canActivate: [AuthGuard],
     children: [
       { path: 'user', redirectTo: 'user/perfil'},
       {
         path: 'user/perfil', component: PerfilComponent
       },
       {
         path: 'eventos', component: EventosComponent,
         children: [
          { path: 'pagamento', component: PagamentoComponent},
          { path: 'ingressos', component: IngressosComponent},
          { path: 'detalhe/:id', component: NovoEventoComponent },
          { path: 'detalhe', component: NovoEventoComponent },
         ],
       },
     ]
  },
  {
    path: 'eventos', component: EventosComponent,
    children: [
      { path: 'editar/:id', component: NovoEventoComponent },
      { path: 'novo', component: NovoEventoComponent },
      { path: 'lista', component: EventoListaComponent },
    ]
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },
  //{ path: 'home', component: EventoListaComponent },
  { path: '**', redirectTo: 'eventos/lista', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
