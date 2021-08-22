# Voxel.WAF.InvoiceCommon.Palladium

La solución se encarga de manejar los objectos comunes a Voxel.WAF.InvoiceApprovalFlow.Palladium, Voxel.WAF.InvoiceApprovalFlow.Cron.Palladium y Voxel.WAF.InvoiceLoadCentre.Palladium.

# Index
- [Requirements](#requirements)
- [Environments](#environments)
  - [Deployers](#deployers)
  - [Application Deployment](#application-deployment)
  - [Application Urls](#application-urls)
  - [Database resources](#database-resources)
- [Distributed tracing](#distributed-tracing)
- [Testing](#testing)
- [Common Objects](#common-objects)
- [Migrations](#migrations)

# Requirements

.Net Core 5.0

# Environments

![Board Producto - Deployment Environments WAF](https://user-images.githubusercontent.com/59824832/129887691-60666162-6f57-4c87-8818-64e9b43e6dec.jpg)

## Database resources

Sqlite Data Base installed on path: C:\sqlite\sqlite-tools\productManual.db

# Testing

To run the tests locally, the environment variable ASPNETCORE_ENVIRONMENT must be equal to Development. This environment variable is set by default visual studio according to the environment in which you deploy (Debug = Development, Release = Production). Sometimes it does not work locally (in production it always works the first time), in this case you have to add a environment variable that is ASPNETCORE_ENVIRONMENT = Development and ** REBOOT !! Visual Studio **.

# Common Objects
  - Product: hace referencia al Cliente de la aplicación, por ejemplo Palladium.
  - Attribute: hace referencia a los establecimientos del cliente.
  - Configuration: agrupa un flujo de trabajo para un conjunto de establecimientos, por ejemplo: el flujo de aprobación de facturas de Palladium.
  
Data Model: ![DataModel](https://github.com/adria-arquimbau/Palfinger.ProductManual/blob/develop/productManual.png)

# Migrations
Todo el modelo de datos está realizado con entity framework. Este modelo de datos se va ir haciendo de modo incremental. Está ubicado dentro del proyecto Voxel.WAF.InvoiceCommon.Palladium.Infrastructure.Data. En Migrations\ProductionsScripts están todos los scripts que van a ser ejecutados en producción, estos hacen referencia a cada migración; por lo cual cuando creamos una migración debemos crear el script correspondiente para producción.

Para ejecutar una migración se puede utilizar el vscode. Todas las instrucciones deben ser ejecutadas dentro del directorio Voxel.WAF.InvoiceCommon.Palladium.Infrastructure.Data.

Más información: https://github.com/VoxelGroup/Voxel.T360/blob/master/README.md

## Rules activas
  Actualmente tenemos tres rules que pueden estar activas en un nivel
  - **LevelRuleFieldType.Required**. Se utiliza para crear una rule que indica que el level es requerido. Para esto hay que crear un registro en LevelRule. Field = 'Required', Condition = '=' y Value = 'true'
  - **LevelRuleFieldType.SubTotal**. Se utiliza para crear una rule que indica un rango de importe en el level. 
  Actualmente tenemos Importe Mínimo, para esto hay que crear un registro en LevelRule. Field = 'Subtotal', Condition = '>=' y Value = '1000.0' (un importe del nivel)
  - **LevelRuleFieldType.Assessment**. Se utiliza para crear una rule que indica que el level posee assessment. Para esto hay que crear un registro en LevelRule. Field = 'Assessment', Condition = '=' y Value = 'true'

## Clean Cache Approval Workflow
Cuando se realiza una actualización en la relación de los usuarios o cecos con el workflow hay que ejecutar un método que limpia el cache del approval workflow, el usuario se tendrá que volver a loguear, de esta forma el usuario podrá ver las facturas de los cecos que se hayan actualizado.
El cache actual de la aplicación es de 5 minutos.

**Integración**
```

curl -X POST "http://intg-iafpalladium-invoiceapprovalflow-palladium-api.lbservice/api/v1/Invoice/cleancache" -H "accept: */*"

```

**Producción**
```

curl -X POST "http://prod-iafpalladium-invoiceapprovalflow-palladium-api.lbservice/api/v1/Invoice/cleancache" -H "accept: */*"

```
