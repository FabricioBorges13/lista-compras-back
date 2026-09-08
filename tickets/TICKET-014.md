# TICKET-014: Endpoints de Produtos

## Fase
🛒 FASE 5: Produtos na Lista

## Descrição
Implementar CRUD de produtos nas listas de compras

## Tarefas
- [ ] `POST /api/lists/{listId}/products` - Add produto (name obrigatório, permission Write)
- [ ] `GET /api/lists/{listId}/products` - Listar (permission Read)
- [ ] `PUT /api/lists/{listId}/products/{id}` - Atualizar (name, quantity, price - Write)
- [ ] `DELETE /api/lists/{listId}/products/{id}` - Remover (Write)
- [ ] `PATCH /api/lists/{listId}/products/{id}/toggle` - Toggle IsBought (Write)

## Critério de Aceite
CRUD produtos funcional, validações de permissão

## Dependências
- TICKET-013
- TICKET-010
- TICKET-012 (permissions)
- TICKET-006 (auth)