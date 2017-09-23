Imports System.Windows.Threading
Imports LCLib.Functions
Public Class WaitingScreen
    Dim num As Integer
    Dim WithEvents timer As DispatcherTimer
    Public Sub ActivateTimer()
        timer = New DispatcherTimer
        prog_bar.Value = 0
        timer.Interval = New TimeSpan(0, 0, 0, 0, 1)
        timer.Start()
    End Sub
    Public Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        If prog_bar.Value < prog_bar.Maximum Then
            prog_bar.Value += 1
        Else
            HideObject()
            LC_Window.LC_TestScreen.ShowTest()
            timer.Stop()
        End If
    End Sub
End Class
