Imports Microsoft.Windows.Controls
Public Class TextScreen
    Public Sub UpdateLesson()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(HtmlTextBlock.Document, _
                                  current_unit.Lessons(current_lesson).Pages(current_page).Content)
    End Sub
End Class
