# Usuários de Teste - FIAP Usuario API

## Usuários disponíveis para teste de autenticação JWT

| ID | Nome | Email | Senha |
|----|------|-------|-------|
| 1 | João Silva | joao.silva@fiap.com.br | 12345678 |
| 2 | Maria Santos | maria.santos@fiap.com.br | senha123 |
| 3 | Pedro Oliveira | pedro.oliveira@fiap.com.br | teste123 |
| 4 | Admin FIAP | admin@fiap.com.br | admin123 |
| 5 | Teste User | teste@fiap.com.br | user1234 |

## Como usar

### 1. Iniciar o MySQL via Docker
```bash
docker-compose up -d mysql
```

### 2. Aguardar o healthcheck
Os scripts de inicialização serão executados automaticamente na primeira vez que o container for criado.

### 3. Testar a geração de token
Use qualquer um dos usuários acima para testar o endpoint de autenticação.

**Exemplo de requisição:**
```json
POST /api/autenticacao/token
{
  "email": "joao.silva@fiap.com.br",
  "senha": "12345678"
}
```

## Observações

?? **ATENÇÃO:** Estas senhas são apenas para ambiente de desenvolvimento/teste. 
- As senhas estão armazenadas em texto plano (sem hash)
- Em produção, implemente hash de senha (bcrypt, Argon2, etc.)
- Nunca comitar credenciais reais no repositório

## Verificar se os dados foram inseridos

```bash
# Conectar ao MySQL
docker exec -it fiapx-usuario-mysql mysql -u user_fiap -ppass_fiap fiap_usuarios

# Executar consulta
SELECT * FROM Usuario;
```

## Recriar o banco do zero

Se precisar recriar o banco e reinserir os dados:
```bash
docker-compose down -v
docker-compose up -d mysql
```
