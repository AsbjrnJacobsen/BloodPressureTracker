#!/bin/bash
export PATH="$PATH:/root/.dotnet/tools"
cd /app

dotnet tool install --global dotnet-ef

export ConString="server=db;port=3306;database=BloodPressureDB;user=sa;password=pepsi1234;"

dotnet ef database update --project /Data_and_Models/Data_and_Models.csproj
