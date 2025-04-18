FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SketchTogether.API/SketchTogether.API.csproj", "SketchTogether.API/"]
COPY ["SketchTogether.Application/SketchTogether.Application.csproj", "SketchTogether.Application/"]
COPY ["SketchTogether.Infrastructure/SketchTogether.Infrastructure.csproj", "SketchTogether.Infrastructure/"]
COPY ["SketchTogether.Domain/SketchTogether.Domain.csproj", "SketchTogether.Domain/"]
RUN dotnet restore "SketchTogether.API/SketchTogether.API.csproj"
COPY . .
WORKDIR "/src/SketchTogether.API"
RUN dotnet build "SketchTogether.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SketchTogether.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SketchTogether.API.dll"]
