#!/bin/bash

# Verificar si el directorio .dotnet ya existe
if [ -d "/app/.dotnet" ]; then
  # Eliminar el directorio .dotnet
  rm -rf /app/.dotnet
fi