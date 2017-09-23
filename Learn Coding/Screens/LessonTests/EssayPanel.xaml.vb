Imports Microsoft.Windows.Controls
Imports System.Text.RegularExpressions
Imports System.IO
Public Class EssayPanel
    Dim essay_folder As String
    Public Sub UpdateTest()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(txtb_question.Document, _
                            current_test.Questions(current_item).Given)
        txtb_number.Text = current_item + 1
    End Sub
    Sub EnableDisableSubmitButton() Handles txt_answer.TextChanged
        Static regex As New Regex("\b", System.Text.RegularExpressions.RegexOptions.Compiled Or System.Text.RegularExpressions.RegexOptions.Multiline)
        Dim number_of_words As Integer = 5
        number_of_words = regex.Matches(txt_answer.Text).Count / 2
        nwords.Text = number_of_words.ToString
        If current_item < current_test.Questions.Count Then
            If number_of_words >= current_test.Questions(current_item).Misc.EssayLimit Then
                btn_submit.IsEnabled = True
            Else
                btn_submit.IsEnabled = False
            End If
        End If
    End Sub
    Public Sub SubmitEssay() Handles btn_submit.Click
        Dim essay_str As New System.Text.StringBuilder
        Dim question As String = New TextRange(txtb_question.Document.ContentStart, txtb_question.Document.ContentEnd).Text
        essay_str.AppendLine("Question: " & question)
        essay_str.AppendLine(txt_answer.Text)
        CheckEssayFolder()
        File.AppendAllText(essay_folder & "\" & current_unit.Language & "-" & current_unit.Lessons(current_lesson).Number & "-essay.txt", essay_str.ToString)
        MsgBox("Essay Submitted!", vbInformation)
        score += 1
        current_item += 1
        txt_answer.Text = ""
        LC_Window.LC_TestScreen.NextTest()
    End Sub
    Public Sub CheckEssayFolder()
        essay_folder = users_dir & "\" & current_user.Username & "\essay"
        If Not System.IO.Directory.Exists(essay_folder) Then
            System.IO.Directory.CreateDirectory(essay_folder)
        End If
    End Sub
End Class
