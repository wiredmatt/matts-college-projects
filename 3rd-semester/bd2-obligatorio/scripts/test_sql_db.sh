#!/bin/bash

__UTILS_ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../tests/utils" && pwd)"
source $__UTILS_ROOT_DIR/assert.sh
source $__UTILS_ROOT_DIR/log.sh

__TESTS_CASES_ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")/../tests/cases" && pwd)"

RESET_DB_SCRIPT="$(dirname "${BASH_SOURCE[0]}")/setup_sql_db.sh"

log_header "Ejecutando tests"

# Find all .sql files and sort them
sql_files=$(find "$__TESTS_CASES_ROOT_DIR" -type f -name "*.sql" | sort -r)

for sql_file in $sql_files; do
    eval $RESET_DB_SCRIPT > /dev/null 2>&1
    log_success "[DB RESET] OK"

    case_name="      [CASO] $(basename "$sql_file")"
    log_msg $case_name

    output=$(sqlcmd -U sa -P Password12345 -i "$sql_file")

    result=$(echo "$output" | tail -n 3 | head -n 1 | xargs)

    result_status=$(echo "$result" | awk -F': ' '{print $1}')

    assert_eq "ESPERADO" "$result_status"

    if [ $? -eq 0 ]; then
        log_success "       > [ ✔ ] $result"
    else
        log_failure "       > [ ✖ ] $result"
    fi
done

eval $RESET_DB_SCRIPT > /dev/null 2>&1

exit 0