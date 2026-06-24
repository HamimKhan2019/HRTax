@echo off
echo Starting HR Tax Calculator...

start "Employee Service (gRPC 5001)" cmd /k "cd /d %~dp0HRTax && dotnet run --project EmployeeService.Api"

timeout /t 3 /nobreak >nul

start "Tax Service (REST 5050)" cmd /k "cd /d %~dp0HRTax && dotnet run --project TaxService.Api"

timeout /t 3 /nobreak >nul

start "Angular Frontend (4200)" cmd /k "cd /d %~dp0tax-calculator-ui && ng serve"

timeout /t 15 /nobreak >nul

start http://localhost:4200/

echo All services launching in separate windows.
echo Close those windows (or Ctrl+C in each) to stop.