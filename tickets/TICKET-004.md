# TICKET-004: Configurar ASP.NET Core Identity

## Fase
🔐 FASE 2: Autenticação (Google OAuth + JWT)

## Descrição
Configurar ASP.NET Core Identity com usuário customizado

## Tarefas
- [ ] Instalar: `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- [ ] Criar `ApplicationUser` herdando de `IdentityUser` (adicionar: Name, GoogleId, PictureUrl)
- [ ] Atualizar `AppDbContext` para herdar de `IdentityDbContext<ApplicationUser>`
- [ ] Criar e aplicar migration
- [ ] Registrar Identity no DI

## Critério de Aceite
Tabelas Identity criadas no SQLite

## Dependências
- TICKET-002