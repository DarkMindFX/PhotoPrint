FROM mcr.microsoft.com/mssql/server:2019-CU8-ubuntu-16.04

USER root

ENV SA_PASSWORD PhotoPrint2021!SaAccount
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

COPY ./sql /sql

RUN chmod +x /sql/import_data.sh

USER mssql

EXPOSE 1433

ENTRYPOINT /bin/bash /sql/entrypoint.sh