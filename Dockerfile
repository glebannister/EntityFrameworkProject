# Stage 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish

#build
WORKDIR /EntityFrameworkProject
COPY ["GlobalMarket.API/*", "GlobalMarket.API/"]
COPY ["GlobalMarket.Core/*", "GlobalMarket.Core/"]
RUN dotnet publish 'GlobalMarket.API' -c Release -o /app/publish

# Stage 2: Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GlobalMarket.API.dll"]