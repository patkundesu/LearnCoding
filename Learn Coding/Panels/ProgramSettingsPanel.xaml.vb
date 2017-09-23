Public Class ProgramSettingsPanel
    Private Sub btn_apply_Click(sender As Object, e As RoutedEventArgs) Handles btn_apply.Click
        If cmb_ratio.Text <> My.Settings.aspect_ratio Then
            My.Settings.aspect_ratio = cmb_ratio.Text
        End If
        If txt_transspeed.Text <> My.Settings.transitions.ToString Then
            My.Settings.transitions = Convert.ToInt32(txt_transspeed.Text)
        End If
        SetResolution()
        My.Settings.Save()
    End Sub

    Private Sub btn_close_Click(sender As Object, e As RoutedEventArgs) Handles btn_close.Click
        LC_Window.chk_settings.IsChecked = False
    End Sub
    Dim WithEvents proc As New Process
    Dim procInf As New ProcessStartInfo
    Dim WithEvents bgWorker As New System.ComponentModel.BackgroundWorker
End Class
