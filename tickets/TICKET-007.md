# TICKET-007: Entidade ShoppingList + Migration

## Fase
📋 FASE 3: CRUD de Listas de Compras

## Descrição
Criar entidade ShoppingList e migration

## Tarefas
- [ ] Criar `ShoppingList` em Core (Id, Name, OwnerId, CreatedAt, UpdatedAt)
- [ ] Adicionar `DbSet<ShoppingList>` no `AppDbContext`
- [ ] Configurar relacionamento User → ShoppingLists (1:N)
- [ ] Criar e aplicar migration

## Critério de Aceite
Tabela ShoppingLists criada com FK para User

## Dependências
- TICKET-004