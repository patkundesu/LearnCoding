Imports LCLib
Imports LCLib.Functions

Public Class UserDeletePanel
#Region "Watermark"
    Private Sub txt_password_GotFocus(sender As Object, e As RoutedEventArgs) Handles txt_password.GotFocus
        lbl_password.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub txt_password_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_password.LostFocus
        If txt_password.Password.Length = 0 Then
            lbl_password.Visibility = Windows.Visibility.Visible
        End If
    End Sub
#End Region
    Private Sub btn_cancel_Click(sender As Object, e As RoutedEventArgs) Handles btn_cancel.Click
        Unload()
    End Sub
    Sub Unload()
        current_user = New LCLib.lc_user

        LC_Window.brd_darken.HideObject()
        HideObject()
        LC_Window.LC_UsersListPanel.IsEnabled = True
        ResetBoxes()
    End Sub
    Sub ResetBoxes()
        txt_password.Password = Nothing
        lbl_password.Visibility = Windows.Visibility.Visible
    End Sub
    Sub EnableDisableCreateButton() Handles txt_password.PasswordChanged
        btn_delete.IsEnabled = (txt_password.Password.Length > 0)
    End Sub

    Private Sub btn_delete_Click(sender As Object, e As RoutedEventArgs) Handles btn_delete.Click
        If Validation(current_user.Username, txt_password.Password) Then
            Dim choice As Integer = MsgBox("Are you sure you want to delete " & current_user.Username & "?" & vbCrLf _
           & "This action cannot be undone.", vbYesNo + vbExclamation)
            If choice = vbYes Then
                Unload()
                DeleteUser()
            End If
        Else
            MsgBox("Wrong Password!", vbExclamation)
        End If
    End Sub
End Class
