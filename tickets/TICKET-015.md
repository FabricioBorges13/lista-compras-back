# TICKET-015: Configurar SignalR + Hub

## Fase
⚡ FASE 6: Real-time com SignalR

## Descrição
Configurar SignalR e criar Hub para comunicação em tempo real

## Tarefas
- [ ] Instalar: `Microsoft.AspNetCore.SignalR`
- [ ] Adicionar SignalR no `Program.cs` (`builder.Services.AddSignalR()`)
- [ ] Criar `ShoppingListHub` com grupos: `list-{listId}`
- [ ] Mapear hub: `app.MapHub<ShoppingListHub>("/hubs/shoppinglist")`

## Critério de Aceite
Hub acessível, conexão SignalR estabelecida

## Dependências
- TICKET-003 (DI configurado)