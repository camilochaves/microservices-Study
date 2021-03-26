FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore 
COPY . .
RUN dotnet build -c Release -o /App/build 

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
EXPOSE 80
WORKDIR /App
COPY --from=build /App/build .
ENTRYPOINT ["dotnet", "WebApp.dll"]