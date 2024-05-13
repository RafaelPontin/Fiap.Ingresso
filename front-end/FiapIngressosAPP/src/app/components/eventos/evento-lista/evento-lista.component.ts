import { ActivatedRoute, Router } from '@angular/router';
import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { ListarEventos } from '../../../models/evento/ListarEventos';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent {

  admin:boolean = true; // Definindo a variável admin como false
  eventoList: any[] = []; // Definindo o tipo de eventoList como array de objetos

  constructor(private eventoService: EventoService, private router : Router, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos() {
    this.eventoService.get().subscribe((data: ListarEventos) => {
      console.log(data);
      this.eventoList = data.data;
    });
  }

  public editarEvento(id: number): void {
    this.router.navigate([`eventos/editar/${id}`]);
  }

  public novoEvento(): void {
    this.router.navigate(['eventos/novo']);
  }

}
