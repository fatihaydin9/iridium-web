FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Iridium.Web/Iridium.Web.csproj", "Iridium.Web/"]
COPY ["Iridium.Application/Iridium.Application.csproj", "Iridium.Application/"]
COPY ["Iridium.Domain/Iridium.Domain.csproj", "Iridium.Domain/"]
COPY ["Iridium.Infrastructure/Iridium.Infrastructure.csproj", "Iridium.Infrastructure/"]
RUN dotnet restore "Iridium.Web/Iridium.Web.csproj"
COPY . .
WORKDIR "/src/Iridium.Web"
RUN dotnet build "Iridium.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Iridium.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Iridium.Web.dll"]
