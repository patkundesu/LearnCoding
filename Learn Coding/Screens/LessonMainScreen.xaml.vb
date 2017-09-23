Imports System.Windows.Media.Animation
Imports LCLib
Imports LCLib.Functions
Public Class LessonMainScreen
    Public Current_Screen As New UIElement
    Public WithEvents bgWorker As New System.ComponentModel.BackgroundWorker
    Public Sub bgworker_DoWork(sender As Object, e As EventArgs) Handles bgWorker.DoWork
        UpdateProgress()
        If current_page = current_unit.Lessons(current_lesson).Pages.Count - 1 Then
            If current_lesson = current_unit.Lessons.Count - 1 Then
                UnitFinished()
            End If
        End If
    End Sub
    
    Public Sub LoadProgress()
        txtb_unit.Text = current_unit.Language
        txtb_lesson.Text = "Lesson " & (current_lesson + 1)
        txtb_lessontopic.Text = current_unit.Lessons(current_lesson).Topic
        PagesListPanel.UpdateList()
        ShowPage()
    End Sub
    Public Sub NextLesson()
        txtb_unit.Text = current_unit.Language
        txtb_lesson.Text = "Lesson " & (current_lesson + 1)
        txtb_lessontopic.Text = current_unit.Lessons(current_lesson).Topic

        PagesListPanel.UpdateList()
    End Sub
    Public Sub NextPage() Handles next_button.Click
        If Not bgWorker.IsBusy Then
            If current_unit.Lessons(current_lesson).Pages.Count - 1 > current_page Then
                current_page += 1
                ShowPage()
            End If
            If current_user.Progress.Current_Page < current_page Then
                current_user.Progress.Current_Page += 1
                If Equals(current_user.Progress.Current_Unit, Fundamentals) Then
                    current_user.FundamentalStats.CurrentPage += 1
                ElseIf Equals(current_user.Progress.Current_Unit, Java) Then
                    current_user.JavaStats.CurrentPage += 1
                ElseIf Equals(current_user.Progress.Current_Unit, CPP) Then
                    current_user.CppStats.CurrentPage += 1
                ElseIf Equals(current_user.Progress.Current_Unit, HTML) Then
                    current_user.HTMLStats.CurrentPage += 1
                ElseIf Equals(current_user.Progress.Current_Unit, JavaScript) Then
                    current_user.JavaScriptStats.CurrentPage += 1
                ElseIf Equals(current_user.Progress.Current_Unit, PHP) Then
                    current_user.PHPStats.CurrentPage += 1
                End If
                PagesListPanel.UpdateList()
                bgWorker.RunWorkerAsync()
            End If
        End If
    End Sub
    Public Sub PreviousPage() Handles previous_button.Click
        If current_page > 0 Then
            current_page -= 1
            ShowPage()
        End If
    End Sub
    Public Sub ShowPage()
        Dim page_ = current_unit.Lessons(current_lesson).Pages(current_page)
        Select Case page_.PageType
            Case "text"
                ChangeType(LTextScreen)
            Case "image"
                ChangeType(LImageScreen)
            Case "image_scroll"
                ChangeType(LImageScrollScreen)
            Case "code"
                ChangeType(LCodeScreen)
            Case "video"
                ChangeType(LVideoScreen)
        End Select
        txtb_currentpage.Text = current_page + 1
        txtb_totalpage.Text = current_unit.Lessons(current_lesson).Pages.Count
    End Sub
    Public Sub ChangeType(scr As UIElement)
        If Equals(Current_Screen, LVideoScreen) Then
            LVideoScreen.media_video.Stop()
        End If
        Dim sb As New Storyboard
        Dim da As New DoubleAnimation

        da.From = 1
        da.To = 0
        da.Duration = New Duration(TimeSpan.FromMilliseconds(My.Settings.transitions))
        sb.Children.Add(da)
        Storyboard.SetTarget(da, Current_Screen)
        Storyboard.SetTargetProperty(da, New PropertyPath(Control.OpacityProperty))
        next_button.IsEnabled = False
        previous_button.IsEnabled = False
        PagesListPanel.lst_lessons.IsEnabled = False

        AddHandler sb.Completed, Sub(s, a)
                                     Current_Screen.IsEnabled = False
                                     Current_Screen.Visibility = Windows.Visibility.Hidden
                                     Current_Screen = scr
                                     If Equals(scr, LCodeScreen) Then
                                         LCodeScreen.UpdateLesson()
                                     ElseIf Equals(scr, LImageScreen) Then
                                         LImageScreen.UpdateLesson()
                                     ElseIf (Equals(scr, LImageScrollScreen)) Then
                                         LImageScrollScreen.UpdateLesson()
                                     ElseIf (Equals(scr, LVideoScreen)) Then
                                         LVideoScreen.UpdateLesson()
                                     ElseIf Equals(scr, LTextScreen) Then
                                         LTextScreen.UpdateLesson()
                                     End If
                                     PagesListPanel.lst_lessons.IsEnabled = True
                                     Current_Screen.ShowObject()
                                     If current_page = 0 Then
                                         proceed_button.HideObject()
                                         previous_button.HideObject()
                                         next_button.ShowObject()
                                         PL_Panel.HideObject()
                                     ElseIf current_page = current_unit.Lessons(current_lesson).Pages.Count - 1 Then
                                         If current_lesson < current_unit.Lessons.Count - 1 Then
                                             next_button.HideObject()
                                             previous_button.ShowObject()
                                             proceed_button.ShowObject()
                                         Else
                                             next_button.HideObject()
                                             previous_button.ShowObject()
                                             PL_Panel.btn_js.IsEnabled = current_user.HTMLStats.Finished
                                             PL_Panel.btn_php.IsEnabled = current_user.HTMLStats.Finished
                                             PL_Panel.ShowObject()

                                             PL_Panel.btn_js.IsEnabled = current_user.HTMLStats.Finished
                                             PL_Panel.btn_php.IsEnabled = current_user.HTMLStats.Finished
                                         End If
                                     Else
                                         next_button.ShowObject()
                                         proceed_button.HideObject()
                                         previous_button.ShowObject()
                                         PL_Panel.HideObject()
                                     End If
                                 End Sub
        sb.Begin()
    End Sub
    Private Sub proceed_button_Click(sender As Object, e As RoutedEventArgs) Handles proceed_button.Click
        Dim choice As Integer = MsgBox("Are you ready to take the test?", vbQuestion + vbYesNo)
        Select Case choice
            Case vbYes
                LC_Window.LC_TestScreen.StartTest()
                NextObject(LC_Window.LC_TestScreen)
            Case vbNo
                Exit Sub
        End Select
    End Sub
End Class
