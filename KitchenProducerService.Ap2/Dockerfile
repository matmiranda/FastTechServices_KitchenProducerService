FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["KitchenProducerService.Api/KitchenProducerService.Api.csproj", "KitchenProducerService.Api/"]
COPY ["KitchenProducerService.Application/KitchenProducerService.Application.csproj", "KitchenProducerService.Application/"]
COPY ["KitchenProducerService.Domain/KitchenProducerService.Domain.csproj", "KitchenProducerService.Domain/"]
COPY ["KitchenProducerService.Infrastructure/KitchenProducerService.Infrastructure.csproj", "KitchenProducerService.Infrastructure/"]
RUN dotnet restore "KitchenProducerService.Api/KitchenProducerService.Api.csproj"
COPY . .
WORKDIR "/src/KitchenProducerService.Api"
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "KitchenProducerService.Api.dll"]
