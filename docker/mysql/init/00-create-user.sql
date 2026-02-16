-- Criar usuário e garantir permissões
CREATE USER IF NOT EXISTS 'user_fiap'@'%' IDENTIFIED BY 'pass_fiap';
GRANT ALL PRIVILEGES ON fiap_usuarios.* TO 'user_fiap'@'%';
FLUSH PRIVILEGES;