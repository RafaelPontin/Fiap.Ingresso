import { CadastroPagamento } from './../../models/pagamento/CadastroPagamento';
import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-pagamento',
  templateUrl: './pagamento.component.html',
  styleUrl: './pagamento.component.scss'
})
export class PagamentoComponent {

   pagamento: CadastroPagamento = {
    IngressoId : "",
    TipoPagamento: 1,
    ValorPagamento: 0 ,
    CodigoVerificador: "",
    NomeCartao: "",
    NumeroCartao: "",
    VencimentoCartao: ""
   };

   habilitaCartao: boolean = true;

   selectionMetodoPagament(event: any) : void{
     this.habilitaCartao = this.pagamento.TipoPagamento == 1;
     if(this.pagamento.TipoPagamento == 1)
      {
        console.log('teste')
        this.pagamento.NumeroCartao = "";
        this.pagamento.NomeCartao = "";
        this.pagamento.VencimentoCartao = "";
        this.pagamento.CodigoVerificador = "";
     }
   }


   fazerPagamento(event: any) : void {

   }


}
