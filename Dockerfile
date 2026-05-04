FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["WeatherStation.Api/WeatherStation.Api.csproj", "WeatherStation.Api/"]
COPY ["WeatherStation.BusinessLogic/WeatherStation.BusinessLogic.csproj", "WeatherStation.BusinessLogic/"]
COPY ["WeatherStation.DataAccess/WeatherStation.DataAccess.csproj", "WeatherStation.DataAccess/"]
COPY ["WeatherStation.Infrastructure/WeatherStation.Infrastructure.csproj", "WeatherStation.Infrastructure/"]

RUN dotnet restore "WeatherStation.Api/WeatherStation.Api.csproj"

COPY . .

WORKDIR "/src/WeatherStation.Api"
RUN dotnet publish "WeatherStation.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

RUN mkdir -p /app/logs && chmod 777 /app/logs

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "WeatherStation.Api.dll"]