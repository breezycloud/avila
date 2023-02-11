FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
RUN dotnet workload install wasm-tools
COPY . .
RUN dotnet restore

COPY . .
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

#final build
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Uzhii.Store.Server.dll

FROM nginx:alpine
WORKDIR /var/www/web
COPY --from=publish /app/output/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
EXPOSE 443