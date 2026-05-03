# Script de inicio - Clínica Salud+
# Guardar como UTF-8 with BOM si hay problemas de acentos

Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "   🏥 CLINICA SALUD+ - Iniciar Proyecto" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""

$projectPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$backendPath = Join-Path $projectPath "ClinicaSaludAPI"

# 1. Verificar MariaDB
Write-Host "[1/3] Verificando base de datos MariaDB..." -ForegroundColor Yellow
$service = Get-Service -Name "MySQL" -ErrorAction SilentlyContinue
if ($service -and $service.Status -eq 'Running') {
    Write-Host "      ✅ MariaDB está activa" -ForegroundColor Green
} else {
    Write-Host "      ⚠️  Iniciando MariaDB..." -ForegroundColor Yellow
    try {
        Start-Service -Name "MySQL" -ErrorAction Stop
        Write-Host "      ✅ MariaDB iniciada" -ForegroundColor Green
    } catch {
        Write-Host "      ❌ Error al iniciar MariaDB. Verifica que esté instalada." -ForegroundColor Red
        Read-Host "Presiona Enter para salir"
        exit 1
    }
}

# 2. Iniciar Backend
Write-Host ""
Write-Host "[2/3] Iniciando Backend (ASP.NET API)..." -ForegroundColor Yellow
$backendJob = Start-Job -ScriptBlock {
    param($path)
    Set-Location $path
    dotnet run --urls "http://localhost:5071"
} -ArgumentList $backendPath

Start-Sleep -Seconds 5

# Verificar que el backend responde
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5071/api/medicos" -UseBasicParsing -TimeoutSec 5
    Write-Host "      ✅ Backend iniciado en http://localhost:5071" -ForegroundColor Green
} catch {
    Write-Host "      ⚠️  Backend iniciándose (puede tardar unos segundos más)..." -ForegroundColor Yellow
}

# 3. Iniciar Frontend
Write-Host ""
Write-Host "[3/3] Iniciando Frontend (Python Server)..." -ForegroundColor Yellow
$frontendJob = Start-Job -ScriptBlock {
    param($path)
    Set-Location $path
    python -m http.server 8080
} -ArgumentList $projectPath

Start-Sleep -Seconds 2
Write-Host "      ✅ Frontend disponible en http://localhost:8080" -ForegroundColor Green

Write-Host ""
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host "   🚀 Proyecto iniciado exitosamente!" -ForegroundColor Cyan
Write-Host "==========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "📁 Frontend:  http://localhost:8080" -ForegroundColor White
Write-Host "⚙️  Backend:   http://localhost:5071" -ForegroundColor White
Write-Host "🗄️  API Docs:  http://localhost:5071/api/medicos" -ForegroundColor White
Write-Host ""

# Abrir navegador
$openBrowser = Read-Host "¿Deseas abrir el navegador ahora? (s/n)"
if ($openBrowser -eq 's' -or $openBrowser -eq 'S') {
    Start-Process "http://localhost:8080/index.html"
}

Write-Host ""
Write-Host "Presiona Ctrl+C para detener los servicios..." -ForegroundColor Gray
while ($true) {
    Start-Sleep -Seconds 1
}
