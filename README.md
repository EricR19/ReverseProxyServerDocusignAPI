# ReverseProxyServerDocusignAPI




## Tabla de Contenidos
- [Descripción](#descripción)
- [Instalación](#instalación)
- [API Reference](#APIReference)
- [Authors](Authors)

## Descripción

Este proyecto implementa un servidor proxy inverso (Reverse Proxy API) como solución para gestionar solicitudes entre clientes y múltiples servidores backend. Desarrollado en .NET, combina conceptos de diseño API con prácticas avanzadas de distribución de tráfico y optimización.
## Instalación

1- Clonar el repositorio

```bash
  git clone https://github.com/EricR19/ReverseProxyServerDocusignAPI.git
cd ReverseProxyServerDocusignAPI
```

2- Restaurar dependencias

```bash
  dotnet restore
```

3- Configuración de credenciales

Este proyecto utiliza la API de DocuSign, y requiere un archivo appsettings.json que contiene las credenciales necesarias para la conexión. Por razones de seguridad, este archivo no está incluido en el repositorio.

Pasos para configurarlo:

Crea un archivo llamado appsettings.json en la raíz del proyecto (o en la ubicación especificada en el código).


```json
{
  "DocuSign": {
    "AccountId": "yourAccountId",
    "IntegratorKey": "yourIntegrationKey",
    "UserId": "yourUserID",
    "RsaPrivateKey": "yourPrivateKey",
    "BasePath": "https://demo.docusign.net/restapi",
    "TemplateId":"yourTemplateId"
  }
}
```
Puedes seguir esta documentacion para mas informacion acerca de la creacion de lo anterior en [DocuSign](https://developers.docusign.com/platform/auth/jwt/) ademas para crear una cuenta Developer, acceder a este [enlace](https://www.docusign.com/developers/sandbox/?postActivateUrl=https://developers.docusign.com/&_gl=1*1ogxljg*_gcl_au*NTMzMjM3MDU0LjE3MzMyODc3Mjg.)

4- Ejecutar el proyecto

```bash
  dotnet run
```

***Importante***

Este proyecto toma un templateId creado directamente en la UI de Docusign, es recomendable que cree uno con casillas iguales a los parametros que vera en la seccion de API Reference, Post Envia un envelope.
Seguir esta documentacion para la creacion del [template](https://support.docusign.com/s/document-item?language=en_US&rsc_301&bundleId=ulp1643236876813&topicId=gso1578456465211.html&_LANG=enus)
## API Reference

#### Post Envia un envelope

```http
  Post /api/Envelope/send-envelope
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `recipientEmail` | `string` | **Required**. Email|
| `recipientName` | `string` | **Required**. Name|
| `montoPrestamo` | `int` | **Required**. Monto|
| `plazoPrestamo` | `string` | **Required**. Plazo|
| `tasaInteres` | `string` | **Required**. Interes|
| `coutaMensual` | `int` | **Required**. Coutas|

#### Get Informacion del envelope

```http
  GET /api/envelope/${envelopeId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |




## Authors

- [@EricR19](https://github.com/EricR19)

