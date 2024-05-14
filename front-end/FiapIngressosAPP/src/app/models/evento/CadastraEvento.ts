export interface CadastraEvento {

    nome : string;
    dataInicio : Date;
    dataFim : Date;
    dataEvento : Date;
    publicoMaximo : number;
    ativo : number;
    logradouro : string;
    numero : string;
    cidade : string;
    bairro : string;
    cep : string;
    descricao? : string;
    siteEvento? : string;
    valor : number;

}
