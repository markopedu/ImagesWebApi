

# MS SQL SERVER 
- docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Secret!123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest

# default 
- MSSQL_PID=<your_product_id | edition_name> (default: Developer)

Server: 0.0.0.0:1433
username: sa 
pw: Secret!123
db: SamuraiAppData

# MIGRATIONS
    - dotnet ef migrations add init -s ../ConsoleApp

    - install: dotnet ef = dotnet tool install --global dotnet-ef 

    - dotnet ef migrations add newrelationships  -s ../ConsoleApp

    - dotnet ef database update -s ../ConsoleApp     

