#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
# COPY ["nuget.config", "."]
COPY ["src/Web/src/Application.Web.csproj", "src/Web/src/"]
COPY ["src/Core/src/Application.Core.csproj", "src/Core/src/"]
COPY ["src/Core.Common/src/Application.Core.Common.csproj", "src/Core.Common/src/"]
COPY ["src/Core.Exercise/src/Application.Core.Exercise.csproj", "src/Core.Exercise/src/"]
COPY ["src/Core.Nutrition/src/Application.Core.Nutrition.csproj", "src/Core.Nutrition/src/"]
COPY ["src/Infrastructure/src/Application.Infrastructure.csproj", "src/Infrastructure/src/"]
RUN dotnet restore "src/Web/src/Application.Web.csproj"
COPY . .
WORKDIR "/src/src/Web/src"
RUN dotnet build "Application.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.Web.dll"]