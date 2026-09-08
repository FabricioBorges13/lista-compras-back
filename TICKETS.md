# Tickets de Implementação - Lista de Compras Backend

Cada ticket pode ser implementado e testado independentemente. Ordem sugerida mantém dependências.

---

## 📦 FASE 1: Setup Inicial

### TICKET-001: Criar Solution e Estrutura de Projetos
- [ ] `dotnet new sln -n ListaCompras`
- [ ] `dotnet new webapi -n ListaCompras.Api -o src/ListaCompras.Api`
- [ ] `dotnet new classlib -n ListaCompras.Core -o src/ListaCompras.Core`
- [ ] `dotnet new classlib -n ListaCompras.Infrastructure -o src/ListaCompras.Infrastructure`
- [ ] `dotnet new xunit -n ListaCompras.Tests -o src/ListaCompras.Tests`
- [ ] Adicionar projetos à solution
- [ ] Configurar referências: Api → Core, Api → Infrastructure, Infrastructure → Core, Tests → todos

**Critério de aceite**: `dotnet build` passa sem erros

---

### TICKET-002: Configurar EF Core + SQLite
- [ ] Instalar pacotes: `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`
- [ ] Criar `AppDbContext` em Infrastructure
- [ ] Configurar connection string no `appsettings.json`
- [ ] Registrar DbContext no DI (Program.cs)
- [ ] Criar migration inicial (pode ser vazia)
- [ ] `dotnet ef database update`

**Critério de aceite**: Banco criado, migration aplicada

---

### TICKET-003: Configurar DI, Logging e CORS
- [ ] Instalar: `Serilog.AspNetCore`, `Serilog.Sinks.Console`, `Serilog.Sinks.File`
- [ ] Configurar Serilog no `Program.cs` (console + arquivo)
- [ ] Configurar CORS policy para `http://localhost:3000` (frontend)
- [ ] Registrar services padrão (controllers, etc)

**Critério de aceite**: Logs aparecem no console/arquivo, CORS permite origem do frontend

---

## 🔐 FASE 2: Autenticação (Google OAuth + JWT)

### TICKET-004: Configurar ASP.NET Core Identity
- [ ] Instalar: `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- [ ] Criar `ApplicationUser` heredando de `IdentityUser` (adicionar: Name, GoogleId, PictureUrl)
- [ ] Atualizar `AppDbContext` para heredar de `IdentityDbContext<ApplicationUser>`
- [ ] Criar e aplicar migration
- [ ] Registrar Identity no DI

**Critério de aceite**: Tabelas Identity criadas no SQLite

---

### TICKET-005: Google OAuth + Endpoints de Auth
- [ ] Instalar: `Microsoft.AspNetCore.Authentication.Google`
- [ ] Configurar Google Auth no `Program.cs` (ClientId/Secret do appsettings)
- [ ] Criar `AuthController` com:
  - `POST /api/auth/google` - recebe credential token do frontend, valida com Google, cria/loga user, retorna JWT
  - `GET /api/auth/me` - retorna user atual (requer JWT)
  - `POST /api/auth/logout` - invalida token (blacklist ou apenas client-side)

**Critério de aceite**: Login com Google funciona, retorna JWT válido

---

### TICKET-006: JWT Token Service + Middleware
- [ ] Criar `JwtTokenService` com: `GenerateToken(user)`, `ValidateToken(token)`
- [ ] Configurar `JwtBearer` authentication no `Program.cs`
- [ ] Adicionar `[Authorize]` nos controllers que precisam
- [ ] Configurar opções: Key, Issuer, Audience, Expiration

**Critério de aceite**: Token JWT válido autoriza requests, token inválido retorna 401

---

## 📋 FASE 3: CRUD de Listas de Compras

### TICKET-007: Entidade ShoppingList + Migration
- [ ] Criar `ShoppingList` em Core (Id, Name, OwnerId, CreatedAt, UpdatedAt)
- [ ] Adicionar `DbSet<ShoppingList>` no `AppDbContext`
- [ ] Configurar relacionamento User → ShoppingLists (1:N)
- [ ] Criar e aplicar migration

**Critério de aceite**: Tabela ShoppingLists criada com FK para User

---

### TICKET-008: Repository Pattern - ShoppingListRepository
- [ ] Criar interface `IShoppingListRepository` em Core
- [ ] Implementar `ShoppingListRepository` em Infrastructure
- [ ] Métodos: `GetByIdAsync`, `GetByUserAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`, `ExistsAsync`

**Critério de aceite**: Repository compila, métodos testáveis

---

### TICKET-009: Service Layer - ShoppingListService
- [ ] Criar `IShoppingListService` e `ShoppingListService` em Core
- [ ] Lógica de negócio: validações, permissões, mapeamento DTO ↔ Entity
- [ ] Injetar repository no service

**Critério de aceite**: Service isolado do controller, testável com mock

---

### TICKET-010: Endpoints REST - ShoppingListsController
- [ ] `POST /api/lists` - Criar (name obrigatório, owner = user logado)
- [ ] `GET /api/lists` - Listar (owner + shared com user)
- [ ] `GET /api/lists/{id}` - Obter com produtos (verificar acesso)
- [ ] `PUT /api/lists/{id}` - Renomear (apenas owner)
- [ ] `DELETE /api/lists/{id}` - Deletar (apenas owner)

**Critério de aceite**: Todos endpoints respondem 200/201/400/403/404 corretamente

---

## 🤝 FASE 4: Compartilhamento de Listas

### TICKET-011: Entidade ListShare + Migration
- [ ] Criar `ListShare` em Core (Id, ListId, UserId, Permission enum Read/Write, SharedAt)
- [ ] Adicionar `DbSet<ListShare>` no `AppDbContext`
- [ ] Configurar relacionamentos: List 1:N ListShare, User 1:N ListShare
- [ ] Criar e aplicar migration

**Critério de aceite**: Tabela ListShares criada com FKs e enum Permission

---

### TICKET-012: Endpoints de Compartilhamento
- [ ] `POST /api/lists/{id}/share` - Body: `{ email, permission }` (apenas owner)
- [ ] `GET /api/lists/{id}/shares` - Lista compartilhamentos (apenas owner)
- [ ] `DELETE /api/lists/{id}/shares/{userId}` - Remove share (apenas owner)
- [ ] `PUT /api/lists/{id}/shares/{userId}` - Atualiza permission (apenas owner)

**Critério de aceite**: Compartilhamento funciona, permissions respeitadas

---

## 🛒 FASE 5: Produtos na Lista

### TICKET-013: Entidade Product + Migration
- [ ] Criar `Product` em Core (Id, ListId, Name, Quantity?, Price?, IsBought, CreatedAt, UpdatedAt)
- [ ] Adicionar `DbSet<Product>` no `AppDbContext`
- [ ] Configurar relacionamento ShoppingList 1:N Product
- [ ] Criar e aplicar migration

**Critério de aceite**: Tabela Products criada com FK para ShoppingList

---

### TICKET-014: Endpoints de Produtos
- [ ] `POST /api/lists/{listId}/products` - Add produto (name obrigatório, permission Write)
- [ ] `GET /api/lists/{listId}/products` - Listar (permission Read)
- [ ] `PUT /api/lists/{listId}/products/{id}` - Atualizar (name, quantity, price - Write)
- [ ] `DELETE /api/lists/{listId}/products/{id}` - Remover (Write)
- [ ] `PATCH /api/lists/{listId}/products/{id}/toggle` - Toggle IsBought (Write)

**Critério de aceite**: CRUD produtos funcional, validações de permissão

---

## ⚡ FASE 6: Real-time com SignalR

### TICKET-015: Configurar SignalR + Hub
- [ ] Instalar: `Microsoft.AspNetCore.SignalR`
- [ ] Adicionar SignalR no `Program.cs` (`builder.Services.AddSignalR()`)
- [ ] Criar `ShoppingListHub` com grupos: `list-{listId}`
- [ ] Mapear hub: `app.MapHub<ShoppingListHub>("/hubs/shoppinglist")`

**Critério de aceite**: Hub acessível, conexão SignalR estabelecida

---

### TICKET-016: Server Events (Broadcast) - Integração com Controllers
- [ ] Injetar `IHubContext<ShoppingListHub>` nos services/controllers
- [ ] Emitir eventos após operações bem-sucedidas:
  - `ProductAdded`, `ProductUpdated`, `ProductRemoved`, `ProductToggled`
  - `ListUpdated`, `ListShared`, `ListUnshared`
- [ ] Método `JoinList(listId)` / `LeaveList(listId)` no Hub (gerencia groups)

**Critério de aceite**: Frontend recebe updates em tempo real via SignalR

---

### TICKET-017: User Presence (Opcional)
- [ ] `UserJoined`, `UserLeft` events
- [ ] Tracking de usuários online por lista (em memória ou Redis)

**Critério de aceite**: Mostra quem está online na lista

---

## ✅ FASE 7: Validações e Regras de Negócio

### TICKET-018: Authorization Policies + Validações
- [ ] Policies: `IsOwner`, `HasWritePermission`, `HasReadPermission`
- [ ] Aplicar nos endpoints via `[Authorize(Policy="...")]` ou checks manuais
- [ ] Validação: Name obrigatório nas listas e produtos
- [ ] Quantity/Price nullable (opcionais)

**Critério de aceite**: Regras de negócio aplicadas consistentemente

---

## 🧪 FASE 8: Testes e Documentação

### TICKET-019: Testes Unitários (Services)
- [ ] Testes para `ShoppingListService` (mock repository)
- [ ] Testes para `ProductService`
- [ ] Testes para `ShareService`
- [ ] Cobertura > 80% nos services

**Critério de aceite**: `dotnet test` passa, cobertura adequada

---

### TICKET-020: Testes de Integração (Endpoints + Auth)
- [ ] Testes com `WebApplicationFactory`
- [ ] Cenários: auth válido, auth inválido, permissions, not found
- [ ] Setup/teardown de banco em memória (SQLite in-memory)

**Critério de aceite**: Endpoints testados end-to-end

---

### TICKET-021: Testes SignalR
- [ ] Conexão, join/leave groups, broadcast recebido
- [ ] Usar `HubConnectionBuilder` para testar cliente

**Critério de aceite**: Eventos SignalR testados

---

### TICKET-022: Swagger/OpenAPI + README
- [ ] Configurar Swagger com JWT auth (bearer token)
- [ ] XML comments nos controllers para docs
- [ ] README: setup, rodar, variáveis de ambiente, Google OAuth config

**Critério de aceite**: Swagger UI funcional, README completo

---

## 📋 Resumo de Dependências

```
TICKET-001 → TICKET-002 → TICKET-003
                    ↓
              TICKET-004 → TICKET-005 → TICKET-006
                    ↓
              TICKET-007 → TICKET-008 → TICKET-009 → TICKET-010
                    ↓
              TICKET-011 → TICKET-012
                    ↓
              TICKET-013 → TICKET-014
                    ↓
              TICKET-015 → TICKET-016 → TICKET-017
                    ↓
              TICKET-018
                    ↓
         TICKET-019, TICKET-020, TICKET-021, TICKET-022 (paralelos)
```