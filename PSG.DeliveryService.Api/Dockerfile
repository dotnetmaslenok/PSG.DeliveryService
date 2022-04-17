FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PSG.DeliveryService.Api/PSG.DeliveryService.Api.csproj", "PSG.DeliveryService.Api/"]
COPY ["PSG.DeliveryService.Application/PSG.DeliveryService.Application.csproj", "PSG.DeliveryService.Application/"]
COPY ["PSG.DeliveryService.Domain/PSG.DeliveryService.Domain.csproj", "PSG.DeliveryService.Domain/"]
COPY ["PSG.DeliveryService.Infrastructure/PSG.DeliveryService.Infrastructure.csproj", "PSG.DeliveryService.Infrastructure/"]
RUN dotnet restore "PSG.DeliveryService.Api/PSG.DeliveryService.Api.csproj" --disable-parallel
COPY . .
WORKDIR "/src/PSG.DeliveryService.Api"
RUN dotnet build "PSG.DeliveryService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PSG.DeliveryService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PSG.DeliveryService.Api.dll"]
