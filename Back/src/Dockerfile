#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fullstack.Api/Fullstack.API.csproj", "Fullstack.Api/"]
COPY ["Fullstack.Application/Fullstack.Application.csproj", "Fullstack.Application/"]
COPY ["Fullstack.Persistence/Fullstack.Persistence.csproj", "Fullstack.Persistence/"]
COPY ["Fullstack.Domain/Fullstack.Domain.csproj", "Fullstack.Domain/"]
RUN dotnet restore "Fullstack.Api/Fullstack.API.csproj"
COPY . .
WORKDIR "/src/Fullstack.Api/Resources/"
WORKDIR "/src/Fullstack.Api"

RUN dotnet build "Fullstack.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fullstack.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fullstack.API.dll"]