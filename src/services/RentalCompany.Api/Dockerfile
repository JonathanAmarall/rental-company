FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/building blocks/RentalCompany.Core/RentalCompany.Core.csproj", "src/BuildingBlocks/RentalCompany.Core/"]
COPY ["src/services/RentalCompany.Api/RentalCompany.Api.csproj", "src/services/RentalCompany.Api/"]
COPY ["src/services/RentalCompany.Data/RentalCompany.Data.csproj", "src/services/RentalCompany.Data/"]
COPY ["src/services/RentalCompany.Domain/RentalCompany.Domain.csproj", "src/services/RentalCompany.Domain/"]
RUN dotnet restore "src/services/RentalCompany.Api/RentalCompany.Api.csproj"
COPY . .
WORKDIR "/src/src/services/RentalCompany.Api"
RUN dotnet build "RentalCompany.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RentalCompany.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalCompany.Api.dll"]