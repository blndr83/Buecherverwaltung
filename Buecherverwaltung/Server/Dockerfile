#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Buecherverwaltung/Server/Buecherverwaltung.Server.csproj", "Buecherverwaltung/Server/"]
COPY ["Buecherverwaltung.Server.Infrastructure/Buecherverwaltung.Server.Infrastructure.csproj", "Buecherverwaltung.Server.Infrastructure/"]
COPY ["Buechervwaltung.Server.Core/Buecherverwaltung.Server.Core.csproj", "Buechervwaltung.Server.Core/"]
COPY ["Buecherverwaltung.Server.Application/Buecherverwaltung.Server.Application.csproj", "Buecherverwaltung.Server.Application/"]
COPY ["Buecherverwaltung.Shared/Buecherverwaltung.Shared.csproj", "Buecherverwaltung.Shared/"]
COPY ["Buecherverwaltung/Client/Buecherverwaltung.Client.csproj", "Buecherverwaltung/Client/"]
RUN dotnet restore "Buecherverwaltung/Server/Buecherverwaltung.Server.csproj"
COPY . .
WORKDIR "/src/Buecherverwaltung/Server"
RUN dotnet build "Buecherverwaltung.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Buecherverwaltung.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Buecherverwaltung.Server.dll"]
