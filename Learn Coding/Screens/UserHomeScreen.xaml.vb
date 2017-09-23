Imports LCLib.Functions
Public Class UserHomeScreen
    Public Sub UpdateProgressInfo()
        Dim h_screen = LC_Window.LC_UserHomeScreen
        If Equals(current_unit, Fundamentals) Then
            img_unit.Source = My.Resources.LC_Fundamentals.ToBitmap
        ElseIf Equals(current_unit, Java) Then
            img_unit.Source = My.Resources.java_logo.ToBitmap
        ElseIf Equals(current_unit, CPP) Then
            img_unit.Source = My.Resources.cpp_logo.ToBitmap
        ElseIf Equals(current_unit, HTML) Then
            img_unit.Source = My.Resources.html_logo.ToBitmap
        ElseIf Equals(current_unit, JavaScript) Then
            img_unit.Source = My.Resources.javascript_logo.ToBitmap
        ElseIf Equals(current_unit, PHP) Then
            img_unit.Source = My.Resources.php_logo.ToBitmap
        End If
        h_screen.txtb_unitno.Text = current_unit.Language
        h_screen.txtb_lessonno.Text = "Lesson " & (current_lesson + 1)
        h_screen.txtb_pageno.Text = "Page " & (current_page + 1)
    End Sub

    Private Sub btn_signout_Click(sender As Object, e As RoutedEventArgs) Handles btn_signout.Click
        SignOut()
        NextObject(LC_Window.LC_UsersListPanel)
    End Sub

    Private Sub btn_lesson_Click(sender As Object, e As RoutedEventArgs) Handles btn_lesson.Click
        current_user.LessonOnGoing = True
        LC_Window.LC_LessonScreen.LoadProgress()
        NextObject(LC_Window.LC_LessonScreen)
    End Sub

    Private Sub btn_lesson_info_Click(sender As Object, e As RoutedEventArgs) Handles img_unit.PreviewMouseDown
        Dim str As New System.Text.StringBuilder
        str.AppendLine("Current Unit: " & current_unit.Language)
        str.AppendLine("Current Lesson: Lesson " & current_unit.Lessons(current_lesson).Number)
        str.AppendLine("Current Page: " & current_unit.Lessons(current_lesson).Pages(current_page).ID & "/" & current_unit.Lessons(current_lesson).Pages.Count)
        MsgBox(str.ToString)
    End Sub

End Class
