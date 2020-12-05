# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
EXPOSE 80 # docker with .NET seems to use port 80 regardless of what we specify here
ENTRYPOINT ["dotnet", "WebCrawler.dll"]

# WORKDIR is the place in the container where actions are will be performed. Copy files to /build, then dotnet restore into there.
