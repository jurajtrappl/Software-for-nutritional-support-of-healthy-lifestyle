# Software for nutritional support of healthy lifestyle

Bachelor thesis at the FMP at Charles University in Prague. Study branch - Programming & software systems.

## Abstract

Health care should be the most important thing in life. Health problems do not manifest themselves until
later, often when they are irreversible. Our thesis seeks to prevent it by creating a tool to support it. The aim
of this thesis is to implement a web application providing a nutritional plan consisting of a meal plan,
drinking regime and physical activity. We present knowledge of the principles of proper nutrition and
modern requirements of web applications. A separate part is devoted to the design of an algorithm for
creating foods that meet the optimal ratio of macronutrients. We present the possibility of exporting content
to calendar platforms. Part of this thesis is responsive design and protection against scripting attacks. Given
our thesis, we believe that application and adherence to its plans is important in promoting health

## Requirements

- **.NET Command Line Interface** (CLI) via **.NET 5.0 SDK**: Follow https://dotnet.microsoft.com/download/dotnet/5.0.
- **Docker 17.06** or later of the **Docker client**. Follow https://www.docker.com/products/docker-desktop.
  - To set up features for Docker Desktop to function correctly follow the section *Virtualization* (https://docs.docker.com/docker-for-windows/troubleshoot/).

## How to deploy

### Generate certificate

We will use self-signed development certificates for hosting pre-built images over localhost.

Generate certificate and configure local machine (using *cmd*):

```
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\{NAME}.pfx -p {PASSWORD}
dotnet dev-certs https --trust
```

Replace `{NAME}` with a file name of the certificate and `{PASSWORD}` with a password. For `PowerShell` use `$env:USERPROFILE` instead of the `%USERPROFILE%`.

### Pull the image

The image is for linux/amd64 os/arch:

```
docker pull jurajtrappl/applicationweb
```

### Windows with Linux container

Make sure to configure the Docker for Linux containers (system tray/notification area -> right click on the Docker icon -> Switch to Linux containers).

Write the following to start the container with ASP.NET Core configured for HTTPS:

```
docker run --rm -it -p 8000:80 -p 8001:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="{PASSWORD}" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/{NAME}.pfx -v $env:USERPROFILE\.aspnet\https:/https/ jurajtrappl/applicationweb
```

Replace `{NAME}` with the file name of the certificate and `{PASSWORD}` with the password (both from the previous step). For `PowerShell` use `$env:USERPROFILE` instead of the `%USERPROFILE%`.

You can replace ports 8000 and 8001 with any other ports. Make sure that you then replace port 8001 (ASPNETCORE_HTTPS_PORT) from the command with the correct port.

### Webpage

Open a browser and paste the following URL:

```
https://localhost:8081
```

All http requests are redirected to HTTPS.
