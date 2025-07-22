# Sistema de Transações de Usuários

Uma aplicação completa para simulação de transações financeiras, construída com .NET 8, utilizando arquitetura limpa, microsserviços, Kafka para mensageria, SendGrid para envio de emails e interface web com Next.js.

## Índice

- [Funcionalidades](#-funcionalidades)
- [Regras de Negócio](#-regras-de-negócio)
- [Arquitetura](#-arquitetura)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Executando com Docker](#-executando-com-docker)
- [Aplicação Web](#-aplicação-web)
- [Endpoints da API](#-endpoints-da-api)
- [Estrutura do Projeto](#-estrutura-do-projeto)

## Funcionalidades

### API Principal
- **Cadastro de Usuários**: Criação de usuários comuns e lojistas
- **Listagem de Usuários**: Consulta de todos os usuários e contagem total
- **Gerenciamento de Carteiras**: Criação e controle de saldo das carteiras
- **Listagem de Carteiras**: Consulta de todas as carteiras e contagem total
- **Processamento de Transações**: Transferência de valores entre carteiras
- **Relatórios de Transações**: 
  - Listagem de todas as transações
  - Contagem total de transações
  - Valor total transacionado
  - Últimas 4 transações realizadas
- **Validação Externa**: Integração com serviço de autorização de transações
- **Mensageria Assíncrona**: Publicação de eventos via Kafka
- **Health Check**: Monitoramento da saúde dos serviços

### Consumer (Microsserviço)
- **Consumo de Eventos**: Processamento assíncrono de eventos de transação
- **Envio de Emails**: Notificação automática via SendGrid
- **Interface de Monitoramento**: Kafka UI para visualização das mensagens

### Interface Web (Frontend)
- **Dashboard Interativo**: Interface moderna e responsiva
- **Visualização de Dados**: Gráficos e estatísticas em tempo real
- **Gestão de Usuários**: Cadastro e visualização de usuários
- **Gestão de Carteiras**: Criação e monitoramento de carteiras
- **Processamento de Transações**: Interface intuitiva para transferências
- **Relatórios**: Visualização de relatórios e métricas

##  Regras de Negócio

### Usuários
- **Campos obrigatórios**: Nome completo, email, CPF e senha
- **Tipos de usuário**: 
  - `User` (1): Usuário comum
  - `Merchant` (2): Lojista
- **Validações**:
  - Email deve ser único no sistema
  - CPF deve ser único no sistema
  - Email deve ter formato válido
  - CPF deve ter formato válido

### Carteiras
- **Saldo inicial**: Toda carteira inicia com R$ 500,00
- **Vinculação**: Cada carteira está vinculada a um usuário único

### Transações
- **Validações de negócio**:
  - Remetente deve ter saldo suficiente
  - Lojistas (Merchant) **não podem** enviar dinheiro (apenas receber)
  - Ambas as carteiras (remetente e destinatário) devem existir
  - Valor da transação deve ser positivo
  
- **Autorização externa**: 
  - Todas as transações são validadas por serviço externo
  - URL: `https://util.devi.tools/api/v2/authorize`
  - Transação é rejeitada se o serviço não autorizar

- **Processamento**:
  - Operação é transacional (tudo ou nada)
  - Em caso de erro, rollback é executado
  - Evento é publicado no Kafka após sucesso

### Notificações
- **Email automático**: Destinatário recebe email após transação concluída
- **Processamento assíncrono**: Via consumer Kafka
- **Provedor**: SendGrid

## Arquitetura

O sistema segue os princípios da **Arquitetura Limpa** e **DDD (Domain Driven Design)**:

```
├── API (Camada de Apresentação)
├── Application (Casos de Uso)
├── Domain (Regras de Negócio)
├── Infrastructure (Acesso a Dados)
├── Consumer (Microsserviço)
└── Shared (Comunicação/DTOs)
```

### Componentes da Infraestrutura
- **SQL Server**: Banco de dados principal
- **Apache Kafka**: Mensageria para eventos
- **Kafka UI**: Interface para monitoramento
- **SendGrid**: Serviço de email

##  Tecnologias Utilizadas

### Backend
- **.NET 8**: Framework principal
- **ASP.NET Core**: API Web
- **Entity Framework Core**: ORM
- **FluentValidation**: Validação de dados
- **AutoMapper**: Mapeamento de objetos

### Frontend
- **Next.js 15**: Framework React com App Router
- **TypeScript**: Tipagem estática
- **Tailwind CSS**: Framework de estilização
- **Shadcn**: Componentes acessíveis
- **React Hook Form**: Gerenciamento de formulários
- **Zod**: Validação de esquemas
- **Lucide React**: Ícones

### Testes
- **xUnit**: Framework de testes
- **FluentAssertions**: Assertions fluentes
- **Bogus**: Geração de dados fake
- **Moq**: Mocking para testes

### Infraestrutura
- **Docker & Docker Compose**: Containerização
- **SQL Server 2022**: Banco de dados
- **Apache Kafka**: Message broker
- **Zookeeper**: Coordenação do Kafka

### Serviços Externos
- **SendGrid**: Envio de emails
- **Util.devi.tools**: Autorização de transações

## 🐳 Executando com Docker

### Pré-requisitos
- Docker
- Docker Compose

### Passos para execução

1. **Clone o repositório**:
```bash
git clone <repository-url>
cd UserTransactions
```

2. **Execute o ambiente completo**:
```bash
docker-compose up -d
```

### Serviços disponíveis após a execução:

| Serviço | URL | Porta | Descrição |
|---------|-----|-------|-----------|
| **Interface Web** | http://localhost:3000 | 3000 | Frontend Next.js |
| **API Principal** | http://localhost:8080/swagger | 8080/8081 | API REST com Swagger |
| **Consumer** | - | 8084/8085 | Microsserviço de eventos |
| **SQL Server** | localhost:1433 | 1433 | Banco de dados |
| **Kafka** | localhost:9092 | 9092 | Message broker |
| **Kafka UI** | http://localhost:9090 | 9090 | Interface do Kafka |
| **Zookeeper** | localhost:2181 | 2181 | Coordenação do Kafka |

### Credenciais do SQL Server
- **Servidor**: `localhost,1433`
- **Usuário**: `sa`
- **Senha**: `YourStrong@Passw0rd`
- **Database**: `userTransactions`

### Verificando a execução

1. **Interface Web**: Acesse http://localhost:3000
2. **API funcionando**: Acesse http://localhost:8080/swagger
3. **Kafka UI**: Acesse http://localhost:9090
4. **Logs dos containers**:
```bash
docker-compose logs -f frontend-usertransactions
docker-compose logs -f api-usertransactions
docker-compose logs -f consumer-usertransactions
```

##  Aplicação Web

A aplicação conta com uma interface web construída com Next.js, oferecendo:

### Funcionalidades da Interface
- **Dashboard Principal**: Visão geral do sistema com métricas importantes
- **Gerenciamento de Usuários**: 
  - Cadastro de novos usuários
  - Listagem de todos os usuários
  - Visualização de total de usuários cadastrados
- **Gerenciamento de Carteiras**:
  - Criação de carteiras para usuários
  - Listagem de todas as carteiras
  - Visualização de total de carteiras ativas
- **Processamento de Transações**:
  - Interface para realizar transferências
  - Listagem de todas as transações
  - Visualização das últimas 4 transações
  - Relatório de valor total transacionado
- **Monitoramento**:
  - Health check dos serviços
  - Status em tempo real da aplicação

### Características Técnicas
- **Design Responsivo**: Interface adaptável para desktop e mobile
- **Tema Claro/Escuro**: Alternância entre modos de visualização
- **Componentes Reutilizáveis**: Arquitetura modular com componentes React
- **Validação de Formulários**: Validação client-side e server-side
- **Feedback Visual**: Notificações e estados de loading
- **Integração com API**: Comunicação em tempo real com o backend

##  Endpoints da API

### Base URL
```
http://localhost:8080/v1
```

### Usuários

#### Criar Usuário
```http
POST /v1/User/register
Content-Type: application/json

{
  "fullName": "João Silva",
  "email": "joao@example.com", 
  "cpf": "12345678901",
  "password": "senha123",
  "userType": 1
}
```

#### Listar Todos os Usuários
```http
GET /v1/User/list-all
```

#### Obter Total de Usuários
```http
GET /v1/User/list-total-quantity
```

### Carteiras

#### Criar Carteira
```http
POST /v1/Wallet/register
Content-Type: application/json

{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

#### Listar Todas as Carteiras
```http
GET /v1/Wallet/list-all
```

#### Obter Total de Carteiras
```http
GET /v1/Wallet/list-total-quantity
```

### Transações

#### Criar Transação
```http
POST /v1/Transaction/register
Content-Type: application/json

{
  "amount": 100.50,
  "senderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "receiverId": "3fa85f64-5717-4562-b3fc-2c963f66afa7"
}
```

#### Listar Todas as Transações
```http
GET /v1/Transaction/list-all
```

#### Obter Total de Transações
```http
GET /v1/Transaction/list-total-quantity
```

#### Obter Valor Total Transacionado
```http
GET /v1/Transaction/list-total-amount
```

#### Obter Últimas 4 Transações
```http
GET /v1/Transaction/list-latest-four
```

### Health Check

#### Verificar Saúde do Sistema
```http
GET /v1/Health
```

### Tipos de Usuário
- **1**: User (Usuário comum) 
- **2**: Merchant (Lojista)

## Estrutura do Projeto

```
UserTransactions/
├── src/
│   ├── Backend/
│   │   ├── UserTransactions.API/          # API Web
│   │   ├── UserTransactions.Application/  # Casos de uso
│   │   ├── UserTransactions.Domain/       # Entidades e regras
│   │   └── UserTransactions.Infrastructure/ # Acesso a dados
│   ├── Consumer/
│   │   └── UserTransactions.Consumer/     # Microsserviço consumer
│   ├── Frontend/
│   │   └── usertransactions-web/          # Aplicação web Next.js
│   └── Shared/
│       ├── UserTransactions.Communication/ # DTOs
│       └── UserTransactions.Exception/    # Exceções
├── tests/
│   └── UserTransactions.Tests/            # Testes unitários
├── docker-compose.yml                     # Orquestração
└── README.md
```

## Fluxo de Transação

1. **Interface Web**: Usuário acessa http://localhost:3000 e utiliza o formulário de transação
2. **Requisição**: Cliente envia POST para `/v1/Transaction/register`
3. **Validação**: Sistema valida dados e regras de negócio
4. **Autorização**: Consulta serviço externo de autorização
5. **Processamento**: Debita remetente e credita destinatário
6. **Persistência**: Salva transação no banco de dados
7. **Evento**: Publica evento no Kafka
8. **Email**: Consumer processa evento e envia email
9. **Atualização**: Interface web atualiza os dados em tempo real

##  Observações Importantes

### Desenvolvimento Local
- Para desenvolvimento sem Docker, configurar SQL Server local
- Ajustar connection strings nos appsettings.json
- Executar migrations: `dotnet ef database update`

### Monitoramento
- Logs disponíveis via `docker-compose logs`
- Kafka UI permite visualizar mensagens e tópicos
- Health checks configurados para todos os serviços

---