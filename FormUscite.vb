Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Public Class FormUscite
    Inherits Form
    Private ReadOnly thisDataSet As New DataSet
    Private ReadOnly dataGridView1 As New DataGridView()
    Private ReadOnly bindingSource1 As New BindingSource()
    Private dataAdapter As New SqlDataAdapter()
    Private WithEvents ExitButton As New Button()
    Private WithEvents UpdateButton As New Button()

    Public Shared Sub Main()
        Application.Run(New FormEntrate())
    End Sub

    ' Initialize the form.
    Public Sub New()
        InitializeComponent()

        With Me
            .StartPosition = FormStartPosition.CenterParent
            .Height = 456
            .Width = 566
            .MinimizeBox = False
            .MaximizeBox = False
            .FormBorderStyle = FormBorderStyle.FixedDialog
        End With
        dataGridView1.Dock = DockStyle.Fill
        dataGridView1.ScrollBars = ScrollBars.Both

        Dim objAlternatingCellStyle As New DataGridViewCellStyle With {
            .BackColor = Color.YellowGreen,
            .ForeColor = Color.Black
        }

        dataGridView1.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle

        ExitButton.Text = "Esci"
        ExitButton.ForeColor = Color.Black
        UpdateButton.Text = "Aggiorna"
        UpdateButton.ForeColor = Color.Black
        Dim panel As New FlowLayoutPanel With {
            .Dock = DockStyle.Top,
            .AutoSize = True
                    }
        panel.Controls.AddRange(New Control() {ExitButton, UpdateButton})

        Controls.AddRange(New Control() {dataGridView1, panel})
        Text = "Uscite"

    End Sub

    Private Sub GetData(selectCommand As String)

        Try
            ' Specify a connection string.  Cassa.My.MySettings.DbCassaConnectionString
            Dim connectionString As String = ConnectionStrings("Cassa.My.MySettings.DbCassaConnectionString").ConnectionString
            ' Create a new data adapter based on the specified query.
            dataAdapter = New SqlDataAdapter(selectCommand, connectionString)
            ' Create a command builder to generate SQL update, insert, and
            ' delete commands based on selectCommand. 
            Dim commandBuilder As New SqlCommandBuilder(dataAdapter)
            ' Populate a new data table and bind it to the BindingSource.
            Dim table As New DataTable With {
                .Locale = Globalization.CultureInfo.InvariantCulture
            }
            Dim v = dataAdapter.Fill(table)
            bindingSource1.DataSource = table
            Dim v1 = dataAdapter.Fill(thisDataSet, "table")
            ' Resize the DataGridView columns to fit the newly loaded content.
            dataGridView1.AutoResizeColumns(
            DataGridViewAutoSizeColumnsMode.AllCells)
        Catch ex As SqlException
            Dim dialogResult1 = MessageBox.Show("Per esguire l'applicazione " +
                "impostare una variabile stringa " +
                "valida per il sitema.")
        End Try

    End Sub

    Private Sub FormEntrate_Load(sender As Object, e As EventArgs) _
        Handles Me.Load
        ' Set the DataGridView properties and Bind it to the BindingSource
        ' and load the data from the database.
        dataGridView1.AutoGenerateColumns = True
        dataGridView1.DataSource = bindingSource1
        GetData("select * from [dbo].[Table] where (Operazione = N'USCITA')")

        Dim objAlternatingCellStyle As New DataGridViewCellStyle With {
            .BackColor = Color.YellowGreen,
            .ForeColor = Color.Black
        }

        dataGridView1.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        Dim objCurrencyCellStyle As New DataGridViewCellStyle With {
            .Format = "c"
        }
        dataGridView1.Columns("Importo").DefaultCellStyle = objCurrencyCellStyle
        dataGridView1.Rows(0).Selected = True
    End Sub
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        ' Dim v = dataAdapter.Update(CType(bindingSource1.DataSource, DataTable))
        Dim DialogResult = MsgBox("Le modifiche prevedono la pulizia delle caselle interessate. Vuoi aggiornare il database?", MsgBoxStyle.YesNo, "Aggiornamento")
        If DialogResult = MsgBoxResult.No Then
            Return
        ElseIf DialogResult = MsgBoxResult.Yes Then
            ' Aggiornamento del database...
            Dim v = dataAdapter.Update(CType(bindingSource1.DataSource, DataTable))
            MsgBox("Aggiornamento avvenuto con successo.")
        End If
    End Sub
    Private Sub FormEntrate_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        dataGridView1.Rows(0).Selected = True
    End Sub
End Class