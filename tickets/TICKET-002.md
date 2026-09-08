# TICKET-002: Configurar EF Core + SQLite

## Fase
📦 FASE 1: Setup Inicial

## Descrição
Configurar Entity Framework Core com SQLite e criar migration inicial

## Tarefas
- [ ] Instalar pacotes: `Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`
- [ ] Criar `AppDbContext` em Infrastructure
- [ ] Configurar connection string no `appsettings.json`
- [ ] Registrar DbContext no DI (Program.cs)
- [ ] Criar migration inicial (pode ser vazia)
- [ ] `dotnet ef database update`

## Critério de Aceite
Banco criado, migration aplicada

## Dependências
- TICKET-001