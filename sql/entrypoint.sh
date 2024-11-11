#!/bin/bash
/opt/mssql/bin/sqlservr &

# Aguarde o SQL Server iniciar
sleep 15

# Execute o script de inicialização
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /init.sql

# Mantenha o container em execução
tail -f /dev/null

