#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BevCapital.Stocks.API/BevCapital.Stocks.API.csproj", "BevCapital.Stocks.API/"]
COPY ["BevCapital.Stocks.Application/BevCapital.Stocks.Application.csproj", "BevCapital.Stocks.Application/"]
COPY ["BevCapital.Stocks.Domain/BevCapital.Stocks.Domain.csproj", "BevCapital.Stocks.Domain/"]
COPY ["BevCapital.Stocks.Domain.Core/BevCapital.Stocks.Domain.Core.csproj", "BevCapital.Stocks.Domain.Core/"]
COPY ["BevCapital.Stocks.Data/BevCapital.Stocks.Data.csproj", "BevCapital.Stocks.Data/"]
COPY ["BevCapital.Stocks.Infra/BevCapital.Stocks.Infra.csproj", "BevCapital.Stocks.Infra/"]
COPY ["BevCapital.Stocks.Background/BevCapital.Stocks.Background.csproj", "BevCapital.Stocks.Background/"]


RUN dotnet restore "BevCapital.Stocks.API/BevCapital.Stocks.API.csproj"
COPY . .
WORKDIR "/src/BevCapital.Stocks.API"
RUN dotnet build "BevCapital.Stocks.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BevCapital.Stocks.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BevCapital.Stocks.API.dll"]