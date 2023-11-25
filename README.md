# E-Agenda Médica

O E-Agenda Médica é uma ASP.NET Core Web API desenvolvida com o objetivo de ser um projeto dedicado à gestão e organização do cronograma de uma clínica. Esta clínica, por sua vez, é um centro onde diversas atividades, como cirurgias e consultas, são realizadas.

Construído por Davi Felipe Borges - @daviborgesfelipe. [LinkedIn](https://www.linkedin.com/in/davi-borges-felipe/)

## Tecnologias

* **ASP.NET Core Web API** - Um framework que facilita a construção de serviços HTTP. [Site oficial](http://www.asp.net/web-api/overview/getting-started-with-aspnet-web-api/tutorial-your-first-web-api)
* **Entity Framework Core** - Um moderno mapeador objeto-banco de dados para .NET. Oferece suporte a consultas LINQ, rastreamento de alterações, atualizações e migrações de esquema. O EF Core é compatível com diversos bancos de dados, incluindo SQL Server, por meio de uma API de plugin do provedor. [Site oficial](https://learn.microsoft.com/pt-br/ef/core)
* **EntityFramework Core Tools** - Ferramentas para o NuGet Package Manager Console no Visual Studio, que habilitam comandos comumente utilizados.. [Site oficial](https://learn.microsoft.com/pt-br/ef/core)
* **FluentValidation** -  Um Framework de Validação gratuito e de código aberto. [Site oficial](https://fluentvalidation.net)
* **FluentResults** -  Um Framework de Validação gratuito e de código aberto. [Site oficial](https://www.nuget.org/packages/FluentResults/3.14.0#readme-body-tab)
* **AutoMapper** - Mediação entre as camadas de domínio e mapeamento de dados usando uma interface semelhante a uma coleção para acessar objetos de domínio.. [Site oficial](https://automapper.org)
* **Serilog** - Biblioteca de registro (logging) para .NET.. [Site oficial](https://serilog.net)
* **SequentialGuidGenerator** - Gera GUIDs sequenciais.. [Referência](https://www.nuget.org/packages/SequentialGuidGenerator/1.1.0)

## Organização do projeto

A organização do projeto segue uma arquitetura de camadas bem definida, o que facilita a manutenção, expansão e compreensão do sistema.

1. Camada de Distribuição
	
    * **Responsabilidade:** Esta camada é responsável pela distribuição de dados. Aqui, são desenvolvidos projetos que lidam com a distribuição de informações, como Web API e outros. 

	* **Objetivo:** Garantir que a comunicação entre diferentes partes do sistema seja eficiente e gerenciar a distribuição de dados conforme necessário.

2. Camada de Aplicação:
	
    * **Responsabilidade:** A Camada de Aplicação orquestra as chamadas que compõem uma feature específica. Ela define os Serviços de Orquestração, manipuladores e os formatos. É o coração da lógica de negócios da aplicação.

	* **Objetivo:** Separar a lógica de negócios da implementação técnica, garantindo uma estrutura organizada para as funcionalidades da aplicação.

3. Camada de Dominio
	
    * **Responsabilidade:** Aqui são desenvolvidos os objetos de negócio, que representam o domínio lógico da aplicação. Inclui também interfaces de acesso aos repositórios de dados, estabelecendo um contrato claro entre a lógica de negócios e a camada de infraestrutura e aplicação.

	* **Objetivo:** Isolar e encapsular as regras de negócio, garantindo que a lógica principal da aplicação seja independente da infraestrutura.

4. Camada de Infraestrutura
    
    * **Responsabilidade:** Desenvolvimento de objetos que controlam o acesso a dados, incluindo conexões com o banco de dados e comandos. Implementações de interfaces que definem o acesso aos bancos de dados estão contidas nesta camada.

	* **Objetivo:** Gerenciar a interação entre a aplicação e os recursos externos, garantindo a eficiência e a manutenção.
	   

## Dependências

As dependências do projeto back-end são gerenciadas com o Nuget.

As instruções a seguir assumem que você utiliza o **Windows** como o sistema operacional.

Para alterar ou executar a API é necessário o **Visual Studio 2022**.

### Instalar dependências do back-end

Ao abrir a solução do projeto existente na pasta **e-AgendaMedica.Server**, o Nuget é responsável por restaurar as dependências quando
o build da solution ocorrer. **Utilize a versão 5.11.0 do Nuget.**

* Esse projeto usa ASP.NET Core Web Api.
* No Visual Studio, faça um build na solution para instalar os pacotes do Nuget.

## Executar o projeto

* Na barra de ferramentas superior do **Visual Studio 2022**, na opcao **Startup Project** escolha o projeto **e-AgendaMedica.WebApi** e inicie a aplicação, o projeto irá iniciar no endereço **http://localhost:7186**

* Por padrão, a API irá abrir o **Swagger**, que dará toda a visão de funcionamento da API, bem como sua documentação.

## FIM

Em caso de dúvidas, entre em contato! [LinkedIn](https://www.linkedin.com/in/davi-borges-felipe/)
Obrigado.

