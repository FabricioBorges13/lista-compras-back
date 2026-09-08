# TICKET-011: Entidade ListShare + Migration

## Fase
🤝 FASE 4: Compartilhamento de Listas

## Descrição
Criar entidade ListShare para compartilhamento de listas entre usuários

## Tarefas
- [ ] Criar `ListShare` em Core (Id, ListId, UserId, Permission enum Read/Write, SharedAt)
- [ ] Adicionar `DbSet<ListShare>` no `AppDbContext`
- [ ] Configurar relacionamentos: List 1:N ListShare, User 1:N ListShare
- [ ] Criar e aplicar migration

## Critério de Aceite
Tabela ListShares criada com FKs e enum Permission

## Dependências
- TICKET-007