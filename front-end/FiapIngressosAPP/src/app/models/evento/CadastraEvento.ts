export interface CadastraEvento {

    Nome : string;
    DataInicio : Date;
    DataFim : Date;
    DataEvento : Date;
    PublicoMaximo : number;
    Ativo : number;
    Logradouro : string;
    Numero : string;
    Cidade : string;
    Bairro : string;
    Cep : string;
    Descricao? : string;
    SiteEvento? : string;
    Valor : number;

}
