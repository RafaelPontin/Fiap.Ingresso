# Fiap.Ingresso

## Grupo de desenvolvimento

|Alunos| E-mail|
|------|-------|
|Antonio Andderson de Freitas Soares|andderson.freitas@gmail.com|
|Elielson do Nascimento Rodrigues|elielsonrj@hotmail.com|
|Rafael Faustino Magalhães Pontin|rfmpontin@gmail.com|
|Alexssander Ferreira do Nascimento|alexssanderferreira@hotmail.com|

## BackEnd: 

* Para inicializar o projeto, abra a solution : **Fiap.Ingresso.Backend.sln**
  
  ![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/59ba30c2-48a9-4370-ba10-10dcf1d904f3)

* Faça a execução das seguintes APIs
  
  * Fiap.Ingresso.Evento.API
  * Fiap.Ingresso.Ingresso.API
  * Fiap.Ingresso.Pagamento.API
  * Fiap.Ingresso.Usuario.API
    
![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/f7358d49-6a53-4bbb-9a85-87f726006f18)

**Obs: o processo inicializara varios migrations e seeds para um inicio mais transparente do projeto**
Finalizando o migration, sera criado esses bancos: 

![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/9d489322-c92f-4ef2-b982-ee4fa46faec3)

### Autenticação
 **Na criação do projeto, ja criamos 2 usuarios**
 * User: usuario@email.com / password: 123123 (usuario normal)
 * User: adm@adm.com / password: 123123 (usuario adminstrador)

## Testes

* Abra  "Gerenciador de testes" no visual studio
* Execute todos os testes
  
   ![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/fdd6f992-f4c2-4391-b658-844684b2a3a6)
* Aguarde todos os teste (pode demorar vai depender da maquina)

![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/628cd705-1abd-4333-80a9-ff9bee132066)
resultado esperado.


## Front End 

* Para o funcionamento e necessario ter um ambiente configurado com o node e angular
* Abra o visual studio code (ou outra ide que suporte javascript)
* Abra o diretorio: **..\front-end\FiapIngressosAPP**
* Execute o comando na raiz do projeto: npm install (necessario para instalar todas as dependecias do projeto) 
  ![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/0e8911e7-0e53-4d84-8137-64da14f91570)
* Execute o comando: ng serve (para executar a aplicação)
![image](https://github.com/RafaelPontin/Fiap.Ingresso/assets/16031920/6b910232-9b98-42ae-a4d2-99c8874304ff)
* abra no navegador de sua preferencia o endereço: http://localhost:4200/



## Criterios de aceite
* https://github.com/RafaelPontin/Fiap.Ingresso/wiki/Levantamento-de-Requisitos-e-Crit%C3%A9rios-de-Aceite

# Tech Challenge 5

## Back End

* Abra o prompt de comando da raiz do projeto back end.
* Instale o Docker e habilite o Kubernetes na sua maquina.
* Instale as seguintes pods e services (pode demorar um pouco)
* Valide se seu Docker esta logando no Docker hub.
* Build as seguintes imagens (caso prefirir modifique o nome do usuario na build da imagem, só lembre de modificar no Kubernetes):
  
```bash docker build -f Dockerfile.usuario -t alexssanderferreira/usuario-api . ```

```bash docker build -f Dockerfile.evento -t alexssanderferreira/evento-api . ```

```bash docker build -f Dockerfile.ingresso -t alexssanderferreira/ingresso-api . ```

```bash docker build -f Dockerfile.pagamento -t alexssanderferreira/pagamento-api . ```

* Execute o comando para dar o puch na imagem (valide seu login no Docker hub antes):

```bash docker docker push alexssanderferreira/usuario-api ```

```bash docker docker push alexssanderferreira/evento-api ```

```bash docker docker push alexssanderferreira/ingresso-api ``` 

```bash docker docker push alexssanderferreira/pagamento-api ```

*  Execute os Kubernetes com os seguintes comandos

```bash kubectl apply -f usuariodb-deployment.yaml ``` 

```bash kubectl apply -f usuariodb-service.yaml ```

```bash kubectl apply -f eventodb-deployment.yaml ```

```bash kubectl apply -f eventodb-service.yaml ```

```bash kubectl apply -f pagamentodb-deployment.yaml ```

```bash kubectl apply -f pagamentodb-service.yaml ```

```bash kubectl apply -f ingressodb-deployment.yaml ```

```bash kubectl apply -f ingressodb-service.yaml ```

```bash kubectl apply -f fiap-ingresso-evento-api-deployment.yaml ```

```bash kubectl apply -f fiap-ingresso-evento-api-service.yaml ```

```bash kubectl apply -f fiap-ingresso-usuario-api-deployment.yaml ```

```bash kubectl apply -f fiap-ingresso-usuario-api-service.yaml ```

```bash kubectl apply -f fiap-ingresso-pagamento-api-deployment.yaml ```

```bash kubectl apply -f fiap-ingresso-pagamento-api-service.yaml ```

```bash kubectl apply -f fiap-ingresso-ingresso-api-deployment.yaml ```

```bash kubectl apply -f fiap-ingresso-ingresso-api-service.yaml ```

## Front End

* Abra o prompt de comando da raiz do projeto front end.
* Instale o Docker e habilite o Kubernetes na sua maquina.
* Build as seguintes imagens (caso prefirir modifique o nome do usuario na build da imagem, só lembre de modificar no Kubernetes):

 ```bash docker build -t alexssanderferreira/front-ingressos . ```

* Execute o conteiner da aplicação:

```bash docker run -p 4201:4200 alexssanderferreira/front-ingressos ```

* Execute o comando para dar o puch na imagem (valide seu login no Docker hub antes):

```bash docker push alexssanderferreira/front-ingressos ```

* Build as seguintes imagens (caso prefirir modifique o nome do usuario na build da imagem, só lembre de modificar no Kubernetes):

```bash kubectl apply -f deployment.yaml ```

```bash kubectl apply -f service.yaml ```
