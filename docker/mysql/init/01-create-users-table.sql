USE fiap_usuarios;

CREATE TABLE IF NOT EXISTS Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nome VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL,
    INDEX idx_email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO Usuarios (Nome, Email, Password, CreatedAt, UpdatedAt) VALUES
('Admin User', 'admin@teste.com', '$2a$12$LQv3c1yqBWVHxkd0LHAkCOYz6TtxMeStkKEA.Z6.y8B8W5hN6Rj3C', UTC_TIMESTAMP(), UTC_TIMESTAMP()),
('João Silva', 'joao.silva@teste.com', '$2a$12$rQv3c1yqBWVHxkd0LHAkCOYz6TtxMeStkKEA.Z6.y8B8W5hN6Rj3D', UTC_TIMESTAMP(), UTC_TIMESTAMP()),
('Maria Santos', 'maria.santos@teste.com', '$2a$12$sQv3c1yqBWVHxkd0LHAkCOYz6TtxMeStkKEA.Z6.y8B8W5hN6Rj3E', UTC_TIMESTAMP(), UTC_TIMESTAMP()),
('Pedro Oliveira', 'pedro.oliveira@teste.com', '$2a$12$tQv3c1yqBWVHxkd0LHAkCOYz6TtxMeStkKEA.Z6.y8B8W5hN6Rj3F', UTC_TIMESTAMP(), UTC_TIMESTAMP()),
('Ana Costa', 'ana.costa@teste.com', '$2a$12$uQv3c1yqBWVHxkd0LHAkCOYz6TtxMeStkKEA.Z6.y8B8W5hN6Rj3G', UTC_TIMESTAMP(), UTC_TIMESTAMP());