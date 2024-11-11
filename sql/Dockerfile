# Use a imagem oficial do Microsoft SQL Server 2019
FROM mcr.microsoft.com/mssql/server:2019-latest

# Defina as variáveis de ambiente necessárias
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=p@ssw0rd

# Mude para o usuário root para instalar pacotes
USER root

# Instale o mssql-tools e outras dependências
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools unixodbc-dev && \
    ln -sfn /opt/mssql-tools/bin/sqlcmd /usr/bin/sqlcmd && \
    ln -sfn /opt/mssql-tools/bin/bcp /usr/bin/bcp && \
    rm -rf /var/lib/apt/lists/*

# Exponha a porta padrão do SQL Server
EXPOSE 1433

# Copie os scripts para o container
COPY init.sql /init.sql
COPY entrypoint.sh /entrypoint.sh

# Defina as permissões corretas
RUN chmod +x /entrypoint.sh

# Volte para o usuário mssql
USER mssql

# Defina o entrypoint
ENTRYPOINT ["/entrypoint.sh"]

