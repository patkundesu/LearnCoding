Imports LCLib.CustomControls
Imports Microsoft.Windows.Controls
Public Class MultipleChoicePanel
    Public Sub DisposeChoices()
        For i = grid_choices.Children.Count - 1 To 0 Step -1
            Dim ctrl As Object = grid_choices.Children(i)
            If TypeOf (ctrl) Is MultipleChoiceButton Then
                grid_choices.Children.Remove(ctrl)
            End If
        Next
    End Sub
    Public Sub UpdateTest()
        Dim TextFormatter As New HtmlFormatter
        DisposeChoices()
        TextFormatter.SetText(txtb_question.Document, _
                            current_test.Questions(current_item).Given)
        txtb_number.Text = current_item + 1 & "."

        For i = 0 To 10
            Dim tmp As String
            Dim rnd As New Random
            Dim next_num = rnd.Next(0, 3)
            Dim current_question = current_unit.Lessons(current_lesson).Test.Questions(current_item)

            tmp = current_question.Choices(i Mod 4)
            current_question.Choices(i Mod 4) = current_question.Choices(next_num)
            current_question.Choices(next_num) = tmp
        Next

        For i = 0 To 3
            Dim choice_btn As New MultipleChoiceButton
            choice_btn.Number = i + 1
            choice_btn.Choice = current_unit.Lessons(current_lesson).Test.Questions(current_item).Choices(i)
            Dim top = 80 + 35 * (i)
            choice_btn.Margin = New Thickness(20, top, 0, 75)
            choice_btn.Tag = i
            AddHandler choice_btn.Click, Sub(s, a)
                                             If choice_btn.Choice = current_test.Questions(current_item).Answer Then
                                                 score += 1
                                                 MsgBox("Correct!", vbInformation)
                                             Else
                                                 MsgBox("Wrong!", vbExclamation)
                                             End If
                                             current_item += 1
                                             LC_Window.LC_TestScreen.NextTest()
                                         End Sub
            grid_choices.Children.Add(choice_btn)
        Next
    End Sub
End Class
