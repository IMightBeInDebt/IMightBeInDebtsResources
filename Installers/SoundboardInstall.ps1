[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

Write-Host ""
Write-Host "     ███╗   ███╗ █████╗ ███████╗███████╗██╗██╗   ██╗███████╗"
Write-Host "     ████╗ ████║██╔══██╗██╔════╝██╔════╝██║██║   ██║██╔════╝"
Write-Host "     ██╔████╔██║███████║███████╗███████╗██║██║   ██║█████╗"
Write-Host "     ██║╚██╔╝██║██╔══██║╚════██║╚════██║██║╚██╗ ██╔╝██╔══╝"
Write-Host "     ██║ ╚═╝ ██║██║  ██║███████║███████║██║ ╚████╔╝ ███████╗"
Write-Host "     ╚═╝     ╚═╝╚═╝  ╚═╝╚══════╝╚══════╝╚═╝  ╚═══╝  ╚══════╝"
Write-Host ""
Write-Host "     Installer" -ForegroundColor Cyan
Write-Host ""

$ErrorActionPreference = "Stop"

function Write-Step($msg) {
    Write-Host "==> $msg" -ForegroundColor Cyan
}

$targetDir = "C:\Python314"
$pythonExe = Join-Path $targetDir "python.exe"
$pythonwExe = Join-Path $targetDir "pythonw.exe"

Write-Step "Checking for Python at $targetDir"

if (Test-Path $pythonExe) {
    Write-Host "Found: $pythonExe" -ForegroundColor Green
} else {
    Write-Step "Not found. Downloading Python 3.14 installer"

    $installerUrl = "https://www.python.org/ftp/python/3.14.0/python-3.14.0-amd64.exe"
    $installerPath = Join-Path $env:TEMP "python-installer.exe"

    Invoke-WebRequest -Uri $installerUrl -OutFile $installerPath

    Write-Step "Installing Python to $targetDir"

    $installArgs = "/quiet InstallAllUsers=0 PrependPath=1 Include_test=0 TargetDir=$targetDir"
    Start-Process -FilePath $installerPath -ArgumentList $installArgs -Wait

    Remove-Item $installerPath -Force
}

Write-Step "Refreshing PATH for current session"

$machinePath = [System.Environment]::GetEnvironmentVariable("Path", "Machine")
$userPath = [System.Environment]::GetEnvironmentVariable("Path", "User")
$env:Path = $machinePath + ";" + $userPath

$scriptsDir = Join-Path $targetDir "Scripts"
if ($env:Path -notlike "*$targetDir*") {
    $env:Path = $env:Path + ";" + $targetDir + ";" + $scriptsDir
}

Write-Step "Verifying installation"
& $pythonExe --version

Write-Step "Upgrading pip"
& $pythonExe -m pip install --upgrade pip

Write-Step "Installing required packages (keyboard)"
& $pythonExe -m pip install keyboard

Write-Step "Done. Python is at $pythonExe and $pythonwExe"

