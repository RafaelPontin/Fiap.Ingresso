import { Component } from '@angular/core';
import { IngressoService } from '../../../services/ingresso.service';
import { EventoService } from '../../../services/evento.service';
import { TokenService } from '../../../services/token.service';
import { DadosIngresso } from '../../../models/ingresso/DadosIngresso';
import { ListarIngressos } from '../../../models/ingresso/ListarIngressos';
import { DadosEventos } from '../../../models/evento/DadosEventos';
import { ListarEventos } from '../../../models/evento/ListarEventos';

@Component({
  selector: 'app-ingressos',
  templateUrl: './ingressos.component.html',
  styleUrl: './ingressos.component.scss'
})
export class IngressosComponent {
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
      ingressos.map((ingresso) => {
        this.eventoService.getById(ingresso.eventoId).subscribe((data: ListarEventos) => {
          var evento = data.data as DadosEventos;
          ingresso.nomeEvento = evento.nome;
          ingresso.dataEvento = evento.dataEvento;
        })})
  };
}
