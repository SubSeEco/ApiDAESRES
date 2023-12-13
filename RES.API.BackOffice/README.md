# API DAES - RES

API REST de comunicacion BD backoffice con cliente de RES

## Instalación Docker

- utilizando docker se construye imagen para contenedor docker (imagen oficial de windows para aplicaciones dotnet - debian bullseye)

## Construcción de la imagen

Estando en este directorio (donde el Dockerfile y RES.API.BackOffice.csproj se ubican), se ejecuta el Dockerfile para generar la imagen

```bash
docker build -t daes-api-backoffice -f Dockerfile . # 'daes-api-backoffice' va a ser el nombre de la imagen que se genera
```

La imagen queda en la librería de docker lista para la ejecución.

## Ejecución del contenedor 

Habiéndose construido la imagen, se mapeando el puerto al cual se le mandaran los requisitos http

```bash
docker run -p 8080:80 daes-api-backoffice # puerto 80 es de la app en el contenedor, 8080 quedaria para el servidor docker
```

