import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CadastraEvento } from '../../../models/evento/CadastraEvento';

@Component({
  selector: 'app-novo-evento',
  templateUrl: './novo-evento.component.html',
  styleUrls: ['./novo-evento.component.scss'] // Correção aqui
})
export class NovoEventoComponent {

  form!: FormGroup;
  novoEvento = {} as CadastraEvento;

  constructor(private eventoService: EventoService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this. validation();
  }

  private validation(): void {
    this.form = this.formBuilder.group({
      nome: [null, Validators.required],
      dataInicio: [null, Validators.required],
      dataFim: [null, Validators.required],
      dataEvento: [null, Validators.required],
      publicoMaximo: [null, Validators.required],
      ativo: [null, Validators.required],
      logradouro: [null, Validators.required],
      numero: [null, Validators.required],
      cidade: [null, Validators.required],
      bairro: [null, Validators.required],
      cep: [null, Validators.required],
      descricao: [null],
      siteEvento: [null],
      valor: [null, Validators.required],
    });
  }

  criarEvento() {
    this.novoEvento = this.form.value; // Movido para dentro da função
    this.eventoService.post(this.novoEvento).subscribe((data: any) => {
      console.log('Evento criado com sucesso:', data);
      this.resetForm();
    }, error => {
      console.error('Erro ao criar evento:', error);
    });
  }

  resetForm() {
    this.form.reset(); // Resetar o formulário usando o método reset do FormGroup
  }
}
