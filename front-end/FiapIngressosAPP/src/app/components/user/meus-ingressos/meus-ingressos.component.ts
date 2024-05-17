import { Component } from '@angular/core';
import { DadosEventos } from '../../../models/evento/DadosEventos';
import { ListarEventos } from '../../../models/evento/ListarEventos';
import { DadosIngresso } from '../../../models/ingresso/DadosIngresso';
import { ListarIngressos } from '../../../models/ingresso/ListarIngressos';
import { EventoService } from '../../../services/evento.service';
import { IngressoService } from '../../../services/ingresso.service';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-meus-ingressos',
  templateUrl: './meus-ingressos.component.html',
  styleUrl: './meus-ingressos.component.scss'
})
export class MeusIngressosComponent {
  ingressoList: DadosIngresso[] = [];

  constructor(
    private ingressoService: IngressoService,
    private eventoService: EventoService,
    public tokenService: TokenService
  ) { }

  ngOnInit(): void {
    this.getIngressos();
  }

  public getIngressos() {
    this.ingressoService.getHistoricoIngressoPorUsuario().subscribe(async (data: ListarIngressos) => {
      this.ingressoList = data.data as DadosIngresso[];
      this.getDadosEventoIngresso(data.data as DadosIngresso[]);
    });
  }

  public getDadosEventoIngresso(ingressos: DadosIngresso[]) {
      ingressos?.map((ingresso) => {
        this.eventoService.getById(ingresso.eventoId).subscribe((data: ListarEventos) => {
          var evento = data.data as DadosEventos;
          ingresso.evento = evento;
        })})
  };
}
