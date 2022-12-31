Write-Host "Cleaning ./out folder..." -ForegroundColor Green -NoNewLine
Remove-Item ./out -Recurse -ErrorAction SilentlyContinue
Write-Host " Done." -ForegroundColor Green

Write-Host "Publishing to ./out..." -ForegroundColor Green
dotnet publish .\src\PhotoDateChanger.csproj `
  --nologo `
  --configuration Release `
  --output ./out `
  --runtime win-x64 `
  --self-contained `
  /p:PublishSingleFile=true `
  /p:UseAppHost=true
