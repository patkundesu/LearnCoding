Public Class ConsoleScreen
    Private Sub btn_back_Click(sender As Object, e As RoutedEventArgs) Handles btn_back.Click
        console_emulator.StopProcess()
        console_emulator.ClearConsole()
        Visibility = Windows.Visibility.Hidden
        IsEnabled = False
    End Sub
End Class
