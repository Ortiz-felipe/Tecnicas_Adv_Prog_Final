# Use SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers to cache dependencies
COPY ["VTVApp.Api/VTVApp.Api.csproj", "VTVApp.Api/"]
RUN dotnet restore "VTVApp.Api/VTVApp.Api.csproj"

# Copy everything else and build the app
COPY . .
WORKDIR "/src/VTVApp.Api"
RUN dotnet build "VTVApp.Api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "VTVApp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Use non-root user for added security
RUN useradd -m myappuser
USER myappuser

# Copy published binaries from publish stage
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VTVApp.Api.dll"]

# Expose ports (if necessary)
EXPOSE 80
EXPOSE 443
