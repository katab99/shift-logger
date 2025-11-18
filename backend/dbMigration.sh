#!/bin/bash

if [ $# -eq 0 ]; then
    echo "Error: No argument provided"
    echo "Usage: $0 <argument>"
    exit 1
fi

MIGRATION_NAME = $1

dotnet ef migrations add $MIGRATION_NAME

dotnet ef database update

echo "Done! Migration saved as $MIGRATION_NAME"