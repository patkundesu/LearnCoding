Imports Microsoft.Windows.Controls
Imports LCLib.CustomControls
Imports WpfAnimatedGif
Imports System.Windows.Media.Animation
Public Class CodingPanel
    Public _codetries As Integer
    Public _output As String
    Public Sub CodingPanel_Loaded() Handles Me.Loaded
        Dim src As ImageSource = img_compiling.Source
        Dim img As Image = img_compiling
        ImageBehavior.SetAnimatedSource(img, src)
        ImageBehavior.SetRepeatBehavior(img, RepeatBehavior.Forever)
        ImageBehavior.SetAutoStart(img, True)
    End Sub
    Public Sub UpdateTest()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(txtb_question.Document, _
                            current_test.Questions(current_item).Given)
        txtb_number.Text = current_item + 1

        code_editor.Text = current_test.Questions(current_item).Code
        txt_class.Text = current_test.Questions(current_item).ClassName
        _codetries = current_test.Questions(current_item).Tries
        txtb_tries.Text = _codetries.ToString
        code_editor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition(current_test.Questions(current_item).Language)
        Language_Used = current_test.Questions(current_item).Language
        Select Case Language_Used
            Case "Java", "C++"
                test_console.IsEnabled = True
                test_console.Visibility = Windows.Visibility.Visible
                test_browser.IsEnabled = False
                test_browser.Visibility = Windows.Visibility.Hidden
            Case "HTML", "JavaScript", "PHP"
                test_console.IsEnabled = False
                test_console.Visibility = Windows.Visibility.Hidden
                test_browser.IsEnabled = True
                test_browser.Visibility = Windows.Visibility.Visible
        End Select
    End Sub

    Private Sub btn_cnr_Click(sender As Object, e As RoutedEventArgs) Handles btn_cnr.Click
        test_console.StopProcess()
        test_console.ClearConsole()
        ClassName_Edited = txt_class.Text
        brd_compiling.Visibility = Windows.Visibility.Visible
        If current_test.Questions(current_item).Misc.CodingInputs.Count = 1 Then
            Select Case Language_Used
                Case "Java", "C++"
                    Code_Edited = code_editor.Text
                    AddHandler test_console.OnExit, AddressOf Running_Finished
                Case "HTML", "JavaScript", "PHP"
                    Code_Edited = code_editor.Text
                    AddHandler test_browser.LoadCompleted, AddressOf Browser_Navigated
            End Select
        End If
        testWorker.RunWorkerAsync()
    End Sub

    Public Sub Compiled_Finished()
        brd_compiling.Visibility = Windows.Visibility.Hidden
        Select Case Language_Used
            Case "Java", "C++"
                test_console.IsEnabled = True
                test_console.Visibility = Windows.Visibility.Visible
                test_browser.IsEnabled = False
                test_browser.Visibility = Windows.Visibility.Hidden
            Case "HTML", "JavaScript", "PHP"
                test_console.IsEnabled = False
                test_console.Visibility = Windows.Visibility.Hidden
                test_browser.IsEnabled = True
                test_browser.Visibility = Windows.Visibility.Visible
                test_browser.NavigateToString(ProgramOutput)
        End Select
    End Sub
    Public Sub Compile_Error()
        RemoveHandler test_console.OnExit, AddressOf Running_Finished
        Dim a As String = IIf(_codetries > 1, "tries", "try")
        _codetries -= 1
        MsgBox("Wrong! " & _codetries & " more " & a & "!", vbExclamation)
        txtb_tries.Text = _codetries
        If (_codetries = 0) Then
            MsgBox("You used all " & current_test.Questions(current_item).Tries & " tries. You're completely wrong!")
            current_item += 1
            LC_Window.LC_TestScreen.NextTest()
        End If
    End Sub
    Public Sub Running_Finished(sender As Object, e As ProcessEventArgs)
        RemoveHandler test_console.OnExit, AddressOf Running_Finished
        _output = test_console.Text.TrimEnd(vbCrLf.ToCharArray)
        For Each _expected_output In current_test.Questions(current_item).Misc.CodingOutputs
            If _output = _expected_output Then
                MsgBox("Correct!", vbInformation)
                score += _codetries
                current_item += 1
                test_console.ClearConsole()
                LC_Window.LC_TestScreen.NextTest()
                Exit Sub
            End If
        Next
        _codetries -= 1
        Dim a As String = IIf(_codetries > 1, "tries", "try")
        MsgBox("Wrong! " & _codetries & " more " & a & "!", vbExclamation)
        txtb_tries.Text = _codetries
        If (_codetries = 0) Then
            MsgBox("You used all " & current_test.Questions(current_item).Tries & " tries. You're completely wrong!")
            current_item += 1
            LC_Window.LC_TestScreen.NextTest()
        End If
    End Sub
    Public Sub Browser_Navigated(sender As Object, e As NavigationEventArgs)
        RemoveHandler test_browser.LoadCompleted, AddressOf Browser_Navigated
        _output = test_browser.Document.body.innerHTML

        For Each _expected_output In current_test.Questions(current_item).Misc.CodingOutputs
            If _output = _expected_output Then
                MsgBox("Correct!", vbInformation)
                score += _codetries
                current_item += 1
                test_console.ClearConsole()
                LC_Window.LC_TestScreen.NextTest()
                Exit Sub
            End If
        Next
        Dim a As String = IIf(_codetries > 1, "tries", "try")
        _codetries -= 1
        MsgBox("Wrong! " & _codetries & " more " & a & "!", vbExclamation)
        txtb_tries.Text = _codetries
        If (_codetries = 0) Then
            MsgBox("You used all " & current_test.Questions(current_item).Tries & " tries. You're completely wrong!")
            current_item += 1
            LC_Window.LC_TestScreen.NextTest()
        End If
    End Sub
End Class
