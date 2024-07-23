#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/servicos/Fiap.Ingresso.Usuario.API/Fiap.Ingresso.Usuario.API.csproj", "src/servicos/Fiap.Ingresso.Usuario.API/"]
COPY ["src/building blocks/Fiap.Ingresso.WebAPI.Core/Fiap.Ingresso.WebAPI.Core.csproj", "src/building blocks/Fiap.Ingresso.WebAPI.Core/"]
RUN dotnet restore "src/servicos/Fiap.Ingresso.Usuario.API/Fiap.Ingresso.Usuario.API.csproj"
COPY . .
WORKDIR "/src/src/servicos/Fiap.Ingresso.Usuario.API"
RUN dotnet build "Fiap.Ingresso.Usuario.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fiap.Ingresso.Usuario.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fiap.Ingresso.Usuario.API.dll"]