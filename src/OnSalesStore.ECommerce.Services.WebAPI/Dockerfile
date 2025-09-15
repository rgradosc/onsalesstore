# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OnSalesStore.ECommerce.Services.WebAPI/OnSalesStore.ECommerce.Services.WebAPI.csproj", "src/OnSalesStore.ECommerce.Services.WebAPI/"]
COPY ["src/OnSalesStore.ECommerce.Application.DTO/OnSalesStore.ECommerce.Application.DTO.csproj", "src/OnSalesStore.ECommerce.Application.DTO/"]
COPY ["src/OnSalesStore.ECommerce.Application.Interfaces/OnSalesStore.ECommerce.Application.Interfaces.csproj", "src/OnSalesStore.ECommerce.Application.Interfaces/"]
COPY ["src/OnSalesStore.ECommerce.Domain/OnSalesStore.ECommerce.Domain.csproj", "src/OnSalesStore.ECommerce.Domain/"]
COPY ["src/OnSalesStore.ECommerce.Transversal.Common/OnSalesStore.ECommerce.Transversal.Common.csproj", "src/OnSalesStore.ECommerce.Transversal.Common/"]
COPY ["src/OnSalesStore.ECommerce.Application.UseCases/OnSalesStore.ECommerce.Application.UseCases.csproj", "src/OnSalesStore.ECommerce.Application.UseCases/"]
COPY ["src/OnSalesStore.ECommerce.Infrastructure/OnSalesStore.ECommerce.Infrastructure.csproj", "src/OnSalesStore.ECommerce.Infrastructure/"]
COPY ["src/OnSalesStore.ECommerce.Persistence/OnSalesStore.ECommerce.Persistence.csproj", "src/OnSalesStore.ECommerce.Persistence/"]
RUN dotnet restore "./src/OnSalesStore.ECommerce.Services.WebAPI/OnSalesStore.ECommerce.Services.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/OnSalesStore.ECommerce.Services.WebAPI"
RUN dotnet build "./OnSalesStore.ECommerce.Services.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OnSalesStore.ECommerce.Services.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnSalesStore.ECommerce.Services.WebAPI.dll"]