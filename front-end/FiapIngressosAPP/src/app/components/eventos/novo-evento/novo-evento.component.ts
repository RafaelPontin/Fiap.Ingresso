import { NovoIngresso } from './../../../models/ingresso/NovoIngresso';
import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { EventoService } from '../../../services/evento.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CadastraEvento } from '../../../models/evento/CadastraEvento';
import { ActivatedRoute } from '@angular/router';
import { ListarEventos } from '../../../models/evento/ListarEventos';
import { format } from 'date-fns';
import { ToastrService } from 'ngx-toastr';
import { IngressoService } from '../../../services/ingresso.service';
import { DadosEventos } from '../../../models/evento/DadosEventos';

@Component({
  selector: 'app-novo-evento',
  templateUrl: './novo-evento.component.html',
  styleUrls: ['./novo-evento.component.scss'], // Correção aqui
})
export class NovoEventoComponent {
  form!: FormGroup;
  novoEvento = {} as any;
  stateSave = 'post';
  novoIngresso = {} as NovoIngresso;

  constructor(
    private eventoService: EventoService,
    private ingressoService: IngressoService,
    private router: Router,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private toaster: ToastrService
  ) {}

  ngOnInit(): void {
    this.validation();
    this.loadEvento();
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

  salvarEvento() {
    if (this.form.valid) {
      if (this.stateSave === 'post') {
        this.novoEvento = this.form.value; // Movido para dentro da função
        this.eventoService.post(this.novoEvento).subscribe((data: ListarEventos ) => {
          if (data.status === 201) {
            const evento = data.data as string;
            this.cadastrarIngresso(evento);
            this.toaster.success('Evento criado com sucesso:', 'Sucesso');
            this.resetForm();
          } else {
            this.toaster.error('Erro ao criar evento!', 'Erro');
          }
        });
      } else {
        this.novoEvento = { id: this.novoEvento.id, ...this.form.value };
        this.eventoService.put(this.novoEvento).subscribe((data: any) => {
          if (data.status === 201) {
            this.toaster.success('Evento atualizado com sucesso:', 'Sucesso');
            this.router.navigate(['eventos/lista']);
            this.resetForm();
          } else {
            this.toaster.error('Erro ao atualizar evento!', 'Erro');
          }
        });
      }
    }
  }

  loadEvento(): void {
    const eventoIdParam = this.route.snapshot.paramMap.get('id');

    if (eventoIdParam !== null) {
      this.stateSave = 'put';
      this.eventoService
        .getById(eventoIdParam)
        .subscribe((data: ListarEventos) => {
          this.novoEvento = { ...data.data as DadosEventos};
          this.novoEvento.dataInicio = format(
            new Date(this.novoEvento.dataInicio),
            'yyyy-MM-dd'
          );
          this.novoEvento.dataFim = format(
            new Date(this.novoEvento.dataFim),
            'yyyy-MM-dd'
          );
          this.novoEvento.dataEvento = format(
            new Date(this.novoEvento.dataEvento),
            'yyyy-MM-dd'
          );
          this.form.patchValue(this.novoEvento);
        });
    }
  }

  cadastrarIngresso(id : string) {
    this.novoIngresso.dataFim = this.novoEvento.dataFim;
    this.novoIngresso.disponiveis = this.novoEvento.publicoMaximo;
    this.novoIngresso.eventoId = id;
    this.novoIngresso.preco = this.novoEvento.valor;
    this.novoIngresso.total = this.novoEvento.publicoMaximo;
    this.ingressoService
      .CadastrarIngresso(this.novoIngresso)
      .subscribe((data: any) => {
        if (data.status === 201) {
          this.router.navigate(['eventos/lista']);
        }
      });
  }

  resetForm() {
    this.form.reset();
  }
}
