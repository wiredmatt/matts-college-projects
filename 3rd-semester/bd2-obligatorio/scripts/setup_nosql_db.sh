#!/bin/bash

__NOSQL_ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../nosql" && pwd)"


mongosh --file "$__NOSQL_ROOT_DIR/reset.js"
mongosh --file "$__NOSQL_ROOT_DIR/a_estructura_reservas.js"