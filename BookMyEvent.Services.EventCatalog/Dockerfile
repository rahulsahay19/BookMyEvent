#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BookMyEvent.Services.EventCatalog/BookMyEvent.Services.EventCatalog.csproj", "BookMyEvent.Services.EventCatalog/"]
COPY ["BookMyEvent.Integration.MessagingBus/BookMyEvent.Integration.MessagingBus.csproj", "BookMyEvent.Integration.MessagingBus/"]
COPY ["BookMyEvent.Integration.Messages/BookMyEvent.Integration.Messages.csproj", "BookMyEvent.Integration.Messages/"]
RUN dotnet restore "BookMyEvent.Services.EventCatalog/BookMyEvent.Services.EventCatalog.csproj"
COPY . .
WORKDIR "/src/BookMyEvent.Services.EventCatalog"
RUN dotnet build "BookMyEvent.Services.EventCatalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookMyEvent.Services.EventCatalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookMyEvent.Services.EventCatalog.dll"]