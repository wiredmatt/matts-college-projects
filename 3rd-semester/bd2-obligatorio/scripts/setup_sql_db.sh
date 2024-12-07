#!/bin/bash

SHOW_OUTPUT=${SHOW_OUTPUT:-false}

__SQL_ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../sql" && pwd)"

if [ "$SHOW_OUTPUT" = true ]; then
    OUTPUT_REDIRECT=""
else
    OUTPUT_REDIRECT="> /dev/null 2>&1"
fi

echo "Eliminando CatHotel DB..."
eval "sqlcmd -U sa -P Password12345 -Q 'DROP DATABASE IF EXISTS CatHotel' -C $OUTPUT_REDIRECT" # drop db
echo "OK!"

echo "Creando CatHotel DB..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/DDL_CatHotel.sql -C $OUTPUT_REDIRECT" # schema def
echo "OK!"

echo "Creando Indices..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/DDL_Indices_CatHotel.sql -C $OUTPUT_REDIRECT" # indexes
echo "OK!"

echo "Creando Triggers..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/TRIGGERS_CatHotel.sql -C $OUTPUT_REDIRECT" # triggers
echo "OK!"

echo "Creando Stored Procedures y Functions..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/TSQL_CatHotel.sql -C $OUTPUT_REDIRECT" # stored procedures and functions
echo "OK!"

echo "Creando Views..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/VIEWS_CatHotel.sql -C $OUTPUT_REDIRECT" # views
echo "OK!"

echo "Precargando CatHotel DB..."
eval "sqlcmd -U sa -P Password12345 -i $__SQL_ROOT_DIR/DML_CatHotel.sql -C $OUTPUT_REDIRECT" # seed
echo "OK!"