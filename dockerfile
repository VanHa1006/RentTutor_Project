# Chọn image cơ sở
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Tạo một image build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY RentTutorSolution/RentTutorPresentation/RentTutorPresentation.csproj RentTutorSolution/RentTutorPresentation/
COPY RentTutorSolution/BusinessAccess/BusinessAccess.csproj RentTutorSolution/BusinessAccess/
COPY RentTutorSolution/DataAccess/DataAccess.csproj RentTutorSolution/DataAccess/
COPY RentTutorSolution/Common/Common.csproj RentTutorSolution/Common/
RUN dotnet restore RentTutorSolution/RentTutorPresentation/RentTutorPresentation.csproj
COPY . .
WORKDIR /src/RentTutorSolution/RentTutorPresentation
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentTutorPresentation.dll"]
