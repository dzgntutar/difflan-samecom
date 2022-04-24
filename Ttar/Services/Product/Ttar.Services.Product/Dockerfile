FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
#EXPOSE 5011:80
COPY Common/Ttar.Common/*.csproj Common/Ttar.Common/
COPY Services/Product/Ttar.Services.Product/*.csproj Services/Product/Ttar.Services.Product/
RUN dotnet restore Services/Product/Ttar.Services.Product/*.csproj
COPY . .
RUN dotnet publish Services/Product/Ttar.Services.Product/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","Ttar.Services.Product.dll" ]