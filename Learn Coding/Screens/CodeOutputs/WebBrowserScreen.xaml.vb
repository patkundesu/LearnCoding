Public Class WebBrowserScreen

    Private Sub btn_back_Click(sender As Object, e As RoutedEventArgs) Handles btn_back.Click
        Visibility = Windows.Visibility.Hidden
        IsEnabled = False
    End Sub
End Class
