@echo off
::    �趨����������ȫ��ģʽ��
:: Code by redtek 2007-1-12 CMD@XP
:: ������http://www.cn-dos.net/forum/viewthread.php?tid=26591
echo exit|%ComSpec% /k prompt e 100 B4 00 B0 12 CD 10 B0 03 CD 10 CD 20 $_g$_q$_|debug>nul

chcp 437>nul
graftabl 936>nul


:rem   ��������κ������
dir
pause