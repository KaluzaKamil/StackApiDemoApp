echo "start entrypoint"
dotnet StackApiDemo.dll
sleep 15s
/opt/mssql-tools18/bin/sqlcmd -S sql -U sa -P MyPass@word -d master -i DbInitScripts/DbInit.sql
echo "end entrypoint"
