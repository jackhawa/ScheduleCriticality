FROM microsoft/mssql-server-linux:latest

EXPOSE 1433

COPY ./entrypoint.sh /
COPY ./initialize.sh /
COPY ./setup.sql /

RUN chmod +x /entrypoint.sh
RUN chmod +x /initialize.sh
RUN chmod +x /setup.sql

ENTRYPOINT bash /entrypoint.sh

