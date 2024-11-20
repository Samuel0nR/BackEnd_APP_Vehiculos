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

#!/bin/bash

# Instalar .NET SDK (si no está disponible)
if ! command -v dotnet &> /dev/null
then
    echo ".NET no está instalado, instalando..."
    curl -sSL https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh | bash /dev/stdin
    # Actualizar el PATH para que el comando dotnet sea accesible
    export PATH=$PATH:/app/.dotnet
fi
