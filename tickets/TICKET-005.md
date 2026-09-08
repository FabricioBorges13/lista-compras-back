# TICKET-005: Google OAuth + Endpoints de Auth

## Fase
🔐 FASE 2: Autenticação (Google OAuth + JWT)

## Descrição
Implementar autenticação com Google OAuth e endpoints de login/me/logout

## Tarefas
- [ ] Instalar: `Microsoft.AspNetCore.Authentication.Google`
- [ ] Configurar Google Auth no `Program.cs` (ClientId/Secret do appsettings)
- [ ] Criar `AuthController` com:
  - `POST /api/auth/google` - recebe credential token do frontend, valida com Google, cria/loga user, retorna JWT
  - `GET /api/auth/me` - retorna user atual (requer JWT)
  - `POST /api/auth/logout` - invalida token (blacklist ou apenas client-side)

## Critério de Aceite
Login com Google funciona, retorna JWT válido

## Dependências
- TICKET-004