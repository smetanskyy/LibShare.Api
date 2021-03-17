FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy everything, restore and build
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
RUN mv LibShare.Api/E-Books out/E-Books

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "LibShare.Api.dll"]