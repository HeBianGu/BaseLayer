@echo off
::    设定：　运行在全屏模式下
:: Code by redtek 2007-1-12 CMD@XP
:: 出处：http://www.cn-dos.net/forum/viewthread.php?tid=26591
echo exit|%ComSpec% /k prompt e 100 B4 00 B0 12 CD 10 B0 03 CD 10 CD 20 $_g$_q$_|debug>nul

chcp 437>nul
graftabl 936>nul


:rem   下面放置任何命令……
dir
pause