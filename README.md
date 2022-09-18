# DogBreedsAPI


Projeto final do módulo de Programação Web III.


## Teconologia utilizada

- .NET 6.0

- ASP.NET Core Web API

## Nuget Package utilizado


- Microsoft.AspNetCore.Authentication.JwtBearer 6.0.9

## O que foi implementado nessa aplicação?

- Geração do JWT a partir do login do usuário;
- Jwt utilizado nos processos de Autenticação e Autorização de acesso as funcionalidades da API;
- Filtro customizado para registro de Logs;
- Separação específica das funcionalidades entre Controller/Model/Repository;
- Cada endpoint tem a declaração de retornos HTTP possíveis para eles;
- Foi utilizado um Json para simular o banco de dados.

## O que é necessário fazer para acessar as funcionalidades da API?

- É necessário logar através do POST da rota AthenticateLogin e fazer a autenticação para que possa acessar as outras funcionalidades.

OBS: Os dados de acesso do administrador podem ser encontrados no appsettings.json. Colocar essas informações cruciais nesse arquivo NÃO É uma boa prática, mas foi disponibilizado dessa forma apenas para fins didáticos.

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
