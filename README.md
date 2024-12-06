# CastroMotors API

Bem-vindo à **CastroMotors API**, uma aplicação RESTful projetada para gerenciar os principais recursos de um sistema de pedidos de veículos. A API permite gerenciar usuários, carros, categorias, marcas, pedidos e itens de pedidos (garagem). Este projeto é ideal para aplicações backend que necessitam de gerenciamento dinâmico de veículos e pedidos de forma estruturada e eficiente.

---

## Índice

1. [Descrição do Projeto](#descrição-do-projeto)
2. [Funcionalidades](#funcionalidades)
3. [Tecnologias Utilizadas](#tecnologias-utilizadas)
4. [Pré-requisitos](#pré-requisitos)
5. [UML](#UML)
6. [Instruções de Configuração e Execução](#instruções-de-configuração-e-execução)

---

## Descrição do Projeto

A **CastroMotors API** é uma API desenvolvida para realizar o gerenciamento de uma loja de veículos online. A aplicação foi desenvolvida em **ASP.NET Core** utilizando um banco de dados em memória (In-Memory Database) para simplificar o desenvolvimento e a execução local.

Esta API fornece funcionalidades essenciais para:

- Gerenciamento de usuários e seus pedidos.
- Criação, edição e exclusão de carros, categorias e marcas.
- Adicionar e remover itens da garagem do usuário.
- Realizar checkout dos pedidos.

---

## Funcionalidades

- **Usuários**
  - CRUD completo para usuários.
  - Gerenciamento de pedidos associados a cada usuário.

- **Carros**
  - CRUD completo para carros.
  - Associações com categorias e marcas.

- **Pedidos**
  - Adicionar itens à garagem do usuário.
  - Remover itens da garagem.
  - Finalizar o pedido (checkout).

- **Categorias e Marcas**
  - CRUD completo para categorias e marcas de carros.

---

## Tecnologias Utilizadas

- **ASP.NET Core** (Framework Backend)
- **In-Memory Database** (Banco de Dados Simulado)
- **Swagger** (Documentação Interativa da API)
- **C#** (Linguagem de Programação)

---

## Pré-requisitos

Antes de começar, certifique-se de ter o seguinte instalado em sua máquina:

- **.NET 6 SDK** ou superior ([Baixar aqui](https://dotnet.microsoft.com/download))
- **Visual Studio** ou **Visual Studio Code** com suporte a C#.
- Ferramentas como **Postman** ou **Insomnia** para testar a API.

---

## UML

![UML - CastroMotors](https://github.com/user-attachments/assets/d542b940-f365-4d75-8e5d-9118a83e2c17)

---

## Instruções de Configuração e Execução

### Passo 1: Clonar o Repositório

Clone o repositório para sua máquina local:

```bash
git clone https://github.com/LucasCastroo/castro-motors-api.git
cd castro-motors-api
```

### Passo 2: Restaurar as Dependências

Restaurar as dependências do projeto com o seguinte comando:

```bash
dotnet restore
```

### Passo 3: Compilar o Projeto

Compile o projeto para verificar se tudo está configurado corretamente:

```bash
dotnet build
```

### Passo 4: Executar a API

Inicie a API com o seguinte comando:

```bash
dotnet run
```

A aplicação será iniciada e estará disponível em http://localhost:5000.

### Passo 5: Acessar a Documentação Swagger

Abra um navegador e acesse:


```bash
http://localhost:5000/swagger
```

A documentação Swagger permite que você interaja com a API diretamente do navegador.

