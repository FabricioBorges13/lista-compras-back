# TICKET-013: Entidade Product + Migration

## Fase
🛒 FASE 5: Produtos na Lista

## Descrição
Criar entidade Product para itens da lista de compras

## Tarefas
- [ ] Criar `Product` em Core (Id, ListId, Name, Quantity?, Price?, IsBought, CreatedAt, UpdatedAt)
- [ ] Adicionar `DbSet<Product>` no `AppDbContext`
- [ ] Configurar relacionamento ShoppingList 1:N Product
- [ ] Criar e aplicar migration

## Critério de Aceite
Tabela Products criada com FK para ShoppingList

## Dependências
- TICKET-007