@ECHO off

set CUR_YYYY=%date:~10,4%
set CUR_MM=%date:~4,2%
set CUR_DD=%date:~7,2%

set CUR_HH=%time:~0,2%
if %CUR_HH% lss 10 (set CUR_HH=0%time:~1,1%)

set CUR_NN=%time:~3,2%
set CUR_SS=%time:~6,2%
set CUR_MS=%time:~9,2%

set VERSION_NAME=%CUR_YYYY%%CUR_MM%%CUR_DD%-%CUR_HH%%CUR_NN%%CUR_SS%

SET COMMIT_SHA = "Unknown"

SET VERSION_NUMBER=%CUR_YYYY%.%CUR_MM%.%CUR_DD%

for /f %%i in ('git rev-parse HEAD') do set COMMIT_SHA=%%i

echo %VERSION_NAME%
echo %VERSION_NUMBER%
echo %COMMIT_SHA%

echo on

mkdir c:\publish\naiad\%VERSION_NAME%

dotnet build Naiad_no_tests.sln -c Release
dotnet publish Naiad_no_tests.sln -r win-x64 -c Release --output c:\publish\naiad\%VERSION_NAME% /p:VersionPrefix=%VERSION_NUMBER% /p:VersionSuffix=%COMMIT_SHA%


erase c:\publish\naiad\%VERSION_NAME%\webroot\* /Q /S

pushd webapp

erase dist\* /Q /S

call npm install

call npm run build

xcopy dist\*.* c:\publish\naiad\%VERSION_NAME%\webroot\*.* /s /y

popd

erase /q c:\publish\naiad\%VERSION_NAME%\web.config

7z a -tzip c:\publish\naiad\naiad_%VERSION_NAME%.zip c:\publish\naiad\%VERSION_NAME%\

:end