# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["Ordering/Ordering.Api/Ordering.Api.csproj", "src/Ordering/Ordering.Api/"]
COPY ["Ordering/Ordering.App/Ordering.App.csproj", "src/Ordering/Ordering.App/"]
COPY ["Ordering/Ordering.Infra/Ordering.Infra.csproj", "src/Ordering/Ordering.Infra/"]
COPY ["Common/Common.csproj", "src/Common/"]
RUN dotnet restore "src/Ordering/Ordering.Api/Ordering.Api.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/Ordering/Ordering.Api"
RUN dotnet publish "Ordering.Api.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]