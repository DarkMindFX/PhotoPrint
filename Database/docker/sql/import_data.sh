echo START import_data.sh 
sleep 30
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "yourStrong(!)Password" -d master -i /sql/create_db.sql
echo END import_data.sh 
