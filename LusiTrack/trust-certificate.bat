@echo off
title Trust ASP.NET Core HTTPS Certificate
color 0A
echo ======================================================================
echo   LusiTrack - ASP.NET Core HTTPS Developer Certificate Setup
echo ======================================================================
echo.
echo  To make https://localhost:7039 secure in Google Chrome and Edge:
echo.
echo  A Windows "Security Warning" popup will appear asking:
echo    "Do you want to install this certificate? (localhost)"
echo.
echo  *** PLEASE CLICK "YES" ON THAT PROMPT ***
echo.
echo ======================================================================
echo.
dotnet dev-certs https --trust
echo.
echo Checking certificate trust status:
dotnet dev-certs https --check --trust
echo.
echo ======================================================================
echo Setup completed! Refresh https://localhost:7039 in your browser.
echo ======================================================================
pause
