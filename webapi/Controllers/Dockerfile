FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS base
WORKDIR /source
COPY webapi.csproj .
RUN dotnet restore
COPY . . 
RUN dotnet publish -c release -o /app --no-restore -r linux-arm64

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
EXPOSE 80
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "webapi.dll"]
