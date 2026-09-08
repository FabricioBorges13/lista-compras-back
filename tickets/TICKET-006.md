# TICKET-006: JWT Token Service + Middleware

## Fase
🔐 FASE 2: Autenticação (Google OAuth + JWT)

## Descrição
Criar serviço de JWT e configurar middleware de autenticação

## Tarefas
- [ ] Criar `JwtTokenService` com: `GenerateToken(user)`, `ValidateToken(token)`
- [ ] Configurar `JwtBearer` authentication no `Program.cs`
- [ ] Adicionar `[Authorize]` nos controllers que precisam
- [ ] Configurar opções: Key, Issuer, Audience, Expiration

## Critério de Aceite
Token JWT válido autoriza requests, token inválido retorna 401

## Dependências
- TICKET-005