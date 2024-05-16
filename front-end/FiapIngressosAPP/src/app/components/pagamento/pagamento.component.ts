import { PagamentoService } from './../../services/pagamento.service';
import { CadastroPagamento } from './../../models/pagamento/CadastroPagamento';
import { Component, OnInit } from '@angular/core';
import { CompraIngresso } from '../../models/ingresso/CompraIngresso';
import { ActivatedRoute, Route } from '@angular/router';
import { EventoService } from '../../services/evento.service';
import { ListarEventos } from '../../models/evento/ListarEventos';
import { DadosEventos } from '../../models/evento/DadosEventos';
import { IngressoService } from '../../services/ingresso.service';


@Component({
  selector: 'app-pagamento',
  templateUrl: './pagamento.component.html',
  styleUrl: './pagamento.component.scss'
})
export class PagamentoComponent implements OnInit {

   pagamento: CadastroPagamento = {
    ingressoId : "",
    tipoPagamento: 1,
    valorPagamento: 0 ,
    codigoVerificador: "",
    nomeCartao: "",
    numeroCartao: "",
    vencimentoCartao: ""
   };


   vendaIngresso: CompraIngresso = {
    pagamentoId: "",
    quantidade: 1,
    usuarioId: "aacb5bac-f647-4151-b30b-eec7ea92469b"
   }



   idEvento: string | null = "";
   idIngresso: string = "";
   idPagameto: string = "";
   valorEvento: number = 0;

   habilitaCartao: boolean = true;
   eventoValido: boolean = false;
   pagamentoSucesso: boolean = false;
   desabilitaBotao: boolean = false;
   habilitaMensagemAcordo: boolean = false;

   mensagemVenda: string = "";

   constructor(private servicePagamento : PagamentoService, private serviceIngresso : IngressoService, private route: ActivatedRoute, private serviceEvento: EventoService ) {
    this.idEvento = this.route.snapshot.paramMap.get('id') ;
    this.getEventoValido();
    this.getValor();
  }

  ngOnInit(): void {

  }


   selectionMetodoPagament(event: any) : void{
     this.habilitaCartao = this.pagamento.tipoPagamento == 1;
     if(this.pagamento.tipoPagamento == 1)
      {
        this.pagamento.numeroCartao = "";
        this.pagamento.nomeCartao = "";
        this.pagamento.vencimentoCartao = "";
        this.pagamento.codigoVerificador = "";
     }
   }


   public async fazerPagamento(event: any)
   {

      if(this.eventoValido)
      {
        await this.gravaPagamento();

        if(this.pagamentoSucesso)
        {
          this.gravaIngresso(this.idPagameto);

          if(this.pagamento.tipoPagamento == 1)
          {
              await this.getLinhaDigitavel();
          }
          else
          {
             this.mensagemVenda = "Pagamento via cartão de credito feito com sucesso!";
             this.habilitaMensagemAcordo = true;
          }
        }
      }
   }


   public gravaIngresso(pagamentoId: string)
   {
      this.vendaIngresso.pagamentoId = pagamentoId;

      this.serviceIngresso.postCompraIngresso(this.vendaIngresso, this.pagamento.ingressoId)
                .subscribe((data: any) => { },
                error=> { })
   }

   public async gravaPagamento()
   {
      this.desabilitaBotao = true;
      this.pagamento.ingressoId = this.idIngresso;
      this.pagamento.valorPagamento = this.valorEvento;
      this.pagamento.tipoPagamento = Number(this.pagamento.tipoPagamento);

      let retorno: any;
      retorno = await this.servicePagamento.postCadastraPagamento(this.pagamento).toPromise();
      if(retorno.status == 201)
      {
        this.idPagameto = retorno.data;
        this.pagamentoSucesso = true;
      }
   }

   public async getLinhaDigitavel(){
    let retorno: any;
    retorno = await this.servicePagamento.getLinhaDigitavel(this.idPagameto).toPromise();
    if(retorno.status == 200){
      this.mensagemVenda = `Boleto gerado com sucesso: linha digitavel: \n ${retorno.data}`;
      this.habilitaMensagemAcordo = true;
    }
   }

   public getEventoValido(){
    console.log(`teste: ${this.idEvento}`);
    if(this.idEvento != null)
    {
      this.serviceIngresso.getEventoValido(this.idEvento).subscribe((data : any) => {
        this.eventoValido = true;
        this.idIngresso = data.data.id
     }, error => {
        this.eventoValido = false;
     });
    }
   }

   private getValor() {
    if(this.idEvento != null)
    {
      this.serviceEvento.getById(this.idEvento).subscribe((data: ListarEventos) => {
        const evento = data.data as DadosEventos;
        console.log(evento);
        this.valorEvento = evento.valor;
        console.log(this.valorEvento);
      }
      , error =>{});
    }
   }

}
