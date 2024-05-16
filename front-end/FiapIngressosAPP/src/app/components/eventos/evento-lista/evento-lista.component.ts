import { TokenService } from './../../../services/token.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { ListarEventos } from '../../../models/evento/ListarEventos';
import { DadosEventos } from '../../../models/evento/DadosEventos';
import { IngressoService } from '../../../services/ingresso.service';
import { ListarIngressos } from '../../../models/ingresso/ListarIngressos';
import { DadosIngresso } from '../../../models/ingresso/DadosIngresso';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent {

  eventoList: any[] = [];

  constructor(private eventoService: EventoService, private ingressoService: IngressoService, private router : Router, private route: ActivatedRoute, public tokenService : TokenService)
  {
  }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos()  {
    this.eventoService.get().subscribe((data: ListarEventos) => {
      this.eventoList = data.data as DadosEventos[];
      //this.getIngressoPorEvento(data.data as DadosEventos[]);
    });
  }

  // public getIngressoPorEvento(eventos: DadosEventos[]) {
  //   eventos.map((evento) => {
  //     this.ingressoService.getEventoValido(evento.id).subscribe((data: ListarIngressosDoEvento) => {
  //       var evento = data.data as DadosIngressoDoEvento;
  //       evento.nomeEvento = evento.nome;
  //     })})
  // };


  public editarEvento(id: number): void {
    this.router.navigate([`eventos/editar/${id}`]);
  }

  public novoEvento(): void {
    this.router.navigate(['eventos/novo']);
  }

  public pagamento(id: string) : void{
    this.router.navigate([`pagamento/${id}`]);
  }

}
