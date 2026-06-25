# MeatFlow

Sistema para gerenciamento de carnes, compradores e pedidos.

## Pré-requisitos

- [Node.js 18+](https://nodejs.org/)
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- SQL Server (local ou Docker)

## Rodando o projeto

### 1. API (.NET)

```bash
# Acesse a pasta do backend
cd APIMeatFlow

# Instale o EF CLI (apenas na primeira vez)
dotnet tool install --global dotnet-ef

# Aplique as migrations (cria o banco db_meatflow automaticamente)
dotnet ef database update --project MeatFlow.Data --startup-project MeatFlow.Controller

# Suba a API
dotnet run --project MeatFlow.Controller
```

A API estará disponível em **http://localhost:5000**
Documentação Swagger em **http://localhost:5000/swagger**

> A connection string padrão usa Windows Authentication no SQL Server local.
> Se precisar ajustar, edite `MeatFlow.Controller/appsettings.json`.

---

### 2. Front-end (React)

```bash
# Acesse a pasta do frontend
cd AwMeatFlow

# Instale as dependências
npm install

# Suba o servidor de desenvolvimento
npm run dev
```

O frontend estará disponível em **http://localhost:3000**

---

## Estrutura do projeto

```
MeatFlow/
├── APIMeatFlow/       # Back-end .NET 8 (API REST)
└── AwMeatFlow/        # Front-end React + TypeScript
```

```
AwMeatFlow/src/
├── app/               # Páginas (uma por rota)
├── components/
│   ├── ui/            # Componentes genéricos (Button, Input, Modal…)
│   └── shared/        # Componentes da aplicação (Header, PageTitle…)
├── features/          # Módulos de domínio (carne, comprador, pedido)
│   └── [feature]/
│       ├── components/ dto/ hooks/ services/ types/ utils/
├── layouts/           # AppLayout (Header + Outlet)
├── lib/               # Instância Axios (http.ts)
├── providers/         # Providers React globais
├── routes/            # Declaração de rotas
└── utils/             # Formatadores (data, moeda, documento)
```

---

## Variáveis de ambiente

O arquivo `.env.local` não é versionado. Antes de rodar o frontend, crie-o a partir do exemplo:

```bash
cd AwMeatFlow
cp .env.example .env.local
```

Se rodar a API em outra porta, ajuste o valor em `.env.local`.

---

## Scripts disponíveis

| Script            | Descrição                            |
|-------------------|--------------------------------------|
| `npm run dev`     | Inicia o servidor de desenvolvimento |
| `npm run build`   | Gera o build de produção             |
| `npm run preview` | Serve o build localmente             |
| `npm run lint`    | Executa o ESLint                     |

---

## Decisões arquiteturais

**Vertical Slice por feature** — cada domínio é autocontido em `src/features/` com seus próprios components, hooks, services, DTOs e types.

**Services atômicos** — um arquivo por operação HTTP (`getCarnes.ts`, `createCarne.ts`…), seguindo responsabilidade única.

**Hooks por operação** — cada hook encapsula loading/error de uma única operação, facilitando composição nas páginas.

**TypeScript strict** — nenhum uso de `any`; todos os contratos são tipados via DTOs espelhando o backend.
