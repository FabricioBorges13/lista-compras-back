# TICKET-003: Configurar DI, Logging e CORS

## Fase
📦 FASE 1: Setup Inicial

## Descrição
Configurar Dependency Injection, Serilog para logging e CORS para frontend

## Tarefas
- [ ] Instalar: `Serilog.AspNetCore`, `Serilog.Sinks.Console`, `Serilog.Sinks.File`
- [ ] Configurar Serilog no `Program.cs` (console + arquivo)
- [ ] Configurar CORS policy para `http://localhost:3000` (frontend)
- [ ] Registrar services padrão (controllers, etc)

## Critério de Aceite
Logs aparecem no console/arquivo, CORS permite origem do frontend

## Dependências
- TICKET-001