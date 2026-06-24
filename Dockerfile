# ==========================================
# Estágio 1: Base de Execução (Runtime)
# ==========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ==========================================
# Estágio 2: Compilação e Restauração (SDK)
# ==========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia os arquivos de projeto (.csproj) respeitando a estrutura de pastas
COPY ["order-api/order-api.csproj", "order-api/"]
COPY ["Infraestructure/Infraestructure.csproj", "Infraestructure/"]
COPY ["application/application.csproj", "application/"]
COPY ["Domain/domain.csproj", "domain/"]

# Restaura as dependências do NuGet aproveitando o cache das camadas do Docker
RUN dotnet restore "order-api/order-api.csproj"

# Copia o restante do código fonte de todas as camadas\
COPY . .

# Instala a ferramenta do EF Core globalmente
RUN dotnet tool install --global dotnet-ef --version 10.0.*
ENV PATH="$PATH:/root/.dotnet/tools"

# Entra na pasta do projeto principal (Driving Adapter)
WORKDIR "/src/order-api"

# ==========================================
# Estágio 2.5: GERAR BUNDLE DE MIGRAÇÃO
# ==========================================
RUN dotnet ef migrations bundle \
--project ../Infraestructure/Infraestructure.csproj \
--configuration ${BUILD_CONFIGURATION} \
-o /app/build/efbundle

RUN dotnet build "order-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ==========================================
# Estágio 3: Publicação (Publish)
# ==========================================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "order-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

RUN cp /app/build/efbundle /app/publish/

# ==========================================
# Estágio 4: Imagem Final de Produção (Enxuta)
# ==========================================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "order-api.dll"]