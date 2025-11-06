# -----------------------------
# Этап runtime (базовый образ)
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# -----------------------------
# Этап сборки
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["KafkaConsumerWorker/KafkaConsumerWorker.csproj", "KafkaConsumerWorker/"]
RUN dotnet restore "KafkaConsumerWorker/KafkaConsumerWorker.csproj"

COPY ["KafkaConsumerWorker/", "KafkaConsumerWorker/"]
WORKDIR "/src/KafkaConsumerWorker"
RUN dotnet publish "KafkaConsumerWorker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------
# Этап финального образа
# -----------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KafkaConsumerWorker.dll"]
