# Obligatorio P3

## Setup

0. Install dotnet, docker, docker-compose and dotnet-ef

```bash
# get dotnet wherever you prefer, in my case I use asdf: https://github.com/hensou/asdf-dotnet
asdf plugin add dotnet # install dotnet plugin through asdf - optional, only if you want to use asdf to manage dotnet
asdf install dotnet latest
asdf global dotnet latest


dotnet tool install -g dotnet-ef

sudo dnf install docker docker-compose # make sure to set up docker to run without sudo (https://docs.docker.com/engine/install/linux-postinstall/)
```

1. Spin up a mssql server instance. Can be achieved by running the following command

```
docker-compose up -d
```

2. Apply the database migrations & seed the database

```
cd WebApi
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=127.0.0.1;Database=ObligatorioMC;User Id=sa;Password=Password12345;Trusted_Connection=True;TrustServerCertificate=True;Integrated Security=False" # Connection string for your sql server database.
# You can otherwise create an appsettings.json file with the connection string (not recommended).
cd ..


dotnet ef database update --startup-project WebApi --project LogicaAccesoDatos
```

4. Start the web application

```
dotnet run --project WebApi
```

## Generating Migrations

To generate a new migration, use the following command:

```sh
dotnet ef migrations add <MigrationName> --project LogicaAccesoDatos --startup-project WebApi
```

## Deploying Migrations

To deploy migrations, use the following command:

```sh
dotnet ef database update --startup-project WebApi --project LogicaAccesoDatos
```

## Extras

- [sqlcmd & bcp](https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-red-hat?view=sql-server-ver16&tabs=rhel8#tools) - install in your system

- check if sql server is running

```bash
sqlcmd -C -S 127.0.0.1 -U sa -P Password12345
```

- [mermerd](https://github.com/KarnerTh/mermerd) - place it under `./bin/mermerd`
- [update-readme](./bin/update-readme/update-readme.cjs) - included
- [gen-doc](./bin/gen-doc/README.md) - included
- [bruno](https://www.usebruno.com/) or [postman](https://www.postman.com/) - install in your system

## ER Diagram

```mermaid
erDiagram
    AtletaDisciplina {
        int IdAtleta PK,FK
        int IdDisciplina PK,FK
    }

    AtletaEvento {
        int IdAtleta PK,FK
        int IdEvento PK,FK
        float Puntaje
    }

    Atletas {
        nvarchar Apellido
        int Id PK
        int IdPais FK
        nvarchar Nombre
        nvarchar Sexo
    }

    Disciplinas {
        int Ano
        int Id PK
        nvarchar Nombre
    }

    Eventos {
        date FechaFin
        date FechaInicio
        int Id PK
        int IdDisciplina FK
        nvarchar Nombre
    }

    Paises {
        int CantidadHabitantes
        nvarchar Delegado_Nombre
        nvarchar Delegado_Telefono
        int Id PK
        nvarchar Nombre
    }

    Usuarios {
        nvarchar Contrasena
        nvarchar Email
        datetime2 FechaAlta
        int Id PK
        int IdCreador FK
        nvarchar Rol
    }

    AtletaDisciplina }o--|| Atletas : "IdAtleta"
    AtletaDisciplina }o--|| Disciplinas : "IdDisciplina"
    AtletaEvento }o--|| Atletas : "IdAtleta"
    AtletaEvento }o--|| Eventos : "IdEvento"
    Atletas }o--|| Paises : "IdPais"
    Eventos }o--|| Disciplinas : "IdDisciplina"
    Usuarios }o--|| Usuarios : "IdCreador"

```

Generated using [mermerd](https://github.com/KarnerTh/mermerd)

```bash
mkdir _generated
# note: the binary included in this repo is for linux_arm64, if you are using a different architecture, you will need to compile it yourself, or run the go program from the source code.
./bin/mermerd/mermerd -c "sqlserver://sa:Password12345@127.0.0.1:1433?database=ObligatorioMC" -o _generated/er.mmd
./bin/update-readme/update-readme.cjs # this will update the README.md file with the generated ER diagram. There's currently no support for embedding and displaying an .mmd file directly in a markdown file.
```

## HTTP Tests

Import `./Peticiones/321268Obligatorio/321268Obligatorio_postman.json` into Postman, or `./Peticiones/321268Obligatorio/321268Obligatorio_bruno.json` into Bruno.
Only the requested endpoint (`/Atletas/Eventos/:id`) was added to the requests collection.
