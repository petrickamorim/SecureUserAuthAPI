# 🔐 SecureUserAuthAPI

API RESTful para autenticação de usuários utilizando **.NET 8**, **JWT** e **Refresh Token**, com persistência em **SQLite** e documentação via **Swagger**.

---

## 🚀 Funcionalidades

- Registro de usuários com senha criptografada (HMACSHA512)
- Login com geração de **Access Token** e **Refresh Token**
- Atualização automática de Access Token via rota `/refresh`
- Proteção de rotas com `[Authorize]`
- Persistência usando **Entity Framework Core** e **SQLite**
- Documentação interativa com **Swagger**

---

## 🧱 Tecnologias Utilizadas

- .NET 8 (ASP.NET Core Web API)
- C#
- JWT (JSON Web Tokens)
- Entity Framework Core
- SQLite
- Swagger (Swashbuckle)

---

## 📂 Estrutura de Pastas

```
AuthAPI/
├── Controllers/
├── DTOs/
├── Models/
├── Data/
├── Program.cs
├── appsettings.json
├── AuthAPI.csproj
```

---

## 🛠️ Como rodar o projeto localmente

1. **Clone o repositório:**
```bash
git clone https://github.com/seu-usuario/SecureUserAuthAPI.git
cd SecureUserAuthAPI
```

2. **Restaure os pacotes:**
```bash
dotnet restore
```

3. **Crie o banco de dados:**
```bash
dotnet ef migrations add Init
dotnet ef database update
```

4. **Rode a aplicação:**
```bash
dotnet run
```

5. **Acesse o Swagger:**
```
https://localhost:5001/swagger
```

---

## 🔐 Fluxo de Autenticação

1. O usuário faz login em `/api/auth/login`.
2. A API responde com:
   - Access Token (expira em 2 minutos)
   - Refresh Token (expira em 7 dias)
3. O cliente usa o Access Token para acessar rotas protegidas.
4. Ao expirar, o cliente envia o Refresh Token para `/api/auth/refresh` e recebe um novo Access Token.

---

## 🧪 Endpoints da API

| Método | Rota                  | Autenticado | Descrição                        |
|--------|------------------------|-------------|----------------------------------|
| POST   | `/api/auth/register`   | ❌          | Registra um novo usuário         |
| POST   | `/api/auth/login`      | ❌          | Autentica e retorna os tokens    |
| POST   | `/api/auth/refresh`    | ❌          | Gera novo Access Token           |
| GET    | `/api/auth/users`      | ✅          | Lista os usuários registrados    |

---

## 📄 Exemplo de JSON para Login/Register

```json
{
  "username": "petrick",
  "password": "123456"
}
```

---

## ✅ Requisitos

- .NET SDK 8.0 ou superior
- Visual Studio ou VS Code
- CLI do EF Core (opcional): `dotnet tool install --global dotnet-ef`

---

## 🧑‍💻 Autor

Desenvolvido por **Petrick Amorim**  
📧 amorimpetrick@icloud.com  
📱 (11) 94524-8092

---

## 📃 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.
