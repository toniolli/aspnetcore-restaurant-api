# 🍽️ Triad API

API REST desenvolvida em ASP.NET Core para gerenciamento de restaurantes.

---

## 📋 Funcionalidades

### 🔒 Segurança

- Autenticação JWT
- ASP.NET Identity
- Controle de usuários
- Controle de perfis
- Controle de permissões
- RBAC (Role Based Access Control)
- Claims customizadas
- Policies dinâmicas
- Middleware global para tratamento de exceções

### 👥 Usuários

- Cadastro de usuários
- Login
- Alteração de senha
- Exclusão de usuários
- Associação de usuários a perfis

### 🛡 Perfis e Permissões

- Cadastro de perfis
- Cadastro de permissões
- Associação Perfil → Permissões
- Associação Usuário → Perfis
- Controle de acesso baseado em permissões

### 🍽 Operação do Restaurante

- Gestão de mesas
- Gestão de pedidos
- Gestão de itens do pedido
- Controle de cardápio
- Gestão de setores de produção
- Controle de ocupação e liberação de mesas

---

## 🛠 Tecnologias Utilizadas

- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL
- ASP.NET Identity
- JWT Bearer Authentication
- AutoMapper
- Swagger / OpenAPI
- xUnit
- Moq

---

## 🏗 Arquitetura

A aplicação foi organizada utilizando separação de responsabilidades em camadas.

```text
Triad
│
├── Application
├── Domain
├── Infra
├── InfraIOC
├── Triad (API)
└── Tests
```

### Domain

Responsável por:

- Entidades
- Enums
- Regras de negócio
- Validações

### Application

Responsável por:

- DTOs
- Interfaces
- Services
- AutoMapper
- Casos de uso

### Infra

Responsável por:

- Repositórios
- Entity Framework Core
- Contextos
- Identity
- Persistência de dados

### API

Responsável por:

- Controllers
- Autenticação
- Autorização
- Middlewares
- Swagger

### Tests

Responsável por:

- Testes de Entidades
- Testes de Services
- Testes de Controllers

---

## 🔐 Segurança

A API utiliza autenticação baseada em JWT e autorização baseada em permissões.

### Fluxo de autorização

```text
Usuário
    ↓
UsuarioPerfil
    ↓
Perfil
    ↓
PerfilPermissao
    ↓
Permissão
    ↓
JWT
    ↓
Claims
    ↓
Policies Dinâmicas
    ↓
Endpoint
```

### Exemplo de uso

```csharp
[Authorize]
[Permissao("GERENCIAR_USUARIOS")]
```

Múltiplas permissões:

```csharp
[Authorize]
[Permissao(
    "GERENCIAR_PEDIDOS",
    "ACESSAR_COZINHA",
    "ACESSAR_BAR")]
```

---

## 📚 Principais Módulos

### Usuários e Segurança

```text
Usuario
UsuarioPerfil
Perfil
Permissao
PerfilPermissao
```

### Operação do Restaurante

```text
Mesa
Pedido
ItemPedido
Cardapio
SetorProducao
```

---

## 🧪 Testes

O projeto possui testes automatizados para:

- Entidades
- Regras de Negócio
- Services
- Controllers

---

## 🚀 Executando Localmente

### Pré-requisitos

- .NET 8 SDK
- PostgreSQL

### Restaurar dependências

```bash
dotnet restore
```

### Compilar projeto

```bash
dotnet build
```

### Executar aplicação

```bash
dotnet run
```

---

## 🐳 Docker

O projeto possui suporte para execução através de containers Docker.

### Pré-requisitos

- Docker Desktop
- Docker Compose

### Executar com Docker

```bash
docker compose up
```

Ou em segundo plano:

```bash
docker compose up -d
```

### Parar os containers

```bash
docker compose down
```

### Ver logs

```bash
docker compose logs
```

### Serviços

Ao iniciar os containers serão disponibilizados:

```text
Triad API
PostgreSQL
```

---

## 📖 Swagger

Após iniciar a aplicação:

```text
http://localhost:8080/swagger
```

---

## 🗄 Banco de Dados

Banco utilizado:

```text
PostgreSQL
```

ORM:

```text
Entity Framework Core
```

---

## 🎯 Objetivos do Projeto

Este projeto foi desenvolvido com o objetivo de aprofundar conhecimentos em:

- ASP.NET Core
- Clean Architecture
- Entity Framework Core
- ASP.NET Identity
- JWT
- RBAC
- Docker
- Testes Automatizados
- Boas práticas de desenvolvimento backend

---

## 🔄 Próximos Passos

- CI/CD
- Testes de Integração
- Cache
- Deploy
- Observabilidade e Logs

---

## 👨‍💻 Autor

Lucas Toniolli


GitHub: https://github.com/toniolli
LinkedIn: https://www.linkedin.com/in/lucas-toniolli-76479860/

---

Projeto desenvolvido para estudo e evolução de habilidades em desenvolvimento backend com ASP.NET Core.