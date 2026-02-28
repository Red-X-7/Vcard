# See https://aka.ms/customizecontainer to learn how to customize your debug container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
# Render için portu dinamik hale getiriyoruz
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Vcard/Vcard.csproj", "Vcard/"]
RUN dotnet restore "./Vcard/Vcard.csproj"
COPY . .
WORKDIR "/src/Vcard"
RUN dotnet build "./Vcard.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Vcard.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# KRİTİK EKLEME: Render'ın portunu uygulamaya bağlıyoruz
ENV ASPNETCORE_URLS=http://*:$PORT

ENTRYPOINT ["dotnet", "Vcard.dll"]