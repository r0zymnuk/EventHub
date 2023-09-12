#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventHub.WebUI/EventHub.WebUI.csproj", "EventHub.WebUI/"]
COPY ["EventHub.Application/EventHub.Application.csproj", "EventHub.Application/"]
COPY ["EventHub.Domain/EventHub.Domain.csproj", "EventHub.Domain/"]
COPY ["EventHub.Infrastructure/EventHub.Infrastructure.csproj", "EventHub.Infrastructure/"]
RUN dotnet restore "EventHub.WebUI/EventHub.WebUI.csproj"
COPY . .
WORKDIR "/src/EventHub.WebUI"
RUN dotnet build "EventHub.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventHub.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet EventHub.WebUI.dll