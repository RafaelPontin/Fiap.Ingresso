import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { CadastraEvento } from '../../../models/evento/CadastraEvento';

@Component({
  selector: 'app-novo-evento',
  templateUrl: './novo-evento.component.html',
  styleUrl: './novo-evento.component.scss'
})
export class NovoEventoComponent {

  novoEvento: CadastraEvento = { // Use a interface para tipar novoEvento
    Nome: '',
    DataInicio: new Date(),
    DataFim: new Date(),
    DataEvento: new Date(),
    PublicoMaximo: 0,
    Ativo: 0,
    Logradouro: '',
    Numero: '',
    Cidade: '',
    Bairro: '',
    Cep: '',
    Descricao: '',
    SiteEvento: '',
    Valor: 0
  };

  constructor(private eventoService: EventoService) { }

  ngOnInit(): void {
  }

  criarEvento() {
    this.eventoService.post(this.novoEvento).subscribe((data: any) => {
      console.log('Evento criado com sucesso:', data);
      // Limpar o formulário ou realizar outra ação após a criação do evento
      this.novoEvento = {
        Nome: '',
        DataInicio: new Date(),
        DataFim: new Date(),
        DataEvento: new Date(),
        PublicoMaximo: 0,
        Ativo: 0,
        Logradouro: '',
        Numero: '',
        Cidade: '',
        Bairro: '',
        Cep: '',
        Descricao: '',
        SiteEvento: '',
        Valor: 0
      };
    }, error => {
      console.error('Erro ao criar evento:', error);
    });
  }

}
