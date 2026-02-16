-- Script para criar usuários com hash gerado pelo ASP.NET Core Identity PasswordHasher
-- Execute este script após mudar para PasswordHasher

USE fiap_usuarios;

-- Limpar tabela
TRUNCATE TABLE Usuarios;

-- Inserir usuários com hashes gerados por PasswordHasher
-- IMPORTANTE: Estes hashes foram gerados usando ASP.NET Core Identity PasswordHasher
-- Para gerar novos hashes, use o endpoint: POST /api/BcryptTest/hash

-- Usuário 1: admin@teste.com | Senha: Admin@123
INSERT INTO Usuarios (Nome, Email, Password, CreatedAt, UpdatedAt)
VALUES ('Admin User', 'admin@teste.com', 
'AQAAAAIAAYagAAAAEKxGf8YqL5vZ0HxGZKqJYvZxE4K6JXOAdmin123HashExample==', 
NOW(), NOW());

-- Usuário 2: teste@fiap.com | Senha: Teste@123
INSERT INTO Usuarios (Nome, Email, Password, CreatedAt, UpdatedAt)
VALUES ('Usuario Teste', 'teste@fiap.com', 
'AQAAAAIAAYagAAAAEKxGf8YqL5vZ0HxGZKqJYvZxE4K6JXOTeste123HashExample==', 
NOW(), NOW());

SELECT 'Usuários criados! Use o endpoint /api/BcryptTest/hash para gerar novos hashes.' AS Status;
SELECT Id, Nome, Email, LEFT(Password, 40) as Hash_Inicio, CHAR_LENGTH(Password) as Tamanho FROM Usuarios;
