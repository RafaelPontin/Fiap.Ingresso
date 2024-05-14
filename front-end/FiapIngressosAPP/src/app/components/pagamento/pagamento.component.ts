import { PagamentoService } from './../../services/pagamento.service';
import { CadastroPagamento } from './../../models/pagamento/CadastroPagamento';
import { Component, OnInit } from '@angular/core';
import { CompraIngresso } from '../../models/Ingresso/CompraIngresso';


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
    quantidade: 1, //todo: pegar da tela de evento
    usuarioId: "aacb5bac-f647-4151-b30b-eec7ea92469b"
   }


   //TODO: depois validar como pegar o evento utilizado
   idEvento: string = "75b76820-8c5b-4237-8b59-69b2c5fa842b";
   idIngresso: string = "";
   idPagameto: string = "";

   habilitaCartao: boolean = true;
   eventoValido: boolean = false;
   pagamentoSucesso: boolean = false;
   desabilitaBotao: boolean = false;
   habilitaMensagemAcordo: boolean = false;

   mensagemVenda: string = "";

   constructor(private service : PagamentoService) {
    this.getEventoValido();
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

      this.service.postCompraIngresso(this.vendaIngresso, this.pagamento.ingressoId)
                .subscribe((data: any) => { },
                error=> { })
   }

   public async gravaPagamento()
   {
      this.desabilitaBotao = true;
      this.pagamento.ingressoId = this.idIngresso;
      this.pagamento.valorPagamento = 200; //todo validar como pegar
      this.pagamento.tipoPagamento = Number(this.pagamento.tipoPagamento);

      let retorno: any;
      retorno = await this.service.postCadastraPagamento(this.pagamento).toPromise();
      if(retorno.status == 201)
      {
        this.idPagameto = retorno.data;
        this.pagamentoSucesso = true;
      }
   }

   public async getLinhaDigitavel(){
    let retorno: any;
    retorno = await this.service.getLinhaDigitavel(this.idPagameto).toPromise();
    if(retorno.status == 200){
      this.mensagemVenda = `Boleto gerado com sucesso: linha digitavel: \n ${retorno.data}`;
      this.habilitaMensagemAcordo = true;
    }
   }

   public getEventoValido(){
     this.service.getEventoValido(this.idEvento).subscribe((data : any) => {
        this.eventoValido = true;
        this.idIngresso = data.data.id
     }, error => {
        this.eventoValido = false;
     });
   }


}
