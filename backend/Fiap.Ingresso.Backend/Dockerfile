#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./Fiap.Ingresso.Evento.API/Fiap.Ingresso.Evento.API.csproj", "src/servicos/Fiap.Ingresso.Evento.API/"]
COPY ["./Fiap.Ingresso.WebAPI.Core/Fiap.Ingresso.WebAPI.Core/", "src/building blocks/Fiap.Ingresso.WebAPI.Core/"]
RUN dotnet restore "src/servicos/Fiap.Ingresso.Evento.API/Fiap.Ingresso.Evento.API.csproj"
COPY . .
WORKDIR "/src/src/servicos/Fiap.Ingresso.Evento.API"
RUN dotnet build "Fiap.Ingresso.Evento.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fiap.Ingresso.Evento.API.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Ingresso.Evento.API.dll"]