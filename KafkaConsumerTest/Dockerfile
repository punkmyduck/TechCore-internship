# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["KafkaConsumerTest/KafkaConsumerTest.csproj", "KafkaConsumerTest/"]
RUN dotnet restore "KafkaConsumerTest/KafkaConsumerTest.csproj"

COPY . .
WORKDIR "/src/KafkaConsumerTest"
RUN dotnet publish "./KafkaConsumerTest.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

# Create non-root user (best-effort, adduser/useradd may differ between images)
RUN useradd -m appuser || adduser --disabled-password --gecos "" appuser || true

COPY --from=build /app/publish .
RUN chown -R appuser:appuser /app

USER appuser

ENTRYPOINT ["dotnet", "KafkaConsumerTest.dll"]