@echo off

set MSBUILD="C:\Program Files (x86)\MSBuild\14.0\Bin"

cls
%MSBUILD%\msbuild.exe "Jrpg.sln" /p:configuration=Unity
pause
