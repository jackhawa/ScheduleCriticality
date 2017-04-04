FROM microsoft/mssql-server-linux:latest

ENV ACCEPT_EULA "Y"
ENV SA_PASSWORD "Welcome1234!"

EXPOSE 1433


COPY ./entrypoint.sh /
COPY ./initialize.sh /
COPY ./setup.sql /
RUN chmod +x /entrypoint.sh
RUN chmod +x /initialize.sh
RUN chmod +x /setup.sql

ENTRYPOINT bash /entrypoint.sh

