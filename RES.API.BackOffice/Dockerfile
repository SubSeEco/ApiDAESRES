FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copia el archivo csproj e restaura las dependencias
COPY . ./
RUN dotnet restore

# Copia el resto del código y compila la aplicación
RUN dotnet publish -c Release -o out

# Crea la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0
# bajar version de TLS para la coneccion con SQL Server
RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/' /etc/ssl/openssl.cnf \
&& sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/' /etc/ssl/openssl.cnf

WORKDIR /app
COPY --from=build /app/out .

# Expone el puerto en el que la aplicación escucha
EXPOSE 80
ENTRYPOINT ["dotnet", "RES.API.BackOffice.dll"]