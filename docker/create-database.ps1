# Script para criar o banco de dados manualmente se necessário
$mysql_host = "localhost"
$mysql_port = "3336"
$mysql_user = "root"
$mysql_password = "root_fiap_2026"
$database_name = "fiap_usuarios"

Write-Host "Criando database $database_name..." -ForegroundColor Yellow

$command = "CREATE DATABASE IF NOT EXISTS ``$database_name`` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;"

mysql -h $mysql_host -P $mysql_port -u $mysql_user -p$mysql_password -e $command

if ($LASTEXITCODE -eq 0) {
    Write-Host "Database criado com sucesso!" -ForegroundColor Green
} else {
    Write-Host "Erro ao criar database!" -ForegroundColor Red
}