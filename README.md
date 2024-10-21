# API de Message Chat

Este documento descreve a API de um sistema de chat simples, permitindo o gerenciamento de usuários, envio de mensagens, controle de amizades, solicitações de amizade, e bloqueios.

---
## Introdução

Esta API foi projetada para gerenciar chats entre usuários. O sistema permite funcionalidades como cadastro, login, envio de mensagens, gerenciamento de amizades, controle de solicitações e bloqueio de usuários.

## Instalação

1. Clone o repositório:
    ```
    https://github.com/beckmanz/WebApiMessage-Chat.git
    ```

2. Navegue até a pasta do projeto e instale as dependências:
    ```
    cd WebApiMessage-Chat
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

1. **Registro:** O usuário se registra no endpoint `/User/Registrar`.

   **Exemplo do Body da Requisição**:
   ```json
   {
       "Username": "usuario1",
       "Email": "usuario1@example.com",
       "Password": "SenhaForte123!"
   }
   ````
2. **Login:** O usuário realiza login no endpoint `/User/Login` e recebe um token JWT.

   **Exemplo do Body da Requisição**:
      ```json
      {
         "Email": "usuario1@example.com",
         "Password": "SenhaForte123!"
      }
      ```
3. **Token JWT:** Esse token deve ser incluído no cabeçalho de todas as requisições subsequentes:
   ```
   Authorization: Bearer {token}
   ```
4. **Expiração:** O token expira após um tempo determinado e precisa ser renovado via login.

## Endpoints da Aplicação

### User

| Método | Endpoint       | Descrição                                  |
|--------|----------------|--------------------------------------------|
| POST   | /User/register | Registra um novo usuário                   |
| POST   | /User/login    | Realiza o login do usuário                 |
| GET    | /User/Listar   | Lista todos os usuários                    |
| GET    | /User/{id}     | Obtém os detalhes de um usuário específico |
| PUT    | /User/Editar   | Edita os dados do usuário autenticado      |
| DELETE | /User/Excluir  | Excluir a conta do usuário autenticado     |

### Friend

| Método | Endpoint                         | Descrição                              |
|--------|----------------------------------|----------------------------------------|
| GET    | /Friend/Adicionar/{targetId}     | Envia uma solicitação de amizade       |
| POST   | /Friend/Amigos                   | Lista os amigos do usuário autenticado |
| DELETE | /Friend/Remover/{amigoId}        | Remover um amigo da lista de amizades  |

### Request

| Método | Endpoint                   | Descrição                                     |
|--------|----------------------------|-----------------------------------------------|
| GET    | /Request/Listar            | Lista as solicitações pendentes de um usuário |
| POST   | /Request/Aceitar/{amigoId} | Aceita uma solicitação de amizade             |
| DELETE | /Request/Recusar/{amigoId} | Recusa ou remove uma solicitação de amizade   |

### Message

| Método | Endpoint                     | Descrição                                       |
|--------|------------------------------|-------------------------------------------------|
| GET    | /Message/Listar              | Lista todas as mensagens do usuário autenticado |
| POST   | /Message/Enviar/ {userId}    | Envia uma nova mensagem                         |
| DELETE | /Message/Excluir/{messageId} | Exclui uma mensagem                             |

### Blocked

| Método | Endpoint                         | Descrição                                          |
|--------|----------------------------------|----------------------------------------------------|
| GET    | /Blocked/Listar                  | Lista os usuários bloqueados de um usuário         |
| POST   | /Blocked/Bloquear/{blockedId}    | Bloqueia um usuário                                |
| DELETE | /Blocked/Desbloquear/{blockedId} | Remove um usuário da lista de bloqueados           |

## Como Usar a API

1. **Registro e Login**: Registre-se via `/User/Registrar` e faça login em `/User/Login` para obter o token JWT.
2. **Autenticação**: Use o token nas requisições aos endpoints protegidos.
3. **Gerenciamento**: Utilize os endpoints para gerenciar usuários, amigos, solicitações, mensagens e bloqueios.

## Licença

Este projeto está licenciado sob a licença MIT.
