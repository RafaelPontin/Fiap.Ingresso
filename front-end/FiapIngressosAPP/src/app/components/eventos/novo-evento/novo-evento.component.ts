import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CadastraEvento } from '../../../models/evento/CadastraEvento';
import { ActivatedRoute } from '@angular/router';
import { ListarEventos } from '../../../models/evento/ListarEventos';

@Component({
  selector: 'app-novo-evento',
  templateUrl: './novo-evento.component.html',
  styleUrls: ['./novo-evento.component.scss'] // Correção aqui
})
export class NovoEventoComponent {

  form!: FormGroup;
  novoEvento = {} as CadastraEvento;
  stateSave = 'post';

  constructor(private eventoService: EventoService, private formBuilder: FormBuilder, private route: ActivatedRoute) { }

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

  // loadEvento(): void {
  //   const eventoIdParam = this.route.snapshot.paramMap.get('id');

  //   if (eventoIdParam !== null) {
  //   this.stateSave = 'put';

  //        this.eventoService.getPersonById(eventoIdParam).subscribe(
  //          (evento: ListarEventos) => {
  //            this.evento = { ...evento };
  //            this.evento.nome = this.evento.nome;
  //            this.evento.dataInicio = this.evento.dataInicio;
  //            this.form.patchValue(this.evento);
  //          },
  //         (error) => console.log(error)
  //       );
  //      }
  //   }
  // }

  resetForm() {
    this.form.reset(); // Resetar o formulário usando o método reset do FormGroup
  }
}
