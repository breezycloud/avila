dotnet build -c Release
dotnet publish -c Release
vercel --prod bin/Release/net7.0/publish/wwwroot/