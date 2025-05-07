# 📌 TrackerVagas - Backend

API desenvolvida em ASP.NET Core para gerenciar candidaturas de emprego. Este backend fornece os endpoints necessários para registrar, atualizar e consultar vagas, etapas do processo seletivo e dados estatísticos da jornada de busca por emprego.

<p align="center">
  <img src="https://github.com/user-attachments/assets/f26877a0-4f5b-4423-b37b-85c1e6ccc428" alt="Demonstração da API" width="600"/>
</p>

---

## 🔧 Funcionalidades

- 📋 CRUD completo de vagas de emprego
- 🧭 Acompanhamento de status por etapa (aplicado, entrevista, proposta etc.)
- 🔍 Filtros e ordenação por status, data e empresa
- 📊 Endpoints para estatísticas e visão geral das candidaturas
- 🔐 Autenticação JWT, Credenciais Google
- 🧪 Testes automatizados *(EM BREVE)*

---

## 🛠️ Tecnologias e Ferramentas

- [.NET 9](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [MySQL](https://dev.mysql.com/doc/)
- [Swagger](https://swagger.io/tools/swagger-ui/) para documentação interativa da API
- [xUnit](https://xunit.net/) *(EM BREVE)*

---

## 🚀 Como executar o projeto

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/TrackerVagas-Backend.git
```
2. **Acessar a pasta raiz do projeto**
```bash
cd TrackerVagas/App
```
3. **Instalar as dependências**
```bash
dotnet restore
```
4. **Crie uma arquivo *.env* na raiz do projeto e preencha as seguintes chaves**
```bash
DATABASECONNECTION = "Server=localhost;Database=TrackerVagasdb;User Id="SEU USUARIO";Password="SUA SENHA""
SECRETKEY = "CRIE SUA CHAVE SECRETA"
```
5. **Execute as migrações**
```bash
dotnet ef database update
```
6. **Inicie o servidor**
```bash
dotnet run
```
---
## 👤 Autor
Desenvolvido por: [Carlos Erick](https://github.com/carloserickms)
📫 Contato: [LinkedIn](https://www.linkedin.com/in/carlos-erick/) | carloserick71@gmail.com
