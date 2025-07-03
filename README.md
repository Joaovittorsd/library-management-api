# 📚 Library Management API

Sistema de gestão de biblioteca comunitária desenvolvido em **.NET 8**.

Este projeto permite:

-   Cadastro e consulta de livros
-   Empréstimo e devolução de exemplares
-   Listagem de livros e empréstimos

---

## 🚀 Tecnologias

-   .NET 8
-   Entity Framework Core
-   SQL Server
-   Moq e xUnit (Testes unitários)

---

## ⚙️ Como executar o projeto

1️⃣ Clone o repositório

```bash
git clone https://github.com/Joaovittorsd/library-management-api.git
cd LibraryManagement.API
```

2️⃣ Configure a string de conexão

No arquivo appsettings.json (ou appsettings.Development.json), ajuste a conexão com seu banco de dados SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
    }
}
```

3️⃣ Restaure os pacotes

```bash
dotnet restore
```

4️⃣ Execute as migrations

Crie o banco de dados e aplique as migrations:

```bash
dotnet ef database update
```

5️⃣ Rode a aplicação

```bash
dotnet run
```
A API estará disponível em:

```arduino
https://localhost:7126/Swagger/index.html
```
ou
```arduino
https://localhost:5106/Swagger/index.html
```

## 🧪 Executar os testes

Para rodar todos os testes unitários:
```bash
dotnet test
```

## 🛠️ Endpoints Disponíveis

📘 Livros
| Método | Rota             | Descrição              |
| ------ | ---------------- | ---------------------- |
| GET    | /api/livros      | Listar todos os livros |
| GET    | /api/livros/{id} | Obter livro por Id     |
| POST   | /api/livros      | Criar novo livro       |


Exemplo payload criação de livro:
```json
{
  "titulo": "Dom Casmurro",
  "autor": "Machado de Assis",
  "anoPublicacao": 1899,
  "quantidadeDisponivel": 3
}
```

📗 Empréstimos
| Método | Rota                                 | Descrição                   |
| ------ | ------------------------------------ | --------------------------- |
| GET    | /api/emprestimos                     | Listar todos os empréstimos |
| GET    | /api/emprestimos/{id}                | Obter empréstimo por Id     |
| POST   | /api/emprestimos/solicitar           | Solicitar empréstimo        |
| POST   | /api/emprestimos/devolucao/{livroId} | Registrar devolução         |


## ✨ Observações

* Este projeto segue os princípios de **Modelagem de Domínio Rico**, com separação entre:

  * Domain
  * Application
  * Infrastructure
  * API

* Os testes cobrem regras de domínio e serviços de aplicação.


## 👤 Autor
João Vitor Lima da SILVA
https://www.linkedin.com/in/joaovittor/
vittor.prweb@outlook.com