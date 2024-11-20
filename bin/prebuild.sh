#!/bin/bash

echo "Ejecutando script prebuild"

# Verificar si el directorio .dotnet ya existe
if [ -d "/app/.dotnet" ]; then
  echo "Eliminando directorio /app/.dotnet"
  # Eliminar el directorio .dotnet
  rm -rf /app/.dotnet
else
  echo "El directorio .dotnet no existe"
fi