#Build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY backend/ .

WORKDIR /src/src/ToDoAPI

RUN dotnet restore ToDoAPI.csproj

RUN dotnet publish ToDoAPI.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


#Runtime

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "ToDoAPI.dll"]