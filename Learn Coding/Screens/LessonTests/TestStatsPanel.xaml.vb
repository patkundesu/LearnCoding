Imports LCLib.Functions
Public Class TestStatsPanel
    Private Sub btn_proceed_Click(sender As Object, e As RoutedEventArgs) Handles btn_proceed.Click
        LC_Window.LC_LessonScreen.ShowPage()
        LC_Window.LC_LessonScreen.next_button.Visibility = Windows.Visibility.Visible
        LC_Window.LC_LessonScreen.next_button.IsEnabled = True
        HideObject()
        LC_Window.LC_TestScreen.NextObject(LC_Window.LC_LessonScreen)
    End Sub
End Class
