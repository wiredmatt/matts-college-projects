# ObligatorioBD2 - Mateo Carriqui & Robert Ibarra

## General Setup (OPTIONAL)

```sh
cd setup
docker-compose up -d
cd ..
```

Will spin up MSSQL Server + MongoDB with Docker

## SQL - Create database & seed

```sh
./scripts/setup_sql_db.sh
```

Will run all the sql scripts located under the `sql` directory, in the following order:

1. `DDL_CatHotel.sql` - Schema Definition
2. `DDL_Indices_CatHotel.sql` - Indexes
3. `TRIGGERS_CatHotel.sql` - Triggers
4. `TSQL_CatHotel.sql` - Stored Procedures & Functions
5. `VIEWS_CatHotel.sql` - VIews
6. `DML_CatHotel.sql` - Data seeding

If you don't want to run the bash script, you shall execute the scripts in the aforementioned order using sqlcmd, like so:

```sh
sqlcmd -U sa -P Password12345 -i FILE_NAME_HERE.sql # example: sqlcmd -U sa -P Password12345 -i DDL_CatHotel.sql
```

## SQL - Test cases

```sh
./scripts/test_sql_db.sh
```

Will run all the sql scripts located under `tests/cases`.

Otherwise you can inspect them and run them manually like so:

```sh
sqlcmd -U sa -P Password12345 -i tests/cases/ReservarHabitacion/ReservarHabitacion_Exito.sql
```

## NoSQL - Create database and seed

```sh
./scripts/setup_nosql_db.sh
```

Will run the scripts `reset.js` and `a_estructura_reservas.js` under the `nosql` directory.

Otherwise you can manually run:

```sh
mongosh --file "$__NOSQL_ROOT_DIR/reset.js"
mongosh --file "$__NOSQL_ROOT_DIR/a_estructura_reservas.js"
```

## NoSQL - Test cases

Using mongosh, you can run all the `.js` files present in the `nosql` directory.

1. `a_estructura_reservas.js` - This script will create all the collections and also solve the first asked requirement
2. `b_listar_reservas_propietario.js`
3. `c_reservas_peluqueria.js`
4. `d_Suite1_estado.js`
5. `e_reservas_julio.js`

Additionally, you can run `reset.js` and `a_estructura_reservas.js` everytime before you re-test everything.
