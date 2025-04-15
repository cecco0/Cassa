Imports System.Configuration.ConfigurationManager
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WinForms
Public Class FormAnteprima
    Private Sub FormAnteprima_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using dsTable As DataSet1 = GetData()
            Dim datasource As New ReportDataSource("dsTable", dsTable.Tables(0))
            ReportViewer1.LocalReport.DataSources.Clear()
            ReportViewer1.LocalReport.DataSources.Add(datasource)
        End Using

        Me.ReportViewer1.RefreshReport()
    End Sub
    Private Function GetData() As DataSet1
        ' Connessione...
        Dim connectionString As String = ConnectionStrings("Cassa.My.MySettings.DbCassaConnectionString").ConnectionString
        Using con As New SqlConnection(connectionString)
            ' SQLCommand per il recupero dati...
            Using cmd As New SqlCommand("SELECT * FROM [dbo].[Table] ORDER BY Id")
                ' SQLDataAdapter...
                Using sda As New SqlDataAdapter()
                    cmd.Connection = con
                    sda.SelectCommand = cmd
                    ' Creazione di un nuovo oggetto DataSet...
                    Using dsTable As New DataSet1()
                        ' Associazioe dati...
                        sda.Fill(dsTable, "Table")
                        Return dsTable
                    End Using
                End Using
            End Using
        End Using
    End Function
End Class