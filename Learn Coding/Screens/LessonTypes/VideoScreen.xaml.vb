Imports Microsoft.Windows.Controls
Imports LCLib.Functions
Public Class VideoScreen
    Public Sub UpdateLesson()
        Dim TextFormatter As New HtmlFormatter
        TextFormatter.SetText(HtmlTextBlock.Document, _
                                  current_unit.Lessons(current_lesson).Pages(current_page).Content)
        Dim ToUri As New UriTypeConverter

        media_video.Source = ToUri.ConvertFrom(current_unit.Lessons(current_lesson).Pages(current_page).Links.Video)
        media_video.Play()
    End Sub
    Private Sub btn_play_Click(sender As Object, e As RoutedEventArgs) Handles btn_play.Click
        media_video.Play()
    End Sub
    Private TotalTime As TimeSpan
    Private Sub btn_pause_Click(sender As Object, e As RoutedEventArgs) Handles btn_pause.Click
        media_video.Pause()
    End Sub
    Private timer As New System.Windows.Threading.DispatcherTimer
    Private Sub media_opened(sender As Object, e As EventArgs) Handles media_video.MediaOpened
        TotalTime = media_video.NaturalDuration.TimeSpan
        timelineSlider.Maximum = media_video.NaturalDuration.TimeSpan.TotalSeconds
        AddHandler timer.Tick, Sub(s, ev)
                                   If media_video.NaturalDuration.TimeSpan.TotalSeconds > 0 Then
                                       timelineSlider.Value = media_video.Position.TotalSeconds
                                   End If
                               End Sub
        timer.Interval = TimeSpan.FromSeconds(1)
        timer.Start()
        AddHandler timelineSlider.MouseLeftButtonUp, AddressOf SeekToMediaPosition
    End Sub
    Private Sub media_stop(sender As Object, e As EventArgs) Handles media_video.MediaEnded
        timer.Stop()
    End Sub
    Private Sub btn_stop_Click(sender As Object, e As RoutedEventArgs) Handles btn_stop.Click
        media_video.Stop()
    End Sub

    Private Sub SeekToMediaPosition()
        Dim SliderValue As Integer = CType(timelineSlider.Value, Integer)

        ' Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds. 
        ' Create a TimeSpan with miliseconds equal to the slider value. 
        Dim ts As New TimeSpan(0, 0, SliderValue)
        media_video.Position = ts
    End Sub
End Class
