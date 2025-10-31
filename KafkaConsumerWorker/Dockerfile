# -----------------------------
# Этап runtime (базовый образ)
# -----------------------------
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# -----------------------------
# Этап сборки
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только csproj и восстанавливаем зависимости
COPY ["KafkaConsumerWorker/KafkaConsumerWorker.csproj", "KafkaConsumerWorker/"]
RUN dotnet restore "KafkaConsumerWorker/KafkaConsumerWorker.csproj"

# Копируем исходники проекта
COPY ["KafkaConsumerWorker/", "KafkaConsumerWorker/"]
WORKDIR "/src/KafkaConsumerWorker"

# Сборка и публикация проекта в Release
RUN dotnet publish "KafkaConsumerWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------
# Этап финального образа
# -----------------------------
FROM base AS final
WORKDIR /app

# Копируем только опубликованные файлы
COPY --from=build /app/publish .

# Запуск KafkaConsumerWorker
ENTRYPOINT ["dotnet", "KafkaConsumerWorker.dll"]
