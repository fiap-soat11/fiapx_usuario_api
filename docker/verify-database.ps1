# Script para verificar a conexão com o banco
$mysql_host = "localhost"
$mysql_port = "3336"
$mysql_user = "user_fiap"
$mysql_password = "pass_fiap"
$database_name = "fiap_usuarios"

Write-Host "Verificando conexão com o banco de dados..." -ForegroundColor Yellow

$command = "SELECT 'Connection OK' as Status;"

mysql -h $mysql_host -P $mysql_port -u $mysql_user -p$mysql_password -D $database_name -e $command

if ($LASTEXITCODE -eq 0) {
    Write-Host "Conexão OK!" -ForegroundColor Green
    
    # Mostrar tabelas
    Write-Host "`nTabelas existentes:" -ForegroundColor Yellow
    mysql -h $mysql_host -P $mysql_port -u $mysql_user -p$mysql_password -D $database_name -e "SHOW TABLES;"
} else {
    Write-Host "Erro na conexão!" -ForegroundColor Red
}