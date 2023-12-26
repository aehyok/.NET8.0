# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG TARGETARCH
WORKDIR /source

COPY  net8/.  .

WORKDIR Services/SystemService/aehyok.SystemService

RUN dotnet publish "aehyok.SystemService.csproj" -o /app -f net8.0


# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
EXPOSE 8080
WORKDIR /app

COPY /etc/ .

COPY --from=build /app .

ENTRYPOINT ["./aehyok.SystemService"]