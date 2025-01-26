## **Objetivo**

Criar uma API mínima para gerenciar uma biblioteca, incluindo:

- Cadastro de clientes.
- Gerenciamento de livros e estoque.
- Controle de empréstimos e devoluções.

---

## **Arquitetura do Projeto**

### **1. Estrutura de Pastas**

```
BibliotecaApi/
├── Controllers/        # Define os endpoints da API, chamando os serviços.
│   ├── ClientsController.cs
│   ├── BookController.cs
│   └── LoansController.cs
├── Models/             # Define as entidades principais (Cliente, Livro, Empréstimo).
│   ├── BooksModel.cs
│   ├── ClientsModel.cs
│   └── LoansModel.cs
├── Services/           # Contém a lógica de negócio (Cliente, Livro, Empréstimo).
│   ├── ClientsService.cs
│   ├── BooksService.cs
│   └── LoansService.cs
├── DTOs/               # Define os objetos de requisição e resposta (Requests e Responses).
│   ├── ClientsDTO.cs
│   ├── BooksDTO.cs
│   └── LoansDTO.cs
├── Data/               # Contexto do banco de dados (Entity Framework).
│   ├── MySQLData.cs
└── Program.cs          # Configuração inicial da aplicação.

```

### **2. Arquitetura Geral**

- **Models**: Representam as tabelas do banco de dados (Cliente, Livro, Empréstimo).
- **DTOs (Data Transfer Objects)**: Abstraem os dados enviados e recebidos nos endpoints, mantendo a separação entre lógica interna e externa.
- **Services**: Contêm a lógica de negócio, removendo a responsabilidade dos controllers.
- **Controllers**: Apenas chamam os métodos dos serviços, retornando respostas para os usuários.
- **Banco de Dados**: Configurado com **Entity Framework Core**, utilizando um banco em memória para simplificação.

---

## **Banco de Dados**

### **Modelo Relacional**

O banco possui três tabelas principais:

1. **Clients**:
   - `Id` (chave primária, gerado automaticamente).
   - `Nome`.
   - `Email` (único).
   - `Status` (ativo/inativo).
2. **Books**:
   - `Id` (chave primária, gerado automaticamente).
   - `Título`.
   - `ISBN` (único).
   - `QuantidadeEstoque`.
3. **Loans**:
   - `Id` (chave primária, gerado automaticamente).
   - `ClienteId` (chave estrangeira para Clientes).
   - `LivroId` (chave estrangeira para Livros).
   - `DataEmprestimo` (data do empréstimo).
   - `DataMaxDevolucao` (data da devolução).
   - `DataDevolucao` (data da devolução).

---

## **Regras de Negócio**

1. **Clientes**:
   - Um cliente deve estar ativo (`Status = true`) para realizar um empréstimo.
   - O email do cliente deve ser único.
2. **Livros**:
   - Apenas livros com `QuantidadeEstoque > 0` podem ser emprestados.
   - O estoque deve ser atualizado automaticamente após empréstimos e devoluções.
3. **Empréstimos**:
   - Um cliente pode pegar apenas um exemplar de cada livro por vez.
   - Não é permitido empréstimo se o cliente já estiver com o mesmo livro.
   - Ao realizar um empréstimo:
     - Verificar se o cliente é válido (existe e está ativo).
     - Verificar se o livro está disponível.
     - Reduzir a quantidade do estoque do livro.
   - Ao realizar a devolução:
     - Atualizar a `DataDevolucao` no registro do empréstimo.
     - Aumentar a quantidade do estoque do livro.

---

## **Etapas do Desenvolvimento**

1. **Configuração do Projeto**:
   - Criar o projeto [ASP.NET](http://asp.net/) Core Web API.
   - Configurar o **Entity Framework Core** com banco em MySQL.
2. **Definição dos Modelos**:
   - Criar as entidades (`Cliente`, `Livro`, `Emprestimo`).
3. **Configuração do Banco de Dados**:
   - Configurar o contexto do EF Core para gerenciar as tabelas e relacionamentos.
4. **Separação em Camadas**:
   - **Controllers**: Chamam os serviços e respondem às requisições HTTP.
   - **Services**: Contêm toda a lógica de negócio, interagindo com o contexto do banco.
   - **DTOs**: Abstraem as requisições e respostas, mantendo o modelo interno isolado.
5. **Implementação dos Endpoints**:
   - CRUD básico para Clientes e Livros.
   - Fluxo de empréstimos e devoluções.
6. **Testes e Validações**:
   - Testar os endpoints usando **Postman** ou **curl**.
   - Validar as regras de negócio.

---

## **Fluxo de Empréstimos**

1. Verificar se o cliente existe e está ativo.
2. Verificar se o livro existe e tem quantidade disponível no estoque.
3. Caso o empréstimo seja válido:
   - Criar o registro do empréstimo.
   - Atualizar o estoque do livro (diminuir quantidade).
4. Devolução:
   - Verificar se o empréstimo existe e ainda não foi devolvido.
   - Atualizar a `DataDevolucao`.
   - Repor o estoque do livro (aumentar quantidade).

---

## **Próximos Passos**

1. Implementar autenticação/autorização (e.g., autenticar clientes).
2. Criar testes unitários para validar os serviços.
3. Melhorar o tratamento de erros com middlewares personalizados.

---
