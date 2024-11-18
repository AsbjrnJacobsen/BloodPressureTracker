FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

# Copy the .csproj file(s) and restore dependencies
COPY Data_and_Models/*.csproj ./
RUN dotnet restore

# Install the EF Core CLI tools globally
RUN dotnet tool install --global dotnet-ef

# Add the dotnet tools directory to the PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy the rest of your application code
COPY Data_and_Models/. ./

# Copy appsettings.json
COPY ../MeasurementService/appsettings.json .

# Build the project
RUN dotnet build -c Release -o out

# Set the entry point to run migrations
ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", ".", "--startup-project", "."]