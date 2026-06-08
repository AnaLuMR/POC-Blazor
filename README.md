# LogisticaPOC — Blazor Multiplataforma

Prova de conceito de **Blazor com HTML/CSS/C#** rodando simultaneamente como aplicação **web** (navegador) e como aplicação **desktop nativa** (Windows, Linux e macOS), compartilhando os mesmos componentes Razor e consumindo a mesma API ASP.NET Core.

---

## Arquitetura

```
LogisticaPoc.slnx
├── src/
│   ├── LogisticaPoc.Api       → API ASP.NET Core Minimal API (porta 5150)
│   ├── LogisticaPoc.Shared    → DTOs, Enums e Requests compartilhados
│   ├── LogisticaPoc.Client    → Razor Class Library: componentes, páginas, layout, CSS
│   ├── LogisticaPoc.Web       → Blazor Web App Server-Side (porta 5000)
│   └── LogisticaPoc.Desktop   → App Desktop com Photino.Blazor
└── tests/
    └── LogisticaPoc.Tests     → Testes unitários e de integração (xUnit)
```

```
[API :5150] ←── HTTP ──┬── [Web :5000]  (navegador, qualquer OS)
                       └── [Desktop]    (janela nativa, Windows/Linux/macOS)

Ambas as UIs compartilham LogisticaPoc.Client (componentes Razor, CSS, serviços)
```

---

## Pré-requisitos

| Requisito | Versão |
|---|---|
| .NET SDK | 10.0 ou superior |
| WebView2 Runtime (Windows) | Incluído no Windows 11; baixar para Windows 10 |
| WebKit / GTK (Linux) | `libwebkit2gtk-4.1-dev` |
| WebKit (macOS) | Nativo do sistema |

---

## Como Executar

### 1. API

```bash
cd src/LogisticaPoc.Api
dotnet run
# Disponível em http://localhost:5150
# OpenAPI/Swagger: http://localhost:5150/openapi/v1.json
```

O banco SQLite (`logistica.db`) é criado automaticamente na primeira execução com 15 entregas, 5 motoristas, 5 veículos e 3 rotas de exemplo.

### 2. Aplicação Web (navegador)

```bash
cd src/LogisticaPoc.Web
dotnet run
# Acesse http://localhost:5000 (ou a porta informada no terminal)
```

### 3. Aplicação Desktop (PhotinoX.Blazor)

> A API precisa estar rodando antes de abrir o Desktop.

```bash
cd src/LogisticaPoc.Desktop
dotnet run
# Uma janela nativa abre automaticamente (Windows, Linux, macOS)
```

Se a janela abrir preta com `Load(/)` no terminal, feche instâncias antigas do app, faça `dotnet build` e rode de novo. O projeto usa `[STAThread]` (WebView2 no Windows) e `blazor.webview.js` (não `blazor.desktop.js`).

---

## Executar os Testes

```bash
dotnet test tests/LogisticaPoc.Tests
```

---

## Funcionalidades da POC

| Módulo | Funcionalidade |
|---|---|
| Dashboard | KPIs de entregas, motoristas e veículos; listagem recente |
| Entregas | Listar, buscar, criar e alterar status |
| Motoristas | Listar e cadastrar |
| Veículos | Listar e cadastrar |
| Rotas | Listar e cadastrar |

---

## Tecnologias

- **.NET 10** — SDK, runtime e web framework
- **ASP.NET Core Minimal API** — API de logística com EF Core + SQLite
- **Blazor Server** — UI web com renderização interativa no servidor
- **PhotinoX.Blazor** — hospeda a mesma UI Blazor em janela desktop nativa (fork com suporte a .NET 10)
- **Razor Class Library** — compartilhamento de componentes entre web e desktop
- **xUnit** — testes com EF Core InMemory

---

## Aprendizados da POC

### O que funciona bem

- Reaproveitamento total de componentes Razor entre web e desktop
- Compartilhamento de modelos, enums, DTOs e serviços via Razor Class Library
- CSS e design system únicos servindo ambas as plataformas
- Photino.Blazor requer apenas trocar o `App.razor`/`Program.cs` para desktop

### Pontos de atenção

- `@rendermode InteractiveServer` só se aplica no Web; o Desktop usa `App.razor` sem render mode
- Photino.Blazor 4.x oficial só suporta até .NET 8 — usamos **PhotinoX.Blazor** (fork) que suporta .NET 10
- No Linux é necessário instalar WebKitGTK manualmente: `sudo apt install libwebkit2gtk-4.1-dev`
- O Desktop abre uma janela nativa mas usa WebView interno, não é uma UI nativa pura
- Para distribuir o Desktop como executável único, usar `dotnet publish -r win-x64 --self-contained`

---

## Estrutura dos Endpoints

| Método | URL | Descrição |
|---|---|---|
| GET | `/api/dashboard` | KPIs gerais |
| GET | `/api/entregas` | Lista todas as entregas |
| GET | `/api/entregas/{id}` | Detalhe de uma entrega |
| POST | `/api/entregas` | Cria nova entrega |
| PUT | `/api/entregas/{id}/status` | Atualiza status |
| GET | `/api/motoristas` | Lista motoristas |
| POST | `/api/motoristas` | Cadastra motorista |
| GET | `/api/veiculos` | Lista veículos |
| POST | `/api/veiculos` | Cadastra veículo |
| GET | `/api/rotas` | Lista rotas |
| POST | `/api/rotas` | Cadastra rota |
