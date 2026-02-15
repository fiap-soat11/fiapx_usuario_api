USE `fiap_usuarios`;

-- Tabela de Usuários
CREATE TABLE IF NOT EXISTS `Usuarios` (
    `Id` INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `Nome` VARCHAR(200) NOT NULL,
    `Email` VARCHAR(200) NOT NULL UNIQUE,
    `Password` VARCHAR(255) NOT NULL,
    `CreatedAt` DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6),
    `UpdatedAt` DATETIME(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) ON UPDATE CURRENT_TIMESTAMP(6),
    
    INDEX `IX_Usuarios_Email` (`Email`),
    INDEX `IX_Usuarios_CreatedAt` (`CreatedAt`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;