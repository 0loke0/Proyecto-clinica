# 🏥 Clínica Salud+ - Guía de Inicio Local

Aplicación web MVC con base de datos para gestión de citas médicas.

---

## 📋 Requisitos Previos

Antes de iniciar, asegúrate de tener instalado:

| Software | Versión | Verificar |
|----------|---------|-----------|
| **.NET SDK** | 9.0 o superior | `dotnet --version` |
| **MariaDB** (o MySQL) | 10.x / 8.x | `mysql --version` |
| **Python** | 3.8 o superior | `python --version` |
| **Navegador** | Chrome/Edge/Firefox | — |

> 💡 **Nota:** MariaDB ya fue instalada durante la configuración del proyecto. Si no está corriendo, usa: `net start MySQL`

---

## 🚀 Inicio Rápido (Automático)

### Opción A: Script Batch (Windows)
Haz doble clic en el archivo:
```
INICIAR_PROYECTO.bat
```
Este script inicia automáticamente la base de datos, el backend, el frontend y abre el navegador.

### Opción B: Script PowerShell
Abre PowerShell en la carpeta del proyecto y ejecuta:
```powershell
.\Iniciar-Proyecto.ps1
```

---

## 🛠️ Inicio Manual (Paso a Paso)

Si prefieres controlar cada servicio por separado, sigue estos pasos:

### Paso 1: Verificar la Base de Datos

Abre **PowerShell como Administrador** y ejecuta:

```powershell
# Verificar estado del servicio
Get-Service -Name "MySQL"

# Si no está Running, iniciarla:
net start MySQL
```

La base de datos `ClinicaSaludDB` ya fue creada durante la instalación.

---

### Paso 2: Iniciar el Backend (API ASP.NET)

Abre una **nueva terminal** y ejecuta:

```powershell
cd "C:\Users\user\Documents\Universidad\2026-1\Desarrollo web\Proyecto\clinica\ClinicaSaludAPI"
dotnet run
```

**Salida esperada:**
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5071
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
```

✅ **La API está disponible en:** `http://localhost:5071`

**Verificar que funciona:** Abre el navegador y visita:
```
http://localhost:5071/api/medicos
```
Deberías ver la lista de médicos en formato JSON.

---

### Paso 3: Iniciar el Frontend

Abre otra **nueva terminal** (sin cerrar la del backend) y ejecuta:

```powershell
cd "C:\Users\user\Documents\Universidad\2026-1\Desarrollo web\Proyecto\clinica"
python -m http.server 8080
```

**Salida esperada:**
```
Serving HTTP on :: port 8080 (http://[::]:8080/) ...
```

✅ **El frontend está disponible en:** `http://localhost:8080`

---

### Paso 4: Abrir la Aplicación

Abre tu navegador y visita:

```
http://localhost:8080/index.html
```

---

## 🌐 URLs Importantes

| Servicio | URL | Descripción |
|----------|-----|-------------|
| 🏠 **Aplicación** | http://localhost:8080 | Interfaz web principal |
| ⚙️ **API** | http://localhost:5071 | Backend REST |
| 👨‍⚕️ **Médicos** | http://localhost:5071/api/medicos | Lista de médicos (JSON) |
| 🧑 **Pacientes** | http://localhost:5071/api/pacientes | Lista de pacientes (JSON) |
| 📅 **Citas** | http://localhost:5071/api/citas | Lista de citas (JSON) |
| 📝 **Recomendaciones** | http://localhost:5071/api/recomendaciones | Recomendaciones (JSON) |

---

## 🗃️ Estructura del Proyecto

```
clinica/
│
├── 📁 ClinicaSaludAPI/          # Backend ASP.NET Core
│   ├── Controllers/             # Controladores API
│   │   ├── PacientesController.cs
│   │   ├── MedicosController.cs
│   │   ├── CitasController.cs
│   │   └── RecomendacionesController.cs
│   ├── Models/                  # Entidades (Modelo)
│   │   ├── Paciente.cs
│   │   ├── Medico.cs
│   │   ├── Cita.cs
│   │   ├── Recomendacion.cs
│   │   └── Especialidad.cs
│   ├── Data/                    # DbContext
│   │   └── ApplicationDbContext.cs
│   ├── Program.cs               # Configuración CORS + EF
│   └── appsettings.json         # Cadena de conexión MySQL
│
├── 📁 css/                      # Estilos
│   └── styles.css
│
├── 📁 js/                       # JavaScript común
│   └── main.js
│
├── index.html                   # Página de inicio
├── registro-paciente.html       # Formulario registro
├── citas.html                   # Agendamiento de citas
├── medicos.html                 # Catálogo de médicos
├── recomendaciones.html         # Recomendaciones médicas
├── documentacion.html           # Documentación del proyecto
│
├── INICIAR_PROYECTO.bat         # Script de inicio automático
├── Iniciar-Proyecto.ps1         # Script PowerShell
└── README.md                    # Este archivo
```

---

## 🔄 Flujo de Uso

1. **Registro de Paciente**
   - Ve a `Registrarme` en el menú
   - Completa el formulario
   - Los datos se guardan en la base de datos vía API

2. **Agendar Cita**
   - Ve a `Agendar Cita`
   - Ingresa tus datos (la identificación debe existir en la BD)
   - Selecciona un médico de la lista cargada dinámicamente
   - Elige fecha y hora
   - Confirma la cita

3. **Generar Recomendación** (vista del médico)
   - Ve a `Recomendaciones`
   - Ingresa la identificación del paciente
   - Selecciona una cita existente
   - Completa el diagnóstico y recomendaciones
   - Guarda

4. **Consultar Recomendaciones**
   - En la pestaña "Consultar" de Recomendaciones
   - Ingresa tu identificación
   - Visualiza tus recomendaciones médicas

---

## 🛠️ Solución de Problemas

### ❌ Error: "No se puede cargar el índice de servicio NuGet"
**Solución:** Ignorar. Es un warning de una fuente de paquetes interna. La compilación funciona correctamente.

### ❌ Error: "Unable to connect to any of the specified MySQL hosts"
**Solución:** La base de datos no está corriendo.
```powershell
net start MySQL
```

### ❌ Error: "No se reconoce el comando dotnet"
**Solución:** El .NET SDK no está instalado o no está en el PATH.
- Descargar desde: https://dotnet.microsoft.com/download
- Reiniciar la terminal después de instalar

### ❌ Error: "No se reconoce el comando python"
**Solución:** Python no está en el PATH.
- Usa: `py -m http.server 8080` en lugar de `python`

### ❌ CORS Error en el navegador
**Solución:** Asegúrate de que:
1. El backend está corriendo en `http://localhost:5071`
2. Estás accediendo al frontend por `http://localhost:8080` (no abriendo el archivo HTML directamente)
3. No usar `https` si la API está en `http`

### ❌ La API no responde después de iniciarla
**Solución:** Espera 5-10 segundos. La primera vez que corre, compila el proyecto y puede tardar.

---

## 📦 Comandos Útiles

### Recompilar el backend:
```powershell
cd ClinicaSaludAPI
dotnet build
```

### Recrear la base de datos (⚠️ borra datos):
```powershell
cd ClinicaSaludAPI
dotnet ef database drop --force
dotnet ef database update
```

### Ver logs de la API:
```powershell
cd ClinicaSaludAPI
dotnet run --verbose
```

---

## 📄 Documentación

Para generar el PDF de entrega:
1. Abre `documentacion.html` en Chrome
2. Presiona `Ctrl + P`
3. Selecciona "Guardar como PDF"
4. Guarda con nombre: `Documentacion_Taller_ClinicaSalud.pdf`

---

## 👥 Integrantes del Equipo

- [Nombre del estudiante 1]
- [Nombre del estudiante 2]
- [Nombre del estudiante 3]
- [Nombre del estudiante 4]

**Asignatura:** Desarrollo Web
**Fecha:** Mayo 2026

---

## 📞 Soporte

Si encuentras algún problema, verifica:
1. Que los tres servicios estén corriendo (MariaDB, Backend, Frontend)
2. Que las URLs sean correctas
3. Que no haya otro programa usando los puertos 5071 o 8080

Para ver puertos en uso:
```powershell
netstat -ano | findstr "5071"
netstat -ano | findstr "8080"
```
