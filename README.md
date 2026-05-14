# 📦 Gestão de Pedidos API

API RESTful para simular o núcleo de um ecossistema de vendas, focada em boas práticas de arquitetura, persistência de dados complexa e qualidade de código. 

Este é o primeiro de uma série de 4 projetos do roadmap de estudos avançados em .NET, focado especificamente em **Arquitetura de Negócios e Relacionamentos (Entity Framework Core)**.

## 🚀 Tecnologias e Padrões Utilizados
* **.NET 8** (C#)
* **Entity Framework Core** (Code-First, Migrations)
* **PostgreSQL** (Containerizado via Docker)
* **Clean Architecture** (Separação estrita em Domain, Application, Infrastructure e API)
* **Domain-Driven Design (DDD)** (Entidades ricas, encapsulamento de regras de negócio)
* **xUnit** (Estrutura base preparada para TDD)

## 🧠 Desafios Técnicos Solucionados
Durante o desenvolvimento, esta API foi projetada para lidar com cenários reais de ambientes corporativos:
* **Gerenciamento de Estado do ORM:** Resolução de problemas complexos de concorrência (`409 Conflict`) manipulando diretamente o `ChangeTracker` e os `EntityStates` (Detached, Modified, Added) no repositório, garantindo transações limpas.
* **Segurança de Configuração:** Ocultação de credenciais via `appsettings.Development.json` e disponibilização de um arquivo `.Example.json` para facilitar o setup seguro do ambiente por outros desenvolvedores da equipe.
* **Prevenção de Ciclos de Objetos:** Mapeamento eficiente das entidades (`Include`) sem quebrar o serializador JSON da API.

## 🧪 Testes Unitários
O projeto utiliza a abordagem Test-Driven Development (TDD) para garantir a qualidade das regras de negócio (Domain). O framework de testes configurado é o xUnit.

Cobertura Principal de Testes (GestaoPedidos.Tests)

✅ Regra de Desconto: Valida se pedidos com valor total acima de R$ 1.000,00 recebem automaticamente 10% de desconto no momento do fechamento.

✅ Regra de Integridade: Assegura que um pedido lança exceção (falha) caso haja tentativa de fechá-lo sem conter nenhum item (ItemPedido).

✅ Regra de Transição de Status: Garante o fluxo correto do pedido, que nasce como Aberto e passa para Fechado apenas após validação de regras internas.

## ⚙️ Como executar o projeto localmente

### Pré-requisitos
* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Banco de Dados (PostgreSQL via Docker)
1. Certifique-se de que o Docker está em execução.
2. Na raiz do projeto, suba a infraestrutura do banco:

   ```bash
   docker-compose up -d


