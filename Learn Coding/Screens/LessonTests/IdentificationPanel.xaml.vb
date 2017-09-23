Imports Microsoft.Windows.Controls
Public Class IdentificationPanel
    Public Sub UpdateQuestion()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(txtb_question.Document, _
                            current_test.Questions(current_item).Given)
        txtb_number.Text = current_item + 1
    End Sub

    Private Sub btn_submit_Click(sender As Object, e As RoutedEventArgs) Handles btn_submit.Click
        Dim current_question = current_test.Questions(current_item)
        For i = 0 To current_question.Misc.IdentificationKeywords.Count - 1
            If txt_answer.Text.ToLower() = current_question.Misc.IdentificationKeywords(i) Then
                score += 1
                MsgBox("Correct!", vbInformation)
                current_item += 1
                txt_answer.Clear()
                LC_Window.LC_TestScreen.NextTest()
                Exit Sub
            End If
        Next
        txt_answer.Clear()
        MsgBox("Wrong!", vbExclamation)
        current_item += 1
        LC_Window.LC_TestScreen.NextTest()
    End Sub
End Class
