# TICKET-010: Endpoints REST - ShoppingListsController

## Fase
📋 FASE 3: CRUD de Listas de Compras

## Descrição
Implementar endpoints REST para CRUD de listas de compras

## Tarefas
- [ ] `POST /api/lists` - Criar (name obrigatório, owner = user logado)
- [ ] `GET /api/lists` - Listar (owner + shared com user)
- [ ] `GET /api/lists/{id}` - Obter com produtos (verificar acesso)
- [ ] `PUT /api/lists/{id}` - Renomear (apenas owner)
- [ ] `DELETE /api/lists/{id}` - Deletar (apenas owner)

## Critério de Aceite
Todos endpoints respondem 200/201/400/403/404 corretamente

## Dependências
- TICKET-009
- TICKET-006 (auth)