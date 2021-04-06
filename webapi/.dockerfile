FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /App
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out
# -o out -r linux-x64 --self-contained false

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine3.12
WORKDIR /App
EXPOSE 80
COPY --from=build-env /App/out .
ENTRYPOINT ["dotnet","WebAPI.dll"]
