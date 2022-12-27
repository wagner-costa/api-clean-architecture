Esse repositório é uma implementação de uma Arquitetura Clean (Clean Architecture) usando .NET7.

O foco é ter um estudo de caso e uma arquitetura base onde você consiga entender a importância de ter uma aplicação desacoplada de frameworks e tecnologias de uma forma que seu software represente mais o contexto de negócio que você está inserido(a) e não uma implementação baseada em regras de negócios de banco de dados.

## Tecnologias
* .NET7
* Entity Framework Core
* AutoMapper
* FluentValidation

## Sobre Clean Architecture e suas responsabilidades

### Domínio (Domain)

Essa camada é responsável por todas as suas entidades, enumerações, exceções, abstrações (interfaces por exemplo), tipos e lógicas específicas ao seu domínio.

### Infraestrutura (Infrastructure)

Essa camada é responsável por conter classes que acessem recursos externos a nossa aplicação, como por exemplo web services, emails ou até mesmo acesso a disco. Essas classes devem implementar abstrações da camada de aplicação.

### Interface de Usuário (UI)

Essa camada é responsável pela interface de usuário, no caso desse projeto temos um exemplo simples utilizando Angular 8 e ASP.NET Core 3. Essa camada depende da aplicação e infraestrutura porém toda dependência que vier de infraestrutura é apenas para consumir injeção de dependências. 

## Suporte

Se de alguma forma o projeto foi útil para você ou sua empresa, dê uma estrela e siga o projeto!


