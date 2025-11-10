# Базовый образ среды выполнения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookServiceDemo/BookServiceDemo.csproj", "BookServiceDemo/"]
RUN dotnet restore "BookServiceDemo/BookServiceDemo.csproj"
COPY . .
WORKDIR "/src/BookServiceDemo"
RUN dotnet build "BookServiceDemo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookServiceDemo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookServiceDemo.dll"]
