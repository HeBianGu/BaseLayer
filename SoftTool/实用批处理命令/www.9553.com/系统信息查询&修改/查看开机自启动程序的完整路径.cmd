@echo off
:: ���ǵ����򲢷Ƕ���װ��ϵͳ���£����Ի�Ҫ��!str:~-1!����ȡ�̷�
:: ���·���к���N�������ַ��Ļ�����·�������N���ַ�������ʾ(һ�������ַ�ռ�����ַ�λ)
:: code by jm 2006-7-27
setlocal enabledelayedexpansion
echo.
echo                �����������ĳ����У�
echo.
for /f "skip=4 tokens=1* delims=:" %%i in ('reg query HKLM\Software\Microsoft\Windows\CurrentVersion\Run') do (
    set str=%%i
    set var=%%j
    set "var=!var:"=!"
    if not "!var:~-1!"=="=" echo !str:~-1!:!var!
)
pause>nul
