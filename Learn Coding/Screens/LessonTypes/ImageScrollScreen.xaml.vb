Imports LCLib.Functions
Public Class ImageScrollScreen
    Public Sub UpdateLesson()
        Dim img = current_unit.Lessons(current_lesson).Pages(current_page).Image
        img_lesson.Source = img.ImageToBitmap
    End Sub
End Class
