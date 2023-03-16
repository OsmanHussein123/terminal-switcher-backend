#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["terminal-switcher-backend/terminal-switcher-backend.csproj", "terminal-switcher-backend/"]
RUN dotnet restore "terminal-switcher-backend/terminal-switcher-backend.csproj"
COPY . .
WORKDIR "/src/terminal-switcher-backend"
RUN dotnet build "terminal-switcher-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "terminal-switcher-backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "terminal-switcher-backend.dll"]