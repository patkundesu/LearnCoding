Imports System.Windows.Media.Animation
Imports LCLib
Imports LCLib.Functions
Public Class LessonTestScreen
    Dim test_panel As New UIElement
    Dim WithEvents bgWorker As New System.ComponentModel.BackgroundWorker
    Sub bgWorker_DoWork() Handles bgWorker.DoWork
        UpdateTestProgress()
        UpdateProgress()
    End Sub
    Sub bgWorker_RunWorkerCompleted() Handles bgWorker.RunWorkerCompleted

    End Sub
    Public Sub StartTest()
        current_test = current_unit.Lessons(current_lesson).Test
        current_item = 0
        score = 0
        LC_Window.btn_user.IsEnabled = False
        WaitingScreen.ActivateTimer()
        WaitingScreen.ShowObject()
    End Sub
    Public Sub ShowTest()
        If current_test.Questions(current_item).Type = "mc" Then
            ChangeScreen(MultipleChoices)
        ElseIf current_test.Questions(current_item).Type = "id" Then
            ChangeScreen(IdentificationScreen)
        ElseIf current_test.Questions(current_item).Type = "es" Then
            ChangeScreen(EssayScreen)
        ElseIf current_test.Questions(current_item).Type = "cd" Then
            ChangeScreen(CodingScreen)
        End If
    End Sub
    Public Sub ChangeScreen(scr As Control)
        test_panel.IsEnabled = False
        Dim sb As New Storyboard
        Dim da As New DoubleAnimation

        da.From = 1
        da.To = 0
        da.Duration = New Duration(TimeSpan.FromMilliseconds(My.Settings.transitions))
        sb.Children.Add(da)
        Storyboard.SetTarget(da, test_panel)
        Storyboard.SetTargetProperty(da, New PropertyPath(Control.OpacityProperty))
        AddHandler sb.Completed, Sub(s, a)
                                     test_panel.Visibility = Windows.Visibility.Hidden
                                     test_panel = scr
                                     test_panel.IsEnabled = True
                                     If Equals(scr, MultipleChoices) Then
                                         MultipleChoices.UpdateTest()
                                     ElseIf Equals(scr, IdentificationScreen) Then
                                         IdentificationScreen.UpdateQuestion()
                                     ElseIf Equals(scr, EssayScreen) Then
                                         EssayScreen.UpdateTest()
                                     ElseIf Equals(scr, CodingScreen) Then
                                         CodingScreen.UpdateTest()
                                     End If
                                     test_panel.ShowObject()
                                 End Sub
        sb.Begin()
    End Sub
    Public Sub EndTest()
        test_panel.NextObject(TestStatsScreen)
        TestStatsScreen.txtb_unit.Text = current_unit.Language & ", Lesson " & (current_lesson + 1).ToString
        TestStatsScreen.txtb_score.Text = score.ToString
        Dim total_score As Integer = 0
        For Each q In current_test.Questions
            If q.Type = "cd" Then
                total_score += q.Tries
            Else
                total_score += 1
            End If
        Next
        percentage = score / total_score
        TestStatsScreen.txtb_items.Text = total_score
        TestStatsScreen.txtb_percentage.Text = percentage * 100 & "%"
        LC_Window.btn_user.IsEnabled = True

        Dim stats As New test_stats
        stats.TestNumber = current_test.Number
        stats.Score = score
        stats.Percentage = percentage
        If Equals(current_unit, Fundamentals) Then
            current_user.FundamentalStats.Tests.Add(stats)
        End If

        current_lesson += 1
        current_page = 0
        current_user.Progress.Current_Lesson = current_lesson
        current_user.Progress.Current_Page = current_page

        bgWorker.RunWorkerAsync()

        LC_Window.LC_LessonScreen.NextLesson()
    End Sub
    Public Sub NextTest()
        If current_item < current_test.Questions.Count Then
            ShowTest()
        Else
            EndTest()
        End If
    End Sub
End Class
