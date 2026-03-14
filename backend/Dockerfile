FROM mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY backend/*.slnx ./

COPY backend/src/MyProject.API/MyProject.API.csproj ./src/MyProject.API/
COPY backend/src/MyProject.Application/MyProject.Application.csproj ./src/MyProject.Application/
COPY backend/src/MyProject.Domain/MyProject.Domain.csproj ./src/MyProject.Domain/
COPY backend/src/MyProject.Infrastructure/MyProject.Infrastructure.csproj ./src/MyProject.Infrastructure/

RUN dotnet restore src/MyProject.API/MyProject.API.csproj

COPY backend/src ./src

EXPOSE 5158

ENTRYPOINT ["dotnet", "watch", "--project", "src/MyProject.API", "--no-launch-profile"]
