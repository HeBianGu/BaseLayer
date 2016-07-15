@echo off
:: 考虑到程序并非都安装在系统盘下，所以还要用!str:~-1!来截取盘符
:: 如果路径中含有N个中文字符的话，此路径的最后N个字符将不显示(一个中文字符占两个字符位)
:: code by jm 2006-7-27
setlocal enabledelayedexpansion
echo.
echo                开机自启动的程序有：
echo.
for /f "skip=4 tokens=1* delims=:" %%i in ('reg query HKLM\Software\Microsoft\Windows\CurrentVersion\Run') do (
    set str=%%i
    set var=%%j
    set "var=!var:"=!"
    if not "!var:~-1!"=="=" echo !str:~-1!:!var!
)
pause>nul
