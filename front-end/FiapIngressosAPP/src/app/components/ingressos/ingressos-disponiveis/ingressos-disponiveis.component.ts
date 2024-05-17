import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ListarEventos } from '../../../models/evento/ListarEventos';
import { ListarIngressos } from '../../../models/ingresso/ListarIngressos';
import { EventoService } from '../../../services/evento.service';
import { IngressoService } from '../../../services/ingresso.service';
import { TokenService } from '../../../services/token.service';
import { DadosEventos } from '../../../models/evento/DadosEventos';
import { Ingressos } from '../../../models/ingresso/Ingressos';


@Component({
  selector: 'app-ingressos-disponiveis',
  templateUrl: './ingressos-disponiveis.component.html',
  styleUrl: './ingressos-disponiveis.component.scss'
})
export class IngressosDisponiveisComponent {
  ingressoList: Ingressos[] = [];

  constructor(private eventoService: EventoService, private ingressoService: IngressoService, private router : Router, private route: ActivatedRoute, public tokenService : TokenService)
  {
  }

  ngOnInit(): void {
    this.getIngressosDisponiveis();
  }

  public getIngressosDisponiveis() {
    this.ingressoService.getDisponiveis().subscribe((data: ListarIngressos) => {
      this.ingressoList = data.data as Ingressos[];
      this.getDadosEventoIngresso(data.data as Ingressos[]);
    });
  }

  public getDadosEventoIngresso(ingressos: Ingressos[]) {
      if(ingressos == null) return;
      ingressos.map((ingresso) => {
        this.eventoService.getById(ingresso.eventoId).subscribe((data: ListarEventos) => {
          var evento = data.data as DadosEventos;
          ingresso.evento = evento;
        })})
  };

  public pagamento(id: string) : void{
    this.router.navigate([`pagamento/${id}`]);
  }
}
