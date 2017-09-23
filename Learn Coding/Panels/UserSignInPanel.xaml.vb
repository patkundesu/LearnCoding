Imports LCLib
Imports LCLib.Functions
Public Class UserSignInPanel
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
    Sub Unload()
        current_user = New lc_user

        LC_Window.brd_darken.HideObject()
        HideObject()
        LC_Window.LC_UsersListPanel.IsEnabled = True
        ResetBoxes()
    End Sub
    Sub ResetBoxes()
        txt_password.Password = Nothing
        lbl_password.Visibility = Windows.Visibility.Visible
        txtb_user.Text = "<insert username>"
    End Sub
    Private Sub btn_cancel_Click(sender As Object, e As RoutedEventArgs) Handles btn_cancel.Click
        Unload()
    End Sub
    Private Sub btn_sign_Click(sender As Object, e As RoutedEventArgs) Handles btn_sign.Click
        If Validation(current_user.Username.Encrypt, txt_password.Password.Encrypt) Then
            ResetBoxes()
            SignIn()

            LC_Window.LC_UsersListPanel.HideObject()
            LC_Window.brd_darken.HideObject()
            NextObject(LC_Window.LC_UserHomeScreen)
        Else
            MsgBox("Wrong Password!", vbExclamation)
        End If
    End Sub
    Sub EnableDisableCreateButton() Handles txt_password.PasswordChanged
        If txt_password.Password.Length > 0 Then
            btn_sign.IsEnabled = True
        Else
            btn_sign.IsEnabled = False
        End If
    End Sub
End Class
