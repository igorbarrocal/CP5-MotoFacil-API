# 🏍️ MotoFacilAPI  

## 📌 Projeto  
**Disciplina:** *Advanced Business Development with .NET*  

👤 **Autores**  
- Igor Barrocal – RM555217  
- Cauan da Cruz – RM558238  

---

## 📖 Descrição  

O **MotoFacilAPI** é uma **API RESTful** construída em **.NET 8**, voltada para o gerenciamento de usuários, motos e serviços realizados nas motos.  
A arquitetura segue os princípios de **Clean Architecture**, **Domain-Driven Design (DDD)** e **Clean Code**, proporcionando:  

✅ Baixo acoplamento  
✅ Alta manutenibilidade  
✅ Escalabilidade  

---

## ⚙️ Funcionalidades  

- 👥 **Gerenciamento de Usuários** (CRUD completo, entidade rica, Value Object para e-mail)
- 🏍️ **Gerenciamento de Motos** (CRUD completo, vínculo com usuário, enum para modelo da moto: `MottuSport`, `MottuE`, `MottuPop`)
- 🔧 **Gerenciamento de Serviços** realizados nas motos (CRUD completo, regras de reagendamento)
- 📦 **Validação de dados** via DTOs e entidades
- 📑 **Documentação interativa** com Swagger/OpenAPI (descrição de endpoints, parâmetros e exemplos)
- 🗄️ **Persistência de dados** com MongoDB (Atlas ou local)
- 🧩 **Paginação** nos endpoints de listagem (`page`, `pageSize`, retorno `totalCount`)
- 🔗 **HATEOAS** (links de navegação nos retornos das entidades)
- 🔒 **Boas práticas REST**: status code adequado, payloads claros, uso correto dos verbos HTTP
- 💓 **Health Check** integrado, monitorando aplicação e banco

---

## 📂 Estrutura do Projeto  
src/

┣ 📂 Api — Controllers REST, validações de entrada, Swagger  
┣ 📂 Application — Serviços de aplicação, DTOs  
┣ 📂 Domain — Entidades, enums, Value Objects, Interfaces de Repositório  
┗ 📂 Infrastructure — Persistência de dados, repositórios (MongoDB)  

---

## 🚀 Tecnologias Utilizadas  

- [.NET 8](https://dotnet.microsoft.com/)  
- **C#**  
- **MongoDB** (Atlas ou local)  
- **Swagger/OpenAPI**  

---

## 📄 Endpoints Principais  

### 👥 Usuários  
| Método | Endpoint        | Descrição            |  
|--------|----------------|----------------------|  
| GET    | `/usuarios`    | Listar todos os usuários (com paginação e HATEOAS) |  
| GET    | `/usuarios/{id}` | Buscar usuário por ID (com HATEOAS) |  
| POST   | `/usuarios`    | Criar novo usuário |  
| PUT    | `/usuarios/{id}` | Atualizar usuário |  
| DELETE | `/usuarios/{id}` | Remover usuário |  

### 🏍️ Motos  
| Método | Endpoint     | Descrição           |  
|--------|-------------|---------------------|  
| GET    | `/motos`    | Listar todas as motos (com paginação e HATEOAS) |  
| GET    | `/motos/{id}` | Buscar moto por ID (com HATEOAS) |  
| POST   | `/motos`    | Criar nova moto (modelo, vínculo ao usuário) |  
| PUT    | `/motos/{id}` | Atualizar moto |  
| DELETE | `/motos/{id}` | Remover moto |  

### 🔧 Serviços  
| Método | Endpoint        | Descrição            |  
|--------|----------------|----------------------|  
| GET    | `/servicos`    | Listar todos os serviços (com paginação e HATEOAS) |  
| GET    | `/servicos/{id}` | Buscar serviço por ID (com HATEOAS) |  
| POST   | `/servicos`    | Criar novo serviço (vinculado a uma moto e usuário) |  
| PUT    | `/servicos/{id}` | Atualizar serviço (reagendar data, etc.) |  
| DELETE | `/servicos/{id}` | Remover serviço |  

### 💓 Health Check
| Método | Endpoint    | Descrição                      |
|--------|-------------|--------------------------------|
| GET    | `/health`   | Verifica status da aplicação e conexão com MongoDB |

Exemplo de resposta:
```json
{
  "status": "Healthy",
  "mongo": "Connected"
}
```

---

## 📝 Exemplos de Payloads  

### Criar Usuário

```json
POST /usuarios
{
  "nome": "João Silva",
  "email": "joao@email.com"
}
```

### Criar Moto

```json
POST /motos
{
  "placa": "ABC1234",
  "modelo": "MottuSport",
  "usuarioId": "65321edbe773e2e8b0118c1d"
}
```

> Modelos válidos: `"MottuSport"`, `"MottuE"`, `"MottuPop"`

### Criar Serviço

```json
POST /servicos
{
  "descricao": "Troca de óleo",
  "data": "2025-09-25T14:00:00Z",
  "usuarioId": "65321edbe773e2e8b0118c1d",
  "motoId": "65321edbe773e2e8b0118c1e"
}
```

---

## 📝 Modelos dos Dados (Swagger/OpenAPI)  

Todos os endpoints têm modelos de dados detalhados, exemplos de payloads de requisição e resposta, e parâmetros descritos no Swagger.  
- Acesse [https://localhost:7150/swagger](https://localhost:7150/swagger) após rodar a API.

### Versionamento da API

- Documentação disponível nas versões **v1** e **v2**
- No Swagger, selecione a versão desejada para visualizar endpoints e novidades

---

## 🛠️ Como Executar Localmente  

### 1️⃣ Clone o repositório  
```bash
git clone https://github.com/igorbarrocal/MotoFacil-API.git
cd MotoFacil-API
```

### 2️⃣ Configure o banco de dados  

Instale e rode o MongoDB localmente (padrão: `mongodb://localhost:27017`)  
Ou use o [MongoDB Atlas](https://www.mongodb.com/atlas/database), alterando a string de conexão em `appsettings.Development.json` ou `appsettings.json`:

```json
"ConnectionStrings": {
  "MongoDb": "mongodb://localhost:27017"
}
```
> **Atenção:** O banco será criado automaticamente na primeira execução.

### 3️⃣ Rode a API  
```bash
dotnet run --project MotoFacil-API
```

Acesse o Swagger em:  
```
https://localhost:7150/swagger
```

---

## 💡 Observações Finais

- Estrutura em **Clean Architecture** (camadas: Api, Application, Domain, Infrastructure)
- **DDD**: entidades ricas, Value Object (Email), interfaces de repositório no domínio
- **Health Check**: endpoint `/health` monitora a aplicação e o banco
- **Swagger** com versionamento: v1, v2
- **Commits semânticos** e estrutura de pastas padronizada

---

## 🧪 Testes

Se houver testes automatizados:
```bash
dotnet test
```
