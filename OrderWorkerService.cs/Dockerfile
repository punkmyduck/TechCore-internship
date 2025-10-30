# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем весь код из корня решения
COPY . .

# Переходим в проект воркера
WORKDIR /src/OrderWorkerService.cs

# Восстанавливаем зависимости и публикуем
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderWorkerService.cs.dll"]