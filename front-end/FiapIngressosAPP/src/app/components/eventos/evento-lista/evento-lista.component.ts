import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent {

  eventoList: any[] = []; // Definindo o tipo de eventoList como array de objetos

  constructor(private eventoService: EventoService) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos() {
    this.eventoService.get().subscribe((data: any) => {
      console.log(data); // Para verificar a resposta da API no console
      this.eventoList = data.result.data; // Atribuindo os dados recebidos à variável eventoList
    });
  }

}
