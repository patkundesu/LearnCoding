Imports LCLib.Functions
Imports Microsoft.Windows.Controls
Public Class ImageScreen
    Public Sub UpdateLesson()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(HtmlTextBlock.Document, _
                                  current_unit.Lessons(current_lesson).Pages(current_page).Content)
        Dim img = current_unit.Lessons(current_lesson).Pages(current_page).Image
        LessonImage.Source = img.ImageToBitmap()
    End Sub
End Class
