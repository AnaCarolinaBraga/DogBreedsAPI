# DogBreedsAPI


Projeto final do módulo de Programação Web III, com tema livre.


## Teconologias utilizadas

- .NET 6.0

- ASP.NET Core Web API

- C#

- JSON

## Nuget Package utilizado


- Microsoft.AspNetCore.Authentication.JwtBearer 6.0.9

## Como essa aplicação foi implementada?

- A partir do login correto de um usuário é gerado um token JWT;

OBS: Os dados de acesso do administrador podem ser encontrados no `appsettings.json`. Colocar essas informações cruciais nesse arquivo **NÃO** é uma boa prática, mas foi disponibilizado dessa forma apenas para fins didáticos.

- Esse token JWT é utilizado nos processos de Autenticação e Autorização para acesso as funcionalidades da DogBreedsAPI;
- Sem um token autenticado, não é possível acessar as funcionalidades da DogBreedsAPI;
- Toda vez que um POST, PUT, PATCH ou DELETE da DogBreedsAPI for executado com sucesso, será gerado um log (foi utilizado um filtro customizado para geração desses Logs);
- Cada endpoint tem a declaração de retornos HTTP possíveis para eles, seguindo as práticas de programação;
- Para facilitar a execução da aplicação em outros computadores, foi utilizado um Json para simular as movimentações no banco de dados. Um outro projeto que utiliza banco de dados pode ser visto [aqui.](https://github.com/AnaCarolinaBraga/agenda-de-contatos-MVC)


## Gif demonstrativo da aplicação

### Overview

<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071046037004894209/1_02-03-17.gif">
</div>

### O que acontece quando tenta solicitar um endpoint para a API sem estar autenticado

<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071046399732498493/2_31.gif">
</div>

### Processo de autenticação - Primeiro usuário que não existe, depois um que existe e demonstração que pode acessar os endpoints quando logado

- - É necessário logar através do POST da rota AthenticateLogin e fazer a autenticação para que possa acessar as funcionalidades da DogBreedsAPI.

<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071054944536383568/3_36.gif">
</div>

### Demonstração do funcionamento de todos os endpoints

#### Pt.1
<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071049945563537438/4_30_pt_1.gif">
</div>

#### Pt.2
<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071050180943675412/4_30_pt_2.gif">
</div>

### Como ficou o registro de logs da demonstração anterior

<div align="center">
<img align="center" src="https://media.discordapp.net/attachments/951643233665044541/1071058517923991603/image.png?width=853&height=276">
</div>

##### Legendas
1- Post de uma nova raça

2- Put de uma raça ja existente (O Log de um Put de nova raça é igual ao Post de uma nova raça)

3- Patch de uma raça ja existente

4- Delete de uma raça pelo ID

## Endpoints

#### AuthenticateLogin
- POST: Para liberar acesso ao sistema, precisa fazer o login corretamente através desse endpoint.

#### DogBreeds
- GET: Um GET das raças de cachorros paginado.
- GET: Um GET de determinada raça através do ID.
- POST: Um POST paginado no qual você pode decidir a partir de filtros quais serão os resultados que irão aparecer. Usar os filtros é opcional. 
- POST: Um POST que adiciona uma raça nova de cachorro. Ela define o ID sozinho.
- PUT: Um PUT em que adiciona uma nova raça de cachorro, caso o ID dele não existe, ou atualiza uma raça de cachorro a partir de um ID já existente.
- PATCH: Um PATCH que atualiza apenas as características de determinada raça de cachorro de determinado ID.
- DELETE: Um DELETE que deleta uma raça de cachorro a partir de um ID.



###### As raças foram construídas segundo as informações desse site [aqui.](https://love.doghero.com.br/racas-de-cachorro/)
