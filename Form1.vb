Imports System.Threading

Public Class Form1
    'This is the splash screen form that will be shown when the application starts.
    Private t As Thread

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the initial value of the ProgressBar to 0 and maximum to 100...
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = 100

        t = New Thread(AddressOf LoadingProgressBar)
        t.Start()
    End Sub

    Private Sub LoadingProgressBar()
        Try
            Dim i As Integer
            Dim progress As Integer = 0

            For i = 0 To 100 Step 1

                Dim v = Invoke(up, i)
                Dim v1 = Invoke(updateStatus, i)
                Thread.Sleep(100)
            Next
            Dim v2 = Invoke(closeForm)
        Catch ex As ThreadAbortException
            Err.Clear()
            Dim v = Invoke(closeForm)
        End Try
    End Sub

    'Delegate method to allows other thread to update value of Progress Bar
    Private Delegate Sub updateProgress(val As Integer)
    Private up As New updateProgress(AddressOf UpdateValue)

    'Delegate method to allows other thread to close
    Private Delegate Sub closeMe()
    Private closeForm As New closeMe(AddressOf CloseSplash)

    'Delegate method to allows other thread to update text of Label Status
    Private Delegate Sub setStatus(status As Integer)
    Private updateStatus As New setStatus(AddressOf WriteLabel)

    'Private Delegate Sub message(ByVal i As Integer)
    'Private msg As New message(AddressOf messaggio)

    'UpdateValue method has been implemented to update value of progress Bar
    Private Sub UpdateValue(val As Integer)
        ProgressBar1.Value = val
    End Sub

    'WriteLabel
    Private Sub WriteLabel(text As String)
        lbl.Text = text & " % " & " Avvio applicazione attendere... "
    End Sub

    'Close
    Private Sub CloseSplash()
        Hide()
        FormMenu.Show()
    End Sub
End Class
