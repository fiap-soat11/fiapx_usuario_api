# WebApi
Reposiório destinado à api da lanchonete

## Instruções para construir a imagem Docker da aplicação

Acesse a pasta "fiapx_usuario" e execute o comando abaixo:

```bash
docker build -t fiapx_usuario.
```

## Instruções para construir a imagem Docker do banco de dados

Acesse a pasta "mysql" e execute o comando abaixo:

```bash
docker build -t fiapx_usuario-db.
```

## Instruções para executar com Docker Compose

1. Certifique-se de ter um arquivo `docker-compose.yml` na raiz do projeto.
2. Execute o comando abaixo para subir os serviços:

```bash
docker-compose up
```

3. Para rodar em segundo plano (modo detached):

```bash
docker-compose up -d
```

4. Para parar os serviços:

```bash
docker-compose down
```

# 🚀 FIAPX - Microserviço de Usuários e Autenticação

[![Build Status](https://github.com/fiap-soat11/fiapx_usuario_api/workflows/Build%20and%20Deploy%20Usuario%20API/badge.svg)](https://github.com/fiap-soat11/fiapx_usuario_api/actions)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=fiap-soat11_fiapx_usuario_api&metric=alert_status)](https://sonarcloud.io/dashboard?id=fiap-soat11_fiapx_usuario_api)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=fiap-soat11_fiapx_usuario_api&metric=coverage)](https://sonarcloud.io/dashboard?id=fiap-soat11_fiapx_usuario_api)

Microserviço responsável pelo gerenciamento de usuários e autenticação JWT para o sistema FIAPX - Pós-graduação em Arquitetura de Software FIAP SOAT11.

## 📋 Índice

- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Pré-requisitos](#-pré-requisitos)
- [Configuração Local](#-configuração-local)
- [Docker](#-docker)
- [Endpoints da API](#-endpoints-da-api)
- [Autenticação](#-autenticação)
- [Testes](#-testes)
- [CI/CD](#-cicd)
- [Segurança](#-segurança)

---

## 🏗️ Arquitetura

Este projeto segue os princípios de **Clean Architecture** com separação clara de responsabilidades:

### Padrões Implementados

- ✅ **Clean Architecture** - Separação de camadas
- ✅ **CQRS** - Commands e Queries (preparado para MediatR)
- ✅ **Repository Pattern** - Abstração de acesso a dados
- ✅ **Dependency Injection** - Inversão de controle
- ✅ **FluentValidation** - Validação de entrada
- ✅ **JWT Authentication** - Autenticação stateless

---

## 🛠️ Tecnologias

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| .NET | 8.0 | Framework principal |
| MySQL | 8.0 | Banco de dados |
| Entity Framework Core | 8.0 | ORM |
| JWT Bearer | 8.0 | Autenticação |
| BCrypt.Net | Latest | Hash de senhas |
| FluentValidation | 11.x | Validação |
| Swagger/OpenAPI | 3.0 | Documentação |
| Docker | Latest | Containerização |
| GitHub Actions | - | CI/CD |
| AWS ECR | - | Registro de imagens |

---

## 📦 Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [MySQL 8.0](https://dev.mysql.com/downloads/) (ou via Docker)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)

---

## ⚙️ Configuração Local

### 1. Clone o repositório

```bash
git clone https://github.com/fiap-soat11/fiapx_usuario_api.git
```

### 2. Acesse a pasta do projeto

```bash
cd fiapx_usuario_api
```

### 3. Crie um arquivo `.env` na raiz do projeto a partir do arquivo `.env.example`

```bash
cp .env.example .env
```

### 4. Configure as variáveis de ambiente no arquivo `.env`

```dotenv
JWT_SECRET=seu_secret_aqui
MYSQL_ROOT_PASSWORD=sua_senha_aqui
MYSQL_DATABASE=fiapx
MYSQL_USER=fiapx
MYSQL_PASSWORD=sua_senha_aqui
```

### 2. Configure o `appsettings.json`

```json
{
  "ConnectionStrings": {
   "DefaultConnection": "Server=localhost;Port=3336;Database=fiap_usuarios;Uid=user_fiap;Pwd=pass_fiap;"
  },
  "Jwt": {
    "Key": "mySuperSecretKey123!@#2026fiapSoat11Usuario",
    "Issuer": "fiapx_usuario_service",
    "Audience": "fiapx_usuario_client",
    "ExpirationInMinutes": 60
  },
}
```

### 5. Execute o Docker Compose

```bash
docker-compose up -d
```

### 6. Acesse a aplicação

A aplicação estará disponível em `http://localhost:5000`

---

## 📖 Endpoints da API

- `POST /api/usuarios` - Criar um novo usuário
- `GET /api/usuarios` - Obter a lista de usuários
- `GET /api/usuarios/{id}` - Obter os detalhes de um usuário
- `PUT /api/usuarios/{id}` - Atualizar um usuário
- `DELETE /api/usuarios/{id}` - Deletar um usuário
- `POST /api/auth/login` - Login e geração de token JWT
- `POST /api/auth/logout` - Logout e invalidação de token

---

## 🔐 Autenticação

A autenticação é feita através de tokens JWT. Um token deve ser enviado no header `Authorization` da seguinte forma:

```http
Authorization: Bearer seu_token_aqui
```

---

## 🧪 Testes

Para rodar os testes automatizados, execute o comando:

```bash
dotnet test
```

---

## CI/CD

O deploy é feito automaticamente através do GitHub Actions ao subir alterações na branch `main`. A imagem é criada e enviada para o AWS ECR.

---

## 🔒 Segurança

Este projeto se preocupa com a segurança dos dados e utiliza as melhores práticas para proteção de informações sensíveis, como senhas e tokens. Além disso, a comunicação com o banco de dados é feita de forma segura utilizando SSL/TLS.

---

## 🎓 Link Útil

- [Documentação do Swagger](http://localhost:5000/swagger/index.html)

---

## 📞 Contato

- **LinkedIn**: [Seu Nome](https://www.linkedin.com/in/seu-nome/)
- **Email**: seu.email@dominio.com

---

Feito com ❤️ pela turma FIAP SOAT11 - Pós-graduação em Arquitetura de Software FIAP
