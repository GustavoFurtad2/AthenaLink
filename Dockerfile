FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY AthenaLink.sln ./
COPY Client/Client.csproj Client/
COPY Server/Server.csproj Server/
COPY Shared/Shared.csproj Shared/

RUN dotnet restore Server/Server.csproj

COPY . .

RUN dotnet build Server/Server.csproj -c Release -o /app/build

RUN dotnet publish Server/Server.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "Server.dll"]