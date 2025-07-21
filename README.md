# Sistema de Transações de Usuários

Uma aplicação completa para simulação de transações financeiras, construída com .NET 8, utilizando arquitetura limpa, microsserviços, Kafka para mensageria e SendGrid para envio de emails.

## 📋 Índice

- [Funcionalidades](#-funcionalidades)
- [Regras de Negócio](#-regras-de-negócio)
- [Arquitetura](#-arquitetura)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Executando com Docker](#-executando-com-docker)
- [Endpoints da API](#-endpoints-da-api)
- [Estrutura do Projeto](#-estrutura-do-projeto)

## 🚀 Funcionalidades

### API Principal
- **Cadastro de Usuários**: Criação de usuários comuns e lojistas
- **Gerenciamento de Carteiras**: Criação e controle de saldo das carteiras
- **Processamento de Transações**: Transferência de valores entre carteiras
- **Validação Externa**: Integração com serviço de autorização de transações
- **Mensageria Assíncrona**: Publicação de eventos via Kafka

### Consumer (Microsserviço)
- **Consumo de Eventos**: Processamento assíncrono de eventos de transação
- **Envio de Emails**: Notificação automática via SendGrid
- **Interface de Monitoramento**: Kafka UI para visualização das mensagens

## 📜 Regras de Negócio

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

## 🏗 Arquitetura

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

## 🛠 Tecnologias Utilizadas

### Backend
- **.NET 8**: Framework principal
- **ASP.NET Core**: API Web
- **Entity Framework Core**: ORM
- **FluentValidation**: Validação de dados
- **AutoMapper**: Mapeamento de objetos

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
| **API Principal** | http://localhost:8080 | 8080/8081 | API REST com Swagger |
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

1. **API funcionando**: Acesse http://localhost:8080/swagger
2. **Kafka UI**: Acesse http://localhost:9090
3. **Logs dos containers**:
```bash
docker-compose logs -f api-usertransactions
docker-compose logs -f consumer-usertransactions
```

## 📡 Endpoints da API

### Base URL
```
http://localhost:8080/v1
```

### Usuários
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

### Carteiras
```http
POST /v1/Wallet/register
Content-Type: application/json

{
  "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

### Transações
```http
POST /v1/Transaction/register
Content-Type: application/json

{
  "amount": 100.50,
  "senderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "receiverId": "3fa85f64-5717-4562-b3fc-2c963f66afa7"
}
```

### Tipos de Usuário
- **1**: User (Usuário comum) 
- **2**: Merchant (Lojista)

## 📁 Estrutura do Projeto

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
│   └── Shared/
│       ├── UserTransactions.Communication/ # DTOs
│       └── UserTransactions.Exception/    # Exceções
├── tests/
│   └── UserTransactions.Tests/            # Testes unitários
├── docker-compose.yml                     # Orquestração
└── README.md
```

## 🔄 Fluxo de Transação

1. **Requisição**: Cliente envia POST para `/v1/Transaction/register`
2. **Validação**: Sistema valida dados e regras de negócio
3. **Autorização**: Consulta serviço externo de autorização
4. **Processamento**: Debita remetente e credita destinatário
5. **Persistência**: Salva transação no banco de dados
6. **Evento**: Publica evento no Kafka
7. **Email**: Consumer processa evento e envia email

## ⚠️ Observações Importantes

### Configurações de Email
- A API Key do SendGrid está hardcoded no docker-compose.yml
- Em produção, usar variáveis de ambiente ou serviços de secrets

### Desenvolvimento Local
- Para desenvolvimento sem Docker, configurar SQL Server local
- Ajustar connection strings nos appsettings.json
- Executar migrations: `dotnet ef database update`

### Monitoramento
- Logs disponíveis via `docker-compose logs`
- Kafka UI permite visualizar mensagens e tópicos
- Health checks configurados para todos os serviços

---