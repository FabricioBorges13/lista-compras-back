# TICKET-016: Server Events (Broadcast) - Integração com Controllers

## Fase
⚡ FASE 6: Real-time com SignalR

## Descrição
Integrar SignalR com controllers/services para broadcast de eventos

## Tarefas
- [ ] Injetar `IHubContext<ShoppingListHub>` nos services/controllers
- [ ] Emitir eventos após operações bem-sucedidas:
  - `ProductAdded`, `ProductUpdated`, `ProductRemoved`, `ProductToggled`
  - `ListUpdated`, `ListShared`, `ListUnshared`
- [ ] Método `JoinList(listId)` / `LeaveList(listId)` no Hub (gerencia groups)

## Critério de Aceite
Frontend recebe updates em tempo real via SignalR

## Dependências
- TICKET-015
- TICKET-010
- TICKET-014
- TICKET-012