sleep 90s
if [ ! -f "/var/opt/mssql/data/CEP.mdf" ]
then
    echo "SQLINUX >>>> File does not exists"
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Welcome1234! -i setup.sql
fi
