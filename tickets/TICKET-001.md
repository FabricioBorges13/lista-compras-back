# TICKET-001: Criar Solution e Estrutura de Projetos

## Fase
📦 FASE 1: Setup Inicial

## Descrição
Criar a estrutura base da solution .NET 8 com 4 projetos

## Tarefas
- [ ] `dotnet new sln -n ListaCompras`
- [ ] `dotnet new webapi -n ListaCompras.Api -o src/ListaCompras.Api`
- [ ] `dotnet new classlib -n ListaCompras.Core -o src/ListaCompras.Core`
- [ ] `dotnet new classlib -n ListaCompras.Infrastructure -o src/ListaCompras.Infrastructure`
- [ ] `dotnet new xunit -n ListaCompras.Tests -o src/ListaCompras.Tests`
- [ ] Adicionar projetos à solution
- [ ] Configurar referências: Api → Core, Api → Infrastructure, Infrastructure → Core, Tests → todos

## Critério de Aceite
`dotnet build` passa sem erros

## Dependências
- Nenhuma (primeiro ticket)