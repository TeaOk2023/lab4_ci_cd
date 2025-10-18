FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
 WORKDIR /src
 COPY *.csproj .
 RUN dotnet restore

 COPY . .
 RUN dotnet publish -c Release -o /app/publish

 FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
 WORKDIR /app
 COPY --from=build /app/publish .
 EXPOSE 80
 CMD ["dotnet", "Blazor_lab1.dll", "--urls", "http://0.0.0.0:80"]
