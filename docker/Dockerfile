FROM mcr.microsoft.com/dotnet/aspnet:7.0


ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
EXPOSE 80
COPY . . 

ENTRYPOINT ["dotnet", "RentalCompany.Api.dll", "--urls", "http://0.0.0.0"]
