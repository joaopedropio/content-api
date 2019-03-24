FROM microsoft/dotnet:2.2-sdk-alpine AS build-env
WORKDIR /build
COPY . .
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build-env /build/ContentApi/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "ContentApi.dll"]