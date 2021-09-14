FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /src
COPY /src/API/API.csproj .
RUN dotnet restore "API.csproj"
COPY . .
RUN dotnet publish "API.csproj" -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as final
WORKDIR /app
COPY --from=build /publish .

ENTRYPOINT ["dotnet", "/src/API/API.dll"]
