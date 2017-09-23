Imports LCLib.Functions
Imports System.ComponentModel
Imports WpfAnimatedGif
Imports System.Windows.Media.Animation
Public Class UserCreatePanel
    Private WithEvents createWorker As New BackgroundWorker
    Dim username As String
    Dim password As String
    Private Sub btn_create_Click(sender As Object, e As RoutedEventArgs) Handles btn_create.Click
        If Not AccountExist(txt_username.Text) Then
            If txt_username.Text.Length >= 6 Then
                If FirstCharNotNum(txt_username.Text) = True Then
                    If NoSpecialChar(txt_username.Text) = True Then
                        If chk_nopass.IsChecked = False Then
                            If txt_password.Password.Length >= 8 Then
                                If NoSpacePassword(txt_password.Password) = True Then
                                    If NoNumberPassword(txt_password.Password) = False Then
                                        If txt_confirm.Password = txt_password.Password Then
                                            username = txt_username.Text
                                            password = txt_password.Password
                                            IsEnabled = False
                                            brd_darken.IsEnabled = True
                                            brd_darken.Visibility = Windows.Visibility.Visible
                                            createWorker.RunWorkerAsync()
                                        Else
                                            MsgBox("Password doesn't match.", vbExclamation, "Invalid Input")
                                            txt_confirm.Password = ""
                                            txt_password.Password = ""
                                            txt_password.Focus()
                                        End If
                                    Else
                                        MsgBox("Password must contain at least 1 number! ", vbExclamation, "Invalid Input")
                                    End If
                                Else
                                    MsgBox("Password must not contain spaces! ", vbExclamation, "Invalid Input")
                                End If
                            Else
                                MsgBox("Password must be at least 8-16 characters!", vbExclamation, "Invalid Input")
                            End If
                        Else
                            username = txt_username.Text
                            password = "nopassword"
                            IsEnabled = False
                            brd_darken.IsEnabled = True
                            brd_darken.Visibility = Windows.Visibility.Visible
                            createWorker.RunWorkerAsync()
                        End If
                    Else
                        MsgBox("Username must not contain special characters! ", vbExclamation, "Invalid Input")
                    End If
                Else
                    MsgBox("Username must start with a Latin Letter, not with numbers!", vbExclamation, "Invalid Input")
                End If
            Else
                MsgBox("Username must be at least 6-16 characters!", vbExclamation, "Invalid Input")
            End If
        Else
            MsgBox("A user with this name already exist!", vbExclamation, "Invalid Input")
        End If
    End Sub
    Sub createWorker_DoWork() Handles createWorker.DoWork
        CreateNew(username, password)
    End Sub
    Sub createWorker_RunWorkerCompleted() Handles createWorker.RunWorkerCompleted
        brd_darken.IsEnabled = False
        brd_darken.Visibility = Windows.Visibility.Hidden
        LC_Window.LC_UsersListPanel.UpdateList()
        Unload()
    End Sub
#Region "Watermark"
    Private Sub txt_username_GotFocus(sender As Object, e As RoutedEventArgs) Handles txt_username.GotFocus
        lbl_username.Visibility = Windows.Visibility.Hidden
    End Sub

    Private Sub txt_username_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_username.LostFocus
        If txt_username.Text.Length = 0 Then
            lbl_username.Visibility = Windows.Visibility.Visible
        End If
    End Sub
    Private Sub txt_password_GotFocus(sender As Object, e As RoutedEventArgs) Handles txt_password.GotFocus
        If chk_nopass.IsChecked = False Then
            lbl_password.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub txt_password_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_password.LostFocus
        If txt_password.Password.Length = 0 Then
            lbl_password.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    Private Sub txt_confirm_GotFocus(sender As Object, e As RoutedEventArgs) Handles txt_confirm.GotFocus
        If chk_nopass.IsChecked = False Then
            lbl_confirm.Visibility = Windows.Visibility.Hidden
        End If
    End Sub

    Private Sub txt_confirm_LostFocus(sender As Object, e As RoutedEventArgs) Handles txt_confirm.LostFocus
        If txt_confirm.Password.Length = 0 Then
            lbl_confirm.Visibility = Windows.Visibility.Visible
        End If
    End Sub
#End Region
    Sub EnableDisableCreateButton() Handles txt_confirm.PasswordChanged, txt_password.PasswordChanged, txt_username.TextChanged
        If (txt_username.Text.Length > 0 And txt_password.Password.Length > 0 And txt_confirm.Password.Length > 0 And chk_nopass.IsChecked = False) _
            Or (txt_username.Text.Length > 0 And chk_nopass.IsChecked = True) Then
            btn_create.IsEnabled = True
        Else
            btn_create.IsEnabled = False
        End If
    End Sub

    Sub ResetBoxes()
        txt_username.Text = Nothing
        lbl_username.Visibility = Windows.Visibility.Visible

        txt_password.Password = Nothing
        lbl_password.Visibility = Windows.Visibility.Visible

        txt_confirm.Password = Nothing
        lbl_confirm.Visibility = Windows.Visibility.Visible
        If chk_nopass.IsChecked = True Then
            chk_nopass.IsChecked = False
        End If
    End Sub
    Sub Unload()
        ResetBoxes()
        If users_list.Count > 0 Then
            NextObject(LC_Window.LC_UsersListPanel)
            LC_Window.LC_HomeScreen.btn_sign.Content = "Sign-in"
        Else
            NextObject(LC_Window.LC_HomeScreen)
        End If
    End Sub

    Private Sub chk_nopass_Checked(sender As Object, e As RoutedEventArgs) Handles chk_nopass.Checked
        lbl_password.Content = "----------"
        txt_password.IsEnabled = False
        txt_password.Visibility = Windows.Visibility.Hidden
        lbl_confirm.Content = "----------"
        txt_confirm.IsEnabled = False
        txt_confirm.Visibility = Windows.Visibility.Hidden
        EnableDisableCreateButton()
    End Sub

    Private Sub chk_nopass_Unchecked(sender As Object, e As RoutedEventArgs) Handles chk_nopass.Unchecked
        lbl_password.Content = "Enter Password"
        txt_password.IsEnabled = True
        txt_password.Visibility = Windows.Visibility.Visible
        lbl_confirm.Content = "Confirm Password"
        txt_confirm.IsEnabled = True
        txt_confirm.Visibility = Windows.Visibility.Visible
        EnableDisableCreateButton()
    End Sub
    Private Sub btn_cancel_Click(sender As Object, e As RoutedEventArgs) Handles btn_cancel.Click
        Unload()
    End Sub

    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        Dim src As ImageSource = img_creating.Source
        Dim img As Image = img_creating
        ImageBehavior.SetAnimatedSource(img, src)
        ImageBehavior.SetRepeatBehavior(img, RepeatBehavior.Forever)
        ImageBehavior.SetAutoStart(img, True)
    End Sub
End Class
