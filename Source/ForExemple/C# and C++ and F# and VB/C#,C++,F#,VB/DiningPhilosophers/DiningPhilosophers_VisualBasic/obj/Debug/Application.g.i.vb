﻿#ExternalChecksum("..\..\Application.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","86B01FB1D7BC02045FE522E63B86D48E")
'------------------------------------------------------------------------------
' <auto-generated>
'     此代码由工具生成。
'     运行时版本:4.0.30319.18444
'
'     对此文件的更改可能会导致不正确的行为，并且如果
'     重新生成代码，这些更改将会丢失。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Effects
Imports System.Windows.Media.Imaging
Imports System.Windows.Media.Media3D
Imports System.Windows.Media.TextFormatting
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports System.Windows.Shell

Namespace DiningPhilosophers
    
    '''<summary>
    '''App
    '''</summary>
    Partial Public Class App
        Inherits System.Windows.Application
        
        '''<summary>
        '''InitializeComponent
        '''</summary>
        <System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
        Public Sub InitializeComponent()
            
            #ExternalSource("..\..\Application.xaml",4)
            Me.StartupUri = New System.Uri("MainWindow.xaml", System.UriKind.Relative)
            
            #End ExternalSource
        End Sub
        
        '''<summary>
        '''Application Entry Point.
        '''</summary>
        <System.STAThreadAttribute(),  _
         System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")>  _
        Public Shared Sub Main()
            Dim app As DiningPhilosophers.App = New DiningPhilosophers.App()
            app.InitializeComponent
            app.Run
        End Sub
    End Class
End Namespace

