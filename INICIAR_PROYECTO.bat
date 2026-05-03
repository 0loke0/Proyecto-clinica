@echo off
chcp 65001 >nul
echo ==========================================
echo    🏥 CLINICA SALUD+ - Iniciar Proyecto
echo ==========================================
echo.

REM Verificar si MariaDB está corriendo
echo [1/3] Verificando base de datos MariaDB...
sc query MySQL | find "RUNNING" >nul
if %errorlevel% == 0 (
    echo      ✅ MariaDB está activa
) else (
    echo      ⚠️  Iniciando MariaDB...
    net start MySQL
)

echo.
echo [2/3] Iniciando Backend (ASP.NET API)...
start "Backend API" cmd /k "cd /d "%~dp0ClinicaSaludAPI" && dotnet run"
echo      ✅ Backend iniciado en http://localhost:5071

echo.
echo [3/3] Iniciando Frontend (Python Server)...
start "Frontend Server" cmd /k "cd /d "%~dp0" && python -m http.server 8080"
echo      ✅ Frontend disponible en http://localhost:8080

echo.
echo ==========================================
echo    🚀 Proyecto iniciado exitosamente!
echo ==========================================
echo.
echo 📁 Frontend:  http://localhost:8080
echo ⚙️  Backend:   http://localhost:5071
echo 🗄️  API Docs:  http://localhost:5071/api/medicos
echo.
echo Presiona cualquier tecla para abrir el navegador...
pause >nul
start http://localhost:8080/index.html
