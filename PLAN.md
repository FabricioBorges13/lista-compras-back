# Plano do Backend - Lista de Compras Compartilhada

## Stack Tecnológica
- **Framework**: .NET 8 (C#)
- **Banco**: SQLite (Entity Framework Core)
- **Auth**: ASP.NET Core Identity + Google OAuth
- **Real-time**: SignalR
- **API**: REST + WebSockets

---

## Estrutura do Projeto

```
src/
├── ListaCompras.Api/           # Projeto principal (Web API)
├── ListaCompras.Core/          # Domain entities, interfaces, DTOs
├── ListaCompras.Infrastructure/ # EF Core, Repositories, Services
└── ListaCompras.Tests/         # Testes unitários e integração
```

---

## Entidades (Domain)

### User
- Id, Email, Name, GoogleId, PictureUrl, CreatedAt

### ShoppingList
- Id, Name, OwnerId, CreatedAt, UpdatedAt

### ListShare
- Id, ListId, UserId, Permission (Read/Write), SharedAt

### Product
- Id, ListId, Name, Quantity (nullable), Price (nullable), CreatedAt, UpdatedAt

---

## Tarefas por Fase

### FASE 1: Setup Inicial
- [ ] 1.1 Criar solution e projetos (.NET 8)
- [ ] 1.2 Configurar EF Core com SQLite
- [ ] 1.3 Configurar Dependency Injection
- [ ] 1.4 Configurar Serilog para logging
- [ ] 1.5 Configurar CORS para frontend

### FASE 2: Autenticação (Google OAuth)
- [ ] 2.1 Configurar ASP.NET Core Identity
- [ ] 2.2 Adicionar Google Authentication
- [ ] 2.3 Criar endpoints: `/api/auth/login`, `/api/auth/me`, `/api/auth/logout`
- [ ] 2.4 Criar JWT token service
- [ ] 2.5 Middleware de autenticação JWT

### FASE 3: CRUD de Listas de Compras
- [ ] 3.1 Criar entidade ShoppingList + Migration
- [ ] 3.2 Repository pattern para ShoppingList
- [ ] 3.3 Service layer para lógica de negócio
- [ ] 3.4 Endpoints REST:
  - `POST /api/lists` - Criar lista (requer nome)
  - `GET /api/lists` - Listar minhas listas (owner + shared)
  - `GET /api/lists/{id}` - Obter lista com produtos
  - `PUT /api/lists/{id}` - Atualizar nome da lista
  - `DELETE /api/lists/{id}` - Deletar lista (apenas owner)

### FASE 4: Compartilhamento de Listas
- [ ] 4.1 Criar entidade ListShare + Migration
- [ ] 4.2 Endpoints de compartilhamento:
  - `POST /api/lists/{id}/share` - Compartilhar com email
  - `GET /api/lists/{id}/shares` - Ver compartilhamentos
  - `DELETE /api/lists/{id}/shares/{userId}` - Remover compartilhamento
  - `PUT /api/lists/{id}/shares/{userId}` - Alterar permissão (Read/Write)

### FASE 5: Produtos na Lista
- [ ] 5.1 Criar entidade Product + Migration
- [ ] 5.2 Endpoints de produtos:
  - `POST /api/lists/{listId}/products` - Adicionar produto
  - `GET /api/lists/{listId}/products` - Listar produtos
  - `PUT /api/lists/{listId}/products/{id}` - Atualizar produto (nome, qtd, preço)
  - `DELETE /api/lists/{listId}/products/{id}` - Remover produto
  - `PATCH /api/lists/{listId}/products/{id}/toggle` - Marcar/desmarcar comprado

### FASE 6: Real-time com SignalR
- [ ] 6.1 Configurar SignalR no projeto
- [ ] 6.2 Criar Hub `ShoppingListHub`
- [ ] 6.3 Groups por ListId (owner + shared users entram no group)
- [ ] 6.4 Eventos do servidor → cliente:
  - `ProductAdded`, `ProductUpdated`, `ProductRemoved`, `ProductToggled`
  - `ListUpdated`, `ListShared`, `ListUnshared`, `UserJoined`, `UserLeft`
- [ ] 6.5 Cliente envia ações via Hub (opcional, ou via REST + broadcast)

### FASE 7: Validações e Regras de Negócio
- [ ] 7.1 Apenas owner pode deletar lista
- [ ] 7.2 Apenas owner pode compartilhar/descompartilhar
- [ ] 7.3 Permission Write necessária para editar produtos
- [ ] 7.4 Validação: lista deve ter nome
- [ ] 7.5 Quantity e Price opcionais (nullable)

### FASE 8: Testes e Documentação
- [ ] 8.1 Testes unitários (Services)
- [ ] 8.2 Testes de integração (Endpoints + Auth)
- [ ] 8.3 Testes SignalR (connexão, groups, broadcast)
- [ ] 8.4 Swagger/OpenAPI configurado
- [ ] 8.5 README com instruções de setup

---

## Endpoints Resumo

| Método | Endpoint | Descrição | Auth |
|--------|----------|-----------|------|
| POST | `/api/auth/google` | Login com Google | Não |
| GET | `/api/auth/me` | Usuário atual | Sim |
| POST | `/api/lists` | Criar lista | Sim |
| GET | `/api/lists` | Minhas listas | Sim |
| GET | `/api/lists/{id}` | Detalhes da lista | Sim |
| PUT | `/api/lists/{id}` | Renomear lista | Owner |
| DELETE | `/api/lists/{id}` | Deletar lista | Owner |
| POST | `/api/lists/{id}/share` | Compartilhar | Owner |
| DELETE | `/api/lists/{id}/shares/{uid}` | Remover share | Owner |
| PUT | `/api/lists/{id}/shares/{uid}` | Atualizar permissão | Owner |
| POST | `/api/lists/{id}/products` | Add produto | Write |
| GET | `/api/lists/{id}/products` | Listar produtos | Read |
| PUT | `/api/lists/{id}/products/{pid}` | Atualizar produto | Write |
| DELETE | `/api/lists/{id}/products/{pid}` | Remover produto | Write |
| PATCH | `/api/lists/{id}/products/{pid}/toggle` | Toggle comprado | Write |

---

## SignalR Events

### Server → Client
```typescript
// Hub: ShoppingListHub
// Group: "list-{listId}"

interface ProductDto { id, name, quantity?, price?, isBought, createdAt }
interface ListDto { id, name, ownerId, products: ProductDto[] }

ProductAdded(listId, product)
ProductUpdated(listId, product)
ProductRemoved(listId, productId)
ProductToggled(listId, productId, isBought)
ListUpdated(listId, list)
ListShared(listId, share)
ListUnshared(listId, userId)
UserJoined(listId, user)
UserLeft(listId, userId)
```

### Client → Server (opcional - pode usar REST)
```typescript
JoinList(listId)
LeaveList(listId)
```

---

## Configuração Google OAuth (appsettings.json)
```json
{
  "Authentication": {
    "Google": {
      "ClientId": "xxx.apps.googleusercontent.com",
      "ClientSecret": "xxx"
    },
    "Jwt": {
      "Key": "sua-chave-secreta-muito-longa",
      "Issuer": "ListaCompras",
      "Audience": "ListaCompras.Client"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=lista_compras.db"
  }
}
```

---

## Ordem de Execução Recomendada

1. **Fase 1** → Base funcionando
2. **Fase 2** → Auth protegendo tudo
3. **Fase 3** → Listas básicas
4. **Fase 4** → Compartilhamento
5. **Fase 5** → Produtos
6. **Fase 6** → Real-time (diferencial)
7. **Fase 7** → Refinamento
8. **Fase 8** → Qualidade

---

## Próximos Passos Imediatos

1. Rodar: `dotnet new sln -n ListaCompras`
2. Criar 3 projetos: Api, Core, Infrastructure
3. Adicionar referências entre projetos
4. Instalar pacotes NuGet necessários