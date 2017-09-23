Imports System.Windows.Media.Animation
Imports LCLib.Functions
Imports Microsoft.Windows.Controls
Public Class PagesListPanel
    Public Sub UpdateList()
        lst_lessons.Items.Clear()
        For i = 0 To current_user.Progress.Current_Page
            Dim _content = current_unit.Lessons(current_lesson).Pages(i).Content

            lst_lessons.Items.Add((i + 1))
        Next
    End Sub
    Private Sub btn_showpanel_Click(sender As Object, e As RoutedEventArgs) Handles btn_showpanel.Click
        Dim ta As New ThicknessAnimation
        Dim sb As New Storyboard
        ta.Duration = New Duration(TimeSpan.FromMilliseconds(My.Settings.transitions))
        ta.From = New Thickness?(New Thickness(-280, 0, 0, 0))
        ta.To = New Thickness?(New Thickness(-50, 0, 0, 0))
        sb.Children.Add(ta)
        Storyboard.SetTarget(ta, Me)
        Storyboard.SetTargetProperty(ta, New PropertyPath(Control.MarginProperty))
        btn_showpanel.IsEnabled = False
        LC_Window.LC_LessonScreen.brd_darken.ShowObject()
        lst_lessons.SelectedIndex = current_page
        AddHandler sb.Completed, Sub(s, a)
                                     btn_showpanel.IsEnabled = True
                                     RemoveHandler btn_showpanel.Click, AddressOf btn_showpanel_Click
                                     AddHandler btn_showpanel.Click, AddressOf btn_hidepanel_Click
                                     btn_showpanel.Content = "<"
                                 End Sub
        sb.Begin()
    End Sub
    Private Sub btn_hidepanel_Click(sender As Object, e As RoutedEventArgs)
        Dim ta As New ThicknessAnimation
        Dim sb As New Storyboard
        ta.Duration = New Duration(TimeSpan.FromMilliseconds(My.Settings.transitions))
        ta.To = New Thickness?(New Thickness(-280, 0, 0, 0))
        ta.From = New Thickness?(New Thickness(-50, 0, 0, 0))
        sb.Children.Add(ta)
        Storyboard.SetTarget(ta, Me)
        Storyboard.SetTargetProperty(ta, New PropertyPath(Control.MarginProperty))
        btn_showpanel.IsEnabled = False
        LC_Window.LC_LessonScreen.brd_darken.HideObject()
        AddHandler sb.Completed, Sub(s, a)
                                     btn_showpanel.IsEnabled = True
                                     btn_showpanel.Content = ">"
                                     RemoveHandler btn_showpanel.Click, AddressOf btn_hidepanel_Click
                                     AddHandler btn_showpanel.Click, AddressOf btn_showpanel_Click
                                 End Sub
        sb.Begin()
    End Sub
    Private Sub lst_lessons_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lst_lessons.SelectionChanged
        If lst_lessons.SelectedIndex > -1 Then
            current_page = lst_lessons.SelectedIndex
            LC_Window.LC_LessonScreen.ShowPage()
        End If
    End Sub
End Class
