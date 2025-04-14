Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Public Class FormScritture
    Inherits Form
    ' Oggetti...
    Private ReadOnly thisDataSet As New DataSet
    Private ReadOnly dataGridView1 As New DataGridView()
    Private ReadOnly bindingSource1 As New BindingSource()
    Private dataAdapter As New SqlDataAdapter()
    Private WithEvents ExitButton As New Button()
    Private WithEvents UpdateButton As New Button()
    Private WithEvents TotalButton As New Button()
    Public Shared Sub Main()
        Application.Run(New FormScritture())
    End Sub

    ' Inizializzazione del Form...
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
        Text = "Scritture"
    End Sub

    Private Sub GetData(selectCommand As String)
        Try
            ' Specificazione di una stringa di connessione...
            Dim connectionString As String = ConnectionStrings("Cassa.My.MySettings.DbCassaConnectionString").ConnectionString
            ' Creazione un nuovo adattatore dati basato sulla query specificata...
            dataAdapter = New SqlDataAdapter(selectCommand, connectionString)
            ' Creazione di un generatore di comandi per generare aggiornamento SQL, inserimento, e
            ' elimina i comandi basati su selectCommand...
            Dim commandBuilder As New SqlCommandBuilder(dataAdapter)
            ' Popolamento di una nuova tabella di dati e associazione al BindingSource...
            Dim table As New DataTable With {
                .Locale = Globalization.CultureInfo.InvariantCulture
            }
            Dim v = dataAdapter.Fill(table)
            bindingSource1.DataSource = table
            Dim v1 = dataAdapter.Fill(thisDataSet, "table")
            ' Ridimensionamento delle colonne DataGridView per adattarle al contenuto appena caricato...
            dataGridView1.AutoResizeColumns(
            DataGridViewAutoSizeColumnsMode.AllCells)
        Catch ex As SqlException
            Dim dialogResult1 = MessageBox.Show("Per esguire l'applicazione " +
                "impostare una variabile stringa " +
                "valida per il sitema.")
        End Try

    End Sub

    Private Sub FormScritture_Load(sender As Object, e As EventArgs) _
        Handles Me.Load
        ' Impostazione delle proprietà di DataGridView e associazione al BindingSource,
        ' infine caricamento dei dati dal database...
        dataGridView1.AutoGenerateColumns = True
        dataGridView1.DataSource = bindingSource1
        GetData("select Id, Data, Operazione, Descrizione, Importo from [dbo].[Table] order by Id")

        Dim objAlternatingCellStyle As New DataGridViewCellStyle With {
            .BackColor = Color.YellowGreen,
            .ForeColor = Color.Black
        }

        dataGridView1.AlternatingRowsDefaultCellStyle = objAlternatingCellStyle
        Dim objDateTimeCellStyle As New DataGridViewCellStyle With {
            .Format = "d"
        }
        Dim objCurrencyCellStyle As New DataGridViewCellStyle With {
            .Format = "c"
        }
        dataGridView1.Columns("Data").DefaultCellStyle = objDateTimeCellStyle
        dataGridView1.Columns("Importo").DefaultCellStyle = objCurrencyCellStyle
    End Sub
    Private Sub ExitButton_Click(sender As Object, e As EventArgs) Handles ExitButton.Click
        ' Chiusura del Form...
        Me.Close()
    End Sub
    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        Dim DialogResult = MsgBox("Le modifiche prevedono la pulizia delle caselle interessate. Vuoi aggiornare il database?", MsgBoxStyle.YesNo, "Aggiornamento")
        If DialogResult = MsgBoxResult.No Then
            Return
        ElseIf DialogResult = MsgBoxResult.Yes Then
            ' Aggiornamento del database...
            Dim v = dataAdapter.Update(CType(bindingSource1.DataSource, DataTable))
            MsgBox("Aggiornamento avvenuto con successo.")
        End If
    End Sub
    Private Sub FormScritture_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ' Prima riga del DataGridView...
        dataGridView1.Rows(0).Selected = True
    End Sub
End Class