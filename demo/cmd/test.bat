@echo off
echo execute cmd.bat
echo I need to excute something in aspire.
echo I want to see running state so please waits
echo args : %1

ping 127.0.0.1 -n %1 > nul

echo finished