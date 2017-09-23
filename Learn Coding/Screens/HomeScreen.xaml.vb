Imports LCLib
Imports LCLib.Functions
Public Class HomeScreen

    Private Sub btn_sign_Click(sender As Object, e As RoutedEventArgs) Handles btn_sign.Click
        If users_list.Count > 0 Then
            LC_Window.LC_UsersListPanel.UpdateList()
            NextObject(LC_Window.LC_UsersListPanel)
        Else
            NextObject(LC_Window.LC_UserCreatePanel)
        End If
    End Sub
End Class
