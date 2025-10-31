# -----------------------------
# Этап сборки
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только csproj и восстанавливаем зависимости
COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
RUN dotnet restore "ApiGateway/ApiGateway.csproj"

# Копируем только исходники проекта (без obj/bin)
COPY ["ApiGateway/", "ApiGateway/"]

WORKDIR "/src/ApiGateway"

# Сборка проекта в Release и публикация в папку /app/publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------
# Этап runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копируем только опубликованные файлы
COPY --from=build /app/publish .

# Пробрасываем порт, который слушает приложение
EXPOSE 8080

# Запуск приложения
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
