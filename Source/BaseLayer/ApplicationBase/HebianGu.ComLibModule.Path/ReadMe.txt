C#中路径相关操作 



1、判定一个给定的路径是否有效,合法
  通过Path.GetInvalidPathChars或Path.GetInvalidFileNameChars方法获得非法的路径/文件名字符，可以根据它来判断路径中是否包含非法字符；

2、如何确定一个路径字符串是表示目录还是文件
   使用Directory.Exists或File.Exist方法，如果前者为真，则路径表示目录；如果后者为真，则路径表示文件
上面的方法有个缺点就是不能处理那些不存在的文件或目录。这时可以考虑使用Path.GetFileName方法获得其包含的文件名，如果一个路径不为空，而文件名为空那么它表示目录，否则表示文件；
3、获得路径的某个特定部分
   Path.GetDirectoryName ：返回指定路径字符串的目录信息。
   Path.GetExtension ：返回指定的路径字符串的扩展名。
   Path.GetFileName ：返回指定路径字符串的文件名和扩展名。
   Path.GetFileNameWithoutExtension ：返回不具有扩展名的路径字符串的文件名。
   Path.GetPathRoot ：获取指定路径的根目录信息。
4、准确地合并两个路径而不用去担心那个烦人的“\”字符
   使用Path.Combine方法，它会帮你处理烦人的“\”。
5、获得系统目录的路径
   Environment.SystemDirectory属性：获取系统目录的完全限定路径
   Environment.GetFolderPath方法：该方法接受的参数类型为Environment.SpecialFolder枚举，通过这个方法可以获得大量系统    文件夹的路径，如我的电脑，桌面，系统目录等
   Path.GetTempPath方法：返回当前系统的临时文件夹的路径
6、判断一个路径是绝对路径还是相对路径
   使用Path.IsPathRooted方法
7、读取或设置当前目录
   使用Directory类的GetCurrentDirectory和SetCurrentDirectory方法
8、使用相对路径
   设置当前目录后（见上个问题），就可以使用相对路径了。对于一个相对路径，我们可以使用Path.GetFullPath方法获得它的完    全限定路径（绝对路径）。
    注意：如果打算使用相对路径，建议你将工作目录设置为各个交互文件的共同起点，否则可能会引入一些不易发现的安全隐患，被恶意用户利用来访问系统文件。

9、文件夹浏览对话框（FolderBrowserDialog类）
  主要属性： Description：树视图控件上显示的说明文本，如上图中的“选择目录--练习”；RootFolder：获取或设置从其开始浏览的根文件夹，如上 图中设置的我的电脑（默认为桌面）；SelectedPath：获取或设置用户选定的路径，如果设置了该属性，打开对话框时会定位到指定路径，默认为根文 件夹，关闭对话框时根据该属性获取用户用户选定的路径；         ShowNewFolderButton：获取或设置是否显示新建对话框按钮；
 主要方法：  ShowDialog：打开该对话框，返回值为DialogResult类型值，如果为DialogResult.OK，则可以由SelectedPath属性获取用户选定的路径；
