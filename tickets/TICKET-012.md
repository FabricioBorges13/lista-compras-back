# TICKET-012: Endpoints de Compartilhamento

## Fase
🤝 FASE 4: Compartilhamento de Listas

## Descrição
Implementar endpoints para gerenciar compartilhamento de listas

## Tarefas
- [ ] `POST /api/lists/{id}/share` - Body: `{ email, permission }` (apenas owner)
- [ ] `GET /api/lists/{id}/shares` - Lista compartilhamentos (apenas owner)
- [ ] `DELETE /api/lists/{id}/shares/{userId}` - Remove share (apenas owner)
- [ ] `PUT /api/lists/{id}/shares/{userId}` - Atualiza permission (apenas owner)

## Critério de Aceite
Compartilhamento funciona, permissions respeitadas

## Dependências
- TICKET-011
- TICKET-010
- TICKET-006 (auth)