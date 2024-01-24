FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/first-microservice.csproj", "first-microservice/"]
RUN dotnet restore "first-microservice/first-microservice.csproj"
COPY . .
WORKDIR "/src/first-microservice"
RUN dotnet build "first-microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "first-microservice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "first-microservice.dll"]
