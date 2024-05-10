import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user/login/login.component';
import { EventoListaComponent } from './components/eventos/evento-lista/evento-lista.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  // {
  //   path: '',
  //   runGuardsAndResolvers: 'always',
  //   canActivate: [AuthGuard],
  //   children: [
  //     { path: 'user', redirectTo: 'user/perfil'},
  //     {
  //       path: 'user/perfil', component: PerfilComponent
  //     },
  //     {
  //       path: 'eventos', component: EventosComponent,
  //       children: [
  //         { path: 'detalhe/:id', component: EventoDetalheComponent },
  //         { path: 'detalhe', component: EventoDetalheComponent },
  //         { path: 'lista', component: EventoListaComponent },
  //       ],
  //     },
  //   ]
  // },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent },
      { path: 'registration', component: RegistrationComponent },
    ]
  },
  { path: 'home', component: EventoListaComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
