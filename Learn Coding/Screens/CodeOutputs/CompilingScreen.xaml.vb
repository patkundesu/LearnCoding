Imports System.Windows.Media.Animation
Imports WpfAnimatedGif
Public Class CompilingScreen
    Private Sub CodeCompilingScreen_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim src As ImageSource = img_compiling.Source
        Dim img As Image = img_compiling
        ImageBehavior.SetAnimatedSource(img, src)
        ImageBehavior.SetRepeatBehavior(img, RepeatBehavior.Forever)
        ImageBehavior.SetAutoStart(img, True)
    End Sub
End Class
