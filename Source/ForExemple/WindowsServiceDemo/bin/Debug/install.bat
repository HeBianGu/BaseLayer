%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe D:\WorkArea\DevTest\BaseLayer\Source\ForExemple\WindowsServiceDemo\bin\Debug\WindowsServiceDemo.exe
Net Start WindowServiceTestDemo
sc config WindowServiceTestDemo start= auto

pause