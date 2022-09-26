// published this image on dockerhub: https://hub.docker.com/repository/docker/goldencave/dotnet6weatherforecast
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UpstreamWebApi.csproj", "./"]
RUN dotnet restore "UpstreamWebApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "UpstreamWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UpstreamWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UpstreamWebApi.dll"]
