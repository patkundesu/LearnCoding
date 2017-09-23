Imports LCLib
Imports LCLib.Functions
Module LearnCoding
    Public LC_Window As MainWindow

    Public base_dir As String = AppDomain.CurrentDomain.BaseDirectory
    Public Sub LearnCoding_StartUp()
        LoadLessons()
        LoadTests()
        LoadUsers()
    End Sub
    Public Sub LoadSettings()
        SetResolution()
        StoryboardExtensions.Transition = My.Settings.transitions
    End Sub
    Public Sub SetResolution()
        Select Case My.Settings.aspect_ratio
            Case "4:3"
                LC_Window.LC_Grid.Width = 800
                LC_Window.LC_Grid.Height = 600
            Case "5:4"
                LC_Window.LC_Grid.Width = 800
                LC_Window.LC_Grid.Height = 640
            Case "Netbook"
                LC_Window.LC_Grid.Width = 896
                LC_Window.LC_Grid.Height = 525
            Case "LCD"
                LC_Window.LC_Grid.Width = 853.75
                LC_Window.LC_Grid.Height = 480
        End Select
    End Sub

#Region "Password_Validation"
    Public Function FirstCharNotNum(userN) As Boolean
        If Asc("a") <= Asc(userN.Chars(0)) And Asc(userN.Chars(0)) <= Asc("z") _
                   Or Asc("A") <= Asc(userN.Chars(0)) And Asc(userN.Chars(0)) <= Asc("Z") _
                   Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function NoSpecialChar(userN As String) As Boolean
        For i = 0 To userN.Length - 1
            If Asc(userN.Chars(i)) <> Asc("_") And (Asc("0") > Asc(userN.Chars(i)) Or (Asc(userN.Chars(i)) > Asc("9") _
                    And Asc("A") > Asc(userN.Chars(i))) Or (Asc(userN.Chars(i)) > Asc("Z") _
                    And Asc("a") > Asc(userN.Chars(i))) Or Asc(userN.Chars(i)) > Asc("z")) _
                    Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function NoSpacePassword(passW As String) As Boolean
        For i = 0 To passW.Length - 1
            If passW.Chars(i) = " " Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function NoNumberPassword(passW As String) As Boolean
        For i = 0 To passW.Length - 1
            If Asc("0") <= Asc(passW.Chars(i)) And Asc(passW.Chars(i)) <= Asc("9") Then
                Return False
            End If
        Next
        Return True
    End Function
#End Region
End Module
