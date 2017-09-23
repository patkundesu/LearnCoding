Imports LCLib.Functions
Public Class ProgrammingLanguagePanel
    Dim WithEvents bgWorker As New System.ComponentModel.BackgroundWorker
    Dim next_unit As New LCLib.unit
    Dim new_lesson As New Integer
    Dim new_page As New Integer
    Sub bgWorker_DoWork() Handles bgWorker.DoWork
        UpdateProgress()

        current_unit = next_unit

        current_user.Progress.Current_Unit = current_unit
        current_user.Progress.Current_Lesson = new_lesson
        current_user.Progress.Current_Page = new_page

        UpdateProgress()

        current_lesson = current_user.Progress.Current_Lesson
        current_page = current_user.Progress.Current_Page
    End Sub
    Sub bgWorker_RunWorkerCompleted() Handles bgWorker.RunWorkerCompleted
        LC_Window.LC_LessonScreen.ShowPage()
        LC_Window.LC_LessonScreen.ShowObject()
    End Sub
    Private Sub JavaButton_Click(sender As Object, e As RoutedEventArgs)
        LC_Window.LC_LessonScreen.HideObject()
        next_unit = Java
        new_lesson = current_user.JavaStats.CurrentLesson
        new_page = current_user.JavaStats.CurrentPage
        bgWorker.RunWorkerAsync()
    End Sub

    Private Sub CppButton_Click(sender As Object, e As RoutedEventArgs)
        LC_Window.LC_LessonScreen.HideObject()
        next_unit = CPP
        new_lesson = current_user.CppStats.CurrentLesson
        new_page = current_user.CppStats.CurrentPage
        bgWorker.RunWorkerAsync()
    End Sub

    Private Sub HtmlButton_Click(sender As Object, e As RoutedEventArgs)
        LC_Window.LC_LessonScreen.HideObject()
        next_unit = HTML
        new_lesson = current_user.HTMLStats.CurrentLesson
        new_page = current_user.HTMLStats.CurrentPage
        bgWorker.RunWorkerAsync()
    End Sub

    Private Sub JsButton_Click(sender As Object, e As RoutedEventArgs)
        LC_Window.LC_LessonScreen.HideObject()
        next_unit = JavaScript
        new_lesson = current_user.JavaScriptStats.CurrentLesson
        new_page = current_user.JavaScriptStats.CurrentPage
        bgWorker.RunWorkerAsync()
    End Sub

    Private Sub PhpButton_Click(sender As Object, e As RoutedEventArgs)
        LC_Window.LC_LessonScreen.HideObject()
        next_unit = PHP
        new_lesson = current_user.PHPStats.CurrentLesson
        new_page = current_user.PHPStats.CurrentPage
        bgWorker.RunWorkerAsync()
    End Sub
End Class
