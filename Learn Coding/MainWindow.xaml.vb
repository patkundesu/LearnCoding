Imports LCLib
Imports LCLib.Functions
Class MainWindow
    Private Sub LC_MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles LC_MainWindow.Loaded
        LC_Window = TryCast(Application.Current.MainWindow, MainWindow)
        LoadSettings()
        If users_list.Count = 0 Then
            LC_Window.LC_HomeScreen.btn_sign.Content = "Create User"
        End If
        LC_HomeScreen.ShowObject()
    End Sub
    
    Private Sub chk_settings_Checked(sender As Object, e As RoutedEventArgs) Handles chk_settings.Checked
        LC_ProgramSettingsPanel.cmb_ratio.Text = My.Settings.aspect_ratio
        LC_ProgramSettingsPanel.txt_transspeed.Text = My.Settings.transitions.ToString

        brd_darken.Visibility = Windows.Visibility.Visible
        LC_ProgramSettingsPanel.Visibility = Windows.Visibility.Visible
        LC_ProgramSettingsPanel.IsEnabled = True
    End Sub
    Private Sub chk_settings_Unchecked(sender As Object, e As RoutedEventArgs) Handles chk_settings.Unchecked
        LC_ProgramSettingsPanel.Visibility = Windows.Visibility.Hidden
        LC_ProgramSettingsPanel.IsEnabled = False
        brd_darken.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub btn_close_Click(sender As Object, e As RoutedEventArgs) Handles btn_close.Click
        Close()
    End Sub

    Private Sub LC_Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        Dim choice As Integer = MsgBox("Are you sure you want to close?", vbYesNo + vbQuestion)
        Select Case choice
            Case vbYes
                End
            Case vbNo
                e.Cancel = True
        End Select
    End Sub

    Private Sub btn_minimize_Click(sender As Object, e As RoutedEventArgs) Handles btn_minimize.Click
        WindowState = Windows.WindowState.Minimized
    End Sub

    Private Sub btn_user_Click(sender As Object, e As RoutedEventArgs) Handles btn_user.Click
        If current_user.LessonOnGoing = True Then
            Dim choice As Integer = MsgBox("Lesson is ongoing. Go back to Home Screen?", vbQuestion + vbYesNo)
            Select Case choice
                Case vbYes
                    current_user.LessonOnGoing = False
                    LC_UserHomeScreen.UpdateProgressInfo()
                    LC_LessonScreen.NextObject(LC_UserHomeScreen)
                Case vbNo
                    Exit Sub
            End Select
        End If
    End Sub
End Class
