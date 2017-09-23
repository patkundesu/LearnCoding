Imports System.Windows.Media.Animation
Imports Microsoft.Windows.Controls
Imports WpfAnimatedGif
Imports System.Windows.Threading
Public Class CodeScreen
    Private WithEvents sb As New Storyboard
    Dim ca As New ColorAnimation
    Dim ta As New ThicknessAnimation

    Public Sub UpdateLesson()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(HtmlTextBlock.Document, _
                      current_unit.Lessons(current_lesson).Pages(current_page).Content)
        java_editor.Text = current_unit.Lessons(current_lesson).Pages(current_page).Codes.Java
        cpp_editor.Text = current_unit.Lessons(current_lesson).Pages(current_page).Codes.Cpp
        html_editor.Text = current_unit.Lessons(current_lesson).Pages(current_page).Codes.HTML
        js_editor.Text = current_unit.Lessons(current_lesson).Pages(current_page).Codes.JavaScript
        php_editor.Text = current_unit.Lessons(current_lesson).Pages(current_page).Codes.PHP
        txt_class.Text = current_unit.Lessons(current_lesson).Pages(current_page).ClassName
        Language_Used = current_unit.Lessons(current_lesson).Pages(current_page).MainLanguage
        Select Case Language_Used
            Case "Java"
                code_collection.SelectedIndex = 0
            Case "C++"
                code_collection.SelectedIndex = 1
            Case "HTML"
                code_collection.SelectedIndex = 2
            Case "JavaScript"
                code_collection.SelectedIndex = 3
            Case "PHP"
                code_collection.SelectedIndex = 4
        End Select
    End Sub
    Private Sub btn_run_Click(sender As Object, e As RoutedEventArgs) Handles btn_run.Click
        Dim language_tab As TabItem = code_collection.SelectedItem
        Language_Used = language_tab.Header
        LC_Window.LC_CodeOutputScreen.CCompileScreen.Visibility = Windows.Visibility.Visible
        ClassName_Edited = txt_class.Text
        Select Case Language_Used
            Case "Java"
                Code_Edited = java_editor.Text
                LC_Window.LC_CodeOutputScreen.CConsoleScreen.IsEnabled = True
            Case "C++"
                Code_Edited = cpp_editor.Text
                LC_Window.LC_CodeOutputScreen.CConsoleScreen.IsEnabled = True
            Case "HTML"
                Code_Edited = html_editor.Text
                LC_Window.LC_CodeOutputScreen.CWebBrowserScreen.IsEnabled = True
            Case "JavaScript"
                Code_Edited = js_editor.Text
                LC_Window.LC_CodeOutputScreen.CWebBrowserScreen.IsEnabled = True
            Case "PHP"
                Code_Edited = php_editor.Text
                LC_Window.LC_CodeOutputScreen.CWebBrowserScreen.IsEnabled = True
        End Select
        compileWorker.RunWorkerAsync()
    End Sub
    Public Sub Compile_Finished()
        If Language_Used = "Java" Or Language_Used = "C++" Then
            LC_Window.LC_CodeOutputScreen.CCompileScreen.Visibility = Windows.Visibility.Hidden
            LC_Window.LC_CodeOutputScreen.CConsoleScreen.Visibility = Windows.Visibility.Visible
        ElseIf Language_Used = "HTML" Or Language_Used = "JavaScript" Or Language_Used = "PHP" Then
            LC_Window.LC_CodeOutputScreen.CWebBrowserScreen.wb_output.NavigateToString(ProgramOutput)
            LC_Window.LC_CodeOutputScreen.CCompileScreen.Visibility = Windows.Visibility.Hidden
            LC_Window.LC_CodeOutputScreen.CWebBrowserScreen.Visibility = Windows.Visibility.Visible
        End If
    End Sub
End Class
