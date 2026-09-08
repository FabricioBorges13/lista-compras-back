# TICKET-018: Authorization Policies + Validações

## Fase
✅ FASE 7: Validações e Regras de Negócio

## Descrição
Implementar policies de autorização e validações de negócio

## Tarefas
- [ ] Policies: `IsOwner`, `HasWritePermission`, `HasReadPermission`
- [ ] Aplicar nos endpoints via `[Authorize(Policy="...")]` ou checks manuais
- [ ] Validação: Name obrigatório nas listas e produtos
- [ ] Quantity/Price nullable (opcionais)

## Critério de Aceite
Regras de negócio aplicadas consistentemente

## Dependências
- TICKET-010
- TICKET-012
- TICKET-014
- TICKET-006