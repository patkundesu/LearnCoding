Imports LCLib
Imports LCLib.Functions
Imports LCLib.CustomControls
Public Class UsersListPanel
    Public Sub UpdateList()
        ClearList()
        For Each user In users_list
            Dim user_btn = New UserSelectButton

            user_btn.User = user
            user_btn.Username = user.Username
            Dim top As Double = users_list.IndexOf(user) * 40 + 20
            user_btn.Margin = New Thickness(20, top, 0, 75)
            user_btn.Tag = users_list.IndexOf(user)
            If Equals(user.Progress.Current_Unit, Fundamentals) And _
                user.Progress.Current_Lesson = 0 And _
                user.Progress.Current_Page = 0 Then
                user_btn.StartLoad = "Start"
            Else
                user_btn.StartLoad = "Load"
            End If

            Dim remove_btn = New UserRemoveButton()
            remove_btn.Margin = New Thickness(0, top + 10, 20, 75)
            remove_btn.Tag = users_list.IndexOf(user)

            Dim user_click As RoutedEventHandler = Sub()
                                                       current_user = users_list(users_list.IndexOf(user))
                                                       If current_user.Password <> "nopassword" Then
                                                           LC_Window.brd_darken.ShowObject()
                                                           LC_Window.LC_UserSignInPanel.ShowObject()
                                                           IsEnabled = False
                                                           LC_Window.LC_UserSignInPanel.txtb_user.Text = current_user.Username
                                                       Else
                                                           SignIn()
                                                           NextObject(LC_Window.LC_UserHomeScreen)
                                                       End If
                                                   End Sub
            AddHandler user_btn.Click, user_click
            Dim remove_click As RoutedEventHandler = Sub()
                                                         current_user = users_list(remove_btn.Tag)
                                                         If current_user.Password <> "nopassword" Then
                                                             LC_Window.brd_darken.ShowObject()
                                                             LC_Window.LC_UserDeletePanel.ShowObject()
                                                             IsEnabled = False

                                                             LC_Window.LC_UserDeletePanel.txtb_user.Text = users_list(remove_btn.Tag).Username
                                                             LC_Window.LC_UserDeletePanel.txtb_user.TextDecorations = TextDecorations.Underline
                                                         Else
                                                             Dim choice As Integer = MsgBox("Are you sure you want to delete " & current_user.Username & "?" & vbCrLf _
                                                                & "This action cannot be undone.", vbYesNo + vbExclamation)
                                                             If choice = vbYes Then
                                                                 DeleteUser()
                                                             End If
                                                         End If
                                                     End Sub
            AddHandler remove_btn.Click, remove_click

            grid_selectuser.Children.Add(user_btn)
            grid_selectuser.Children.Add(remove_btn)
        Next
    End Sub
    Public Sub ClearList()
        For i = grid_selectuser.Children.Count - 1 To 0 Step -1
            Dim ctrl = grid_selectuser.Children(i)
            If TypeOf (ctrl) Is UserSelectButton Or TypeOf (ctrl) Is UserRemoveButton Then
                grid_selectuser.Children.Remove(ctrl)
            End If
        Next
    End Sub

    Private Sub btn_back_Click(sender As Object, e As RoutedEventArgs) Handles btn_back.Click
        NextObject(LC_Window.LC_HomeScreen)
    End Sub

    Private Sub btn_create_Click(sender As Object, e As RoutedEventArgs) Handles btn_create.Click
        NextObject(LC_Window.LC_UserCreatePanel)
    End Sub
End Class
