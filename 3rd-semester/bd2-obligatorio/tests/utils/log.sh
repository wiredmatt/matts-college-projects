#!/usr/bin/env bash

# Define color codes
RED='\033[0;31m'
GREEN='\033[0;32m'
BLUE='\033[0;34m'
CLEAR_BLUE='\033[0;94m'
NC='\033[0m' # No Color

# Log a header
log_header() {
  printf "${BLUE}==========  %s  ==========${NC}\n" "$@" >&2
}

# Log a message
log_msg() {
  printf "${CLEAR_BLUE}%s${NC}\n" "$@" >&2
}

# Log a success message
log_success() {
  printf "${GREEN}%s${NC}\n" "$@" >&2
}

# Log a failure message
log_failure() {
  printf "${RED}%s${NC}\n" "$@" >&2
}