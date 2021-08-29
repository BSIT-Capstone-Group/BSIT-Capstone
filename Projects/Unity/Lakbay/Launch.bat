@ECHO OFF
REM BFCPEOPTIONSTART
REM Advanced BAT to EXE Converter www.BatToExeConverter.com
REM BFCPEEXE=C:\Users\NI.L.A\Documents\GitHub\BSIT-Capstone\Projects\Unity\Lakbay\Launch.exe
REM BFCPEICON=
REM BFCPEICONINDEX=-1
REM BFCPEEMBEDDISPLAY=0
REM BFCPEEMBEDDELETE=1
REM BFCPEADMINEXE=0
REM BFCPEINVISEXE=0
REM BFCPEVERINCLUDE=0
REM BFCPEVERVERSION=1.0.0.0
REM BFCPEVERPRODUCT=Product Name
REM BFCPEVERDESC=Product Description
REM BFCPEVERCOMPANY=Your Company
REM BFCPEVERCOPYRIGHT=Copyright Info
REM BFCPEOPTIONEND
@ECHO ON
@echo off
rem Hideself

@REM Figure out the version of the Project using `ProjectVersion.txt`.
set projectVersionFile=ProjectSettings/ProjectVersion.txt
for /f "tokens=2" %%v in (%projectVersionFile%) do set version=%%v%

@REM Start running the Unity Hub then the Unity Editor. 
start "Unity Hub" /b "%PROGRAMFILES%\Unity Hub\Unity Hub.exe"
start "Unity Editor" /b "%PROGRAMFILES%\Unity\Hub\Editor\%version%\Editor\Unity.exe" -projectPath "./" -buildTarget "Android"

exit