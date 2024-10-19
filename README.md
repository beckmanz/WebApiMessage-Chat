# Documentação da API de Message Chat

Este documento descreve a API de um sistema de chat simples, permitindo o gerenciamento de usuários, envio de mensagens, controle de amizades, solicitações de amizade, e bloqueios.

## Índice

- [Introdução](#introdução)
- [Instalação](#instalação)
- [Autenticação](#autenticação)
- [Fluxo de Autenticação](#fluxo-de-autenticação)
- [Endpoints da Aplicação](#endpoints-da-aplicação)
  - [User](#user)
  - [Friend](#friend)
  - [Request](#request)
  - [Message](#message)
  - [Blocked](#blocked)
- [Tratamento de Erros](#tratamento-de-erros)
- [Como Usar a API](#como-usar-a-api)
- [Contribuindo](#contribuindo)
- [Licença](#licença)

---

## Introdução

Esta API foi projetada para gerenciar chats entre usuários. O sistema permite funcionalidades como cadastro, login, envio de mensagens, gerenciamento de amizades, controle de solicitações e bloqueio de usuários.

## Instalação

1. Clone o repositório:
    ```
    git clone https://github.com/usuario/repo-message-chat.git
    ```

2. Navegue até a pasta do projeto e instale as dependências:
    ```
    cd repo-message-chat
    dotnet restore
    ```

3. Configure o banco de dados no arquivo `appsettings.json` e aplique as migrações:
    ```
    dotnet ef database update
    ```

4. Execute o projeto:
    ```
    dotnet run
    ```

## Autenticação

A autenticação é feita por meio de JWT (JSON Web Token). Ao efetuar o login, o usuário recebe um token, que deve ser enviado nas requisições a endpoints protegidos.

### Fluxo de Autenticação

1. **Registro:** O usuário se registra no endpoint `/api/user/register`.
2. **Login:** O usuário realiza login no endpoint `/api/user/login` e recebe um token JWT.
3. **Token JWT:** Esse token deve ser incluído no cabeçalho de todas as requisições subsequentes:
   ```
   Authorization: Bearer {token}
   ```
4. **Expiração:** O token expira após um tempo determinado e precisa ser renovado via login.

## Endpoints da Aplicação

### User

| Método | Endpoint            | Descrição                                 |
|--------|---------------------|-------------------------------------------|
| POST   | /api/user/register   | Registra um novo usuário                  |
| POST   | /api/user/login      | Realiza o login do usuário                |
| GET    | /api/user            | Lista todos os usuários                   |
| GET    | /api/user/{id}       | Obtém os detalhes de um usuário específico|
| PUT    | /api/user/{id}       | Edita os dados de um usuário              |

### Friend

| Método | Endpoint               | Descrição                                         |
|--------|------------------------|---------------------------------------------------|
| GET    | /api/friend/{userId}    | Lista os amigos de um usuário específico          |
| POST   | /api/friend/add         | Adiciona uma nova amizade                         |

### Request

| Método | Endpoint               | Descrição                                           |
|--------|------------------------|-----------------------------------------------------|
| GET    | /api/request/{userId}   | Lista as solicitações pendentes de um usuário       |
| POST   | /api/request/send       | Envia uma solicitação de amizade                    |
| DELETE | /api/request/{id}       | Recusa ou remove uma solicitação de amizade         |

### Message

| Método | Endpoint                | Descrição                                           |
|--------|-------------------------|-----------------------------------------------------|
| GET    | /api/message/{userId}    | Lista todas as mensagens de um usuário específico   |
| POST   | /api/message/send        | Envia uma nova mensagem                             |

### Blocked

| Método | Endpoint                | Descrição                                           |
|--------|-------------------------|-----------------------------------------------------|
| GET    | /api/blocked/{userId}    | Lista os usuários bloqueados de um usuário          |
| POST   | /api/blocked/add         | Bloqueia um usuário                                 |
| DELETE | /api/blocked/{id}        | Remove um usuário da lista de bloqueados            |

## Tratamento de Erros

Os erros são retornados no formato JSON, incluindo o código de status HTTP, uma mensagem de erro e detalhes adicionais se aplicável. Por exemplo:

```json
{
    "status": 400,
    "mensagem": "Requisição inválida",
    "detalhes": "O campo 'email' é obrigatório"
}
```

Os principais códigos de erro incluem:

- **400 Bad Request:** Quando há erros de validação ou parâmetros faltando.
- **401 Unauthorized:** Quando o token JWT não é válido ou está ausente.
- **403 Forbidden:** Quando o usuário não tem permissão para acessar o recurso.
- **404 Not Found:** Quando o recurso solicitado não é encontrado.

## Como Usar a API

1. **Registro e Login**: Registre-se via `/api/user/register` e faça login em `/api/user/login` para obter o token JWT.
2. **Autenticação**: Use o token nas requisições aos endpoints protegidos.
3. **Gerenciamento**: Utilize os endpoints para gerenciar usuários, amigos, solicitações e bloqueios.

## Contribuindo

Contribuições são bem-vindas! Siga os passos abaixo para contribuir com o projeto:

1. Faça um fork do repositório.
2. Crie uma branch para sua funcionalidade (`git checkout -b feature/nova-funcionalidade`).
3. Faça o commit das suas alterações (`git commit -m 'Adiciona nova funcionalidade'`).
4. Envie suas alterações (`git push origin feature/nova-funcionalidade`).
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a licença MIT.
