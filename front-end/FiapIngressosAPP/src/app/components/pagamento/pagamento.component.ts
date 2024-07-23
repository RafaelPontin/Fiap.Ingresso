import { Component, OnInit } from '@angular/core';
import { CompraIngresso } from '../../models/ingresso/compraIngresso';
import { ActivatedRoute, Route } from '@angular/router';
import { IngressoService } from '../../services/ingresso.service';
import { ListarIngressos } from '../../models/ingresso/listarIngressos';
import { Ingressos } from '../../models/ingresso/ingressos';
import { CadastroPagamento } from '../../models/pagamento/cadastroPagamento';
import { PagamentoService } from '../../services/pagamento.service';


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
   valorTotal: number = 0;
   quantidadeDisponivel: number = 0;

   constructor(private servicePagamento : PagamentoService, private serviceIngresso : IngressoService, private route: ActivatedRoute) {
    this.idEvento = this.route.snapshot.paramMap.get('id') ;
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
      this.getEventoValido();
      if(this.quantidadeDisponivel == 0)
      {
        this.mensagemVenda = "Ingressos esgotados!";
        this.habilitaMensagemAcordo = true;
        this.desabilitaBotao = true;
        return;
      }
      if(this.vendaIngresso.quantidade > this.quantidadeDisponivel)
      {
        this.mensagemVenda = "Quantidade de ingressos maior que a disponivel!";
        this.habilitaMensagemAcordo = true;
        this.desabilitaBotao = true;
        return;
      }

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
      this.pagamento.valorPagamento = this.valorTotal;
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
    if(this.idEvento != null)
    {
      this.serviceIngresso.getEventoValido(this.idEvento).subscribe((data : ListarIngressos) => {
        this.eventoValido = true;
        const ingressoback = data.data as Ingressos;
        this.idIngresso = ingressoback.id;
        this.valorEvento = ingressoback.preco;
        this.valorTotal = this.valorEvento;
        this.quantidadeDisponivel = ingressoback.disponiveis;
     }, error => {
        this.eventoValido = false;
     });
    }
   }

   private calcularValorTotal() {
    this.valorTotal = this.valorEvento * this.vendaIngresso.quantidade;
  }

   public aumentarQuantidade() {
    this.vendaIngresso.quantidade++;
    this.calcularValorTotal();
    }

  public diminuirQuantidade() {
    if(this.vendaIngresso.quantidade > 1)
    {
      this.vendaIngresso.quantidade--;
      this.calcularValorTotal();
    }
  }
}
