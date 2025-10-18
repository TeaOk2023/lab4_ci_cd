FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /src
COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /app

WORKDIR /app
EXPOSE 80

CMD ["dotnet", "Blazor_lab1.dll", "--urls", "http://0.0.0.0:80"]
