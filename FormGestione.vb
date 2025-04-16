Imports System.Configuration.ConfigurationManager
Imports System.Data.SqlClient
Imports System.Threading
Imports Microsoft.VisualBasic.FileIO
Public Class FormGestione
    ' La finestra di progettazione richiede questa procedura...
    ' Dichiarazione degli oggetti e variabili locali...
    Private connectionString As String = ConnectionStrings("Cassa.My.MySettings.DbCassaConnectionString").ConnectionString
    Private dataAdapter As New SqlDataAdapter(
           "SELECT Id, Data, Operazione, Descrizione, Importo FROM [dbo].[Table]", connectionString)
    Private dataSet As DataSet
    Private dataView As DataView
    Private currencyManager As CurrencyManager

    Public Sub New()
        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()
        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().

    End Sub
    Private ReadOnly Property DateTimePicker11 As DateTimePicker
        Get
            Return DateTimePicker1
        End Get
    End Property

    Private Sub FillDataSetAndView()
        ' Nuova istanza dell'oggetto DataSet...
        dataSet = New DataSet
        ' Riempimento dell'oggetto DataSet...
        Dim unused = dataAdapter.Fill(dataSet, "[dbo].[Table]")
        ' Impostazione dell'oggetto DataView sull'oggetto DataSet...
        dataView = New DataView(dataSet.Tables("[dbo].[Table]"))
        ' Impostazione dell'oggettoCurrencyManager sull'oggetto DataView...
        currencyManager = CType(BindingContext(dataView), CurrencyManager)
    End Sub

    Private Sub BindFields()
        ' Cancellazione di eventuali associazioni precedenti...
        TextBox1.DataBindings.Clear()
        DateTimePicker1.DataBindings.Clear()
        TextBox2.DataBindings.Clear()
        TextBox3.DataBindings.Clear()
        TextBox4.DataBindings.Clear()
        ' Aggiunta di nuove associazioni all'oggetto DataView...
        Dim binding = TextBox1.DataBindings.Add("Text", dataView, "Id")
        Dim binding1 = DateTimePicker1.DataBindings.Add("Text", dataView, "Data")
        Dim binding2 = TextBox2.DataBindings.Add("Text", dataView, "Operazione")
        Dim binding3 = TextBox3.DataBindings.Add("Text", dataView, "Descrizione")
        Dim binding4 = TextBox4.DataBindings.Add("Text", dataView, "Importo")
    End Sub

    Private Sub ShowPosition()
        'Impostazione formato della data...
        'e formattazione...
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        ' Formattazione dell'importo...
        Try
                Dim importo As Decimal
            If Decimal.TryParse(TextBox4.Text, importo) Then
                TextBox4.Text = FormatCurrency(importo, 2, TriState.True, TriState.True)
            End If
        Catch ex As FormatException
                MessageBox.Show("Errore di formattazione: " & ex.Message)
            TextBox4.Text = "0.00"
            TextBox4.Text = FormatCurrency(TextBox4.Text, 2, TriState.True, TriState.True)

        End Try
        TextBox9.Text = $"{currencyManager.Position + 1} di {currencyManager.Count}"
    End Sub
    Private Sub FrmGestione_Load(sender As Object, e As EventArgs) Handles Me.Load
        'ToolStripStatusLabelGst.Text = ""
        ' Aggiunta degli elementi alla casella combinata...
        Dim v = ComboBox1.Items.Add("Data")
        Dim v1 = ComboBox1.Items.Add("Operazione")
        Dim v2 = ComboBox1.Items.Add("Descrizione")
        Dim v3 = ComboBox1.Items.Add("Importo")
        ' Selezione del primo elemento visualizzato nella lista...
        ComboBox1.SelectedIndex = 0
        ' Compilazione dell'oggetto DataSet...
        FillDataSetAndView()
        ' Associazione dei dati ai campi...
        BindFields()
        ' Formattazione data e visualizzazione della posizione della scrittura corrente...
        ShowPosition()
        ' Disabilitazione del campo Id impiegato per le sole aggiunte...
        MethodTotali()
        FormatData()
        TextBox1.Enabled = False
        Button5.Enabled = False
        DateTimePicker1.Focus()
        Timer1.Enabled = True
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If ToolStripProgressBar1.Value <= ToolStripProgressBar1.Maximum - 1 Then
            ToolStripProgressBar1.Value += 10
            ToolStripStatusLabel1.Text = $"Lettura dati."
            ' Ciclo If per verifica stato tra Value e Maximum e caricamento dei dati...
            If ToolStripProgressBar1.Value = ToolStripProgressBar1.Maximum Then
                ' Assegnazione del numero di scritture presenti nel database...
                Dim a As Integer = currencyManager.Count
                ' Messaggio...
                Dim v As String = $" Pronto. Elaborate {a} scritture"
                ToolStripStatusLabel1.Text = v
                ' Disattivazione del Timer1...
                Thread.Sleep(100)
                Timer1.Enabled = False
                ToolStripProgressBar1.Visible = False
            End If
        Else
        End If
    End Sub
    Private Sub FormGestione_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        ' Pulizia lista della casella combinata...
        ComboBox1.Items.Clear()
        ' Pulizia dei testi della ToolStripStatusLabel...
        ToolStripStatusLabel1.Text = ""
        ' Pulizia dei testi nella casella di ricerche...
        TextBox8.Text = ""
        'Ripristino dei controlli...
        RipristinaControlli()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Posizionamento prima Scrittura...
        currencyManager.Position = 0
        ' Visualizzazione della posizione della Scrittura corrente, ovvero la prima...
        ShowPosition()
        ToolStripStatusLabel1.Text = "Prima scrittura del database."
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Posizionamento sulla Scrittura precedente...
        currencyManager.Position -= 1
        ' Visualizzazione della posizione della Scrittura corrente, ovvero la precedente...
        ShowPosition()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Posizionamento sulla Scrittura successiva...
        currencyManager.Position += 1
        ' Visualizzazione della posizione della Scrittura corrente, ovvero la successiva...
        ShowPosition()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Posizionamento sull'ultima Scrittura...
        currencyManager.Position = currencyManager.Count - 1
        ' Visualizzazione della posizione della Scrittura corrente, ovvero l'ultima...
        ShowPosition()
        ToolStripStatusLabel1.Text = "Ultima scrittura del database."
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Ordinamento delle proprietà dell'oggetto DataView...
        Select Case ComboBox1.SelectedIndex
            Case 0
                dataView.Sort = "Data"
            Case 1
                dataView.Sort = "Operazione"
            Case 2
                dataView.Sort = "Descrizione"
            Case 3
                dataView.Sort = "Importo"
        End Select
        ' Chiamata dell'evento click per il pulsante MoveFirst...
        Button8_Click(Nothing, Nothing)
        ' Visualizzazione del messaggio dell'avvenuto riordinamento delle scritture...
        ToolStripStatusLabel1.Text = "Scritture riordinate."
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Ciclo If per la verifica se i criteri sono stati immessi o meno nell'apposita casella...
        If TextBox8.Text = "" Then
            ToolStripStatusLabel1.Text = "Azione interrotta. Scelgliere il campo ed immettere i criteri di ricerca."
            Exit Sub
        End If
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Ordinamento delle proprietà dell'oggetto DataView...
        Dim intPosition As Integer
        Select Case ComboBox1.SelectedIndex
            Case 0
                dataView.Sort = "Data"
            Case 1
                dataView.Sort = "Operazione"
            Case 2
                dataView.Sort = "Descrizione"
            Case 3
                dataView.Sort = "Importo"
            Case Else
        End Select
        ' Se il campo di ricerca non è il prezzo o non è la data, allora...
        If ComboBox1.SelectedIndex = 1 Or ComboBox1.SelectedIndex = 2 Then
            intPosition = dataView.Find(TextBox8.Text)
            ' Altrimenti trova il prezzo e la data...
        ElseIf ComboBox1.SelectedIndex = 0 Then
            intPosition = dataView.Find(CType(TextBox8.Text, Date))
        Else
            intPosition = dataView.Find(CType(TextBox8.Text, Decimal))
        End If

        If intPosition = -1 Then
            ' Visualizzazione del messaggio il quale avverte che la Scrittura non è stata trovata...
            ToolStripStatusLabel1.Text = "Scrittura non trovata."
            TextBox8.Text = ""
        Else
            ' Altrimenti visualizzazione di un messaggio che informa
            ' che il record è stato trovato e riposizionamento su quel record...
            ToolStripStatusLabel1.Text = "Scrittura trovata."
            TextBox8.Text = ""
            currencyManager.Position = intPosition
        End If
        ' Visualizzazione della posizione della Scrittura corrente...
        ShowPosition()
    End Sub
    Private Sub MethodTotali()
        ' Connessione al database...
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            ' Creazione dei comandi per il calcolo delle entrate e uscite...
            Dim cmdEntrate As New SqlCommand("SELECT SUM(Importo) As TotaleEntrate
                   FROM [dbo].[Table]
                   WHERE (Operazione = N'ENTRATA')
                   OR Importo IS NULL", conn)
            Dim cmdUscite As New SqlCommand("SELECT SUM(Importo) As TotaleUscite
                   FROM [dbo].[Table]
                   WHERE (Operazione = N'USCITA')
                   OR Importo IS NULL", conn)
            ' Aggiunta dei parametri...
            Dim sqlParameter1 = cmdEntrate.Parameters.AddWithValue("@Importo", TextBox5.Text).DbType =
            DbType.Decimal
            Dim sqlParameter = cmdUscite.Parameters.AddWithValue("@Importo", TextBox5.Text).DbType =
            DbType.Decimal
            ' Esecuzione dei comandi per il calcolo delle entrate e uscite...
            Try
                Dim Entrate = cmdEntrate.ExecuteScalar()
                TextBox5.Text = CStr(If(Entrate IsNot DBNull.Value, Entrate, "0"))
            Catch ex As Exception
                Dim DialogResult = MessageBox.Show(ex.Message)
            End Try
            Try
                Dim Uscite = cmdUscite.ExecuteScalar
                TextBox6.Text = CStr(If(Uscite IsNot DBNull.Value, Uscite, "0"))
            Catch ex As Exception
                Dim DialogResult = MessageBox.Show(ex.Message)
            End Try
            conn.Close()
            ' Calcolo del totale delle entrate e uscite...
            Dim totEntrate As String = TextBox5.Text
            Dim totUscite As String = TextBox6.Text
            'Saldo finale...
            Dim totale As String = totEntrate - totUscite
            TextBox7.Text = totale
        End Using
    End Sub
    Private Sub FormatData()
        ' Formattazione dei dati...
        Try
            TextBox5.Text = FormatCurrency(TextBox5.Text, 2, TriState.True, TriState.True)
            TextBox6.Text = FormatCurrency(TextBox6.Text, 2, TriState.True, TriState.True)
            TextBox7.Text = FormatCurrency(TextBox7.Text, 2, TriState.True, TriState.True)
        Catch ex As Exception
            Dim unused = MessageBox.Show(ex.Message)
            TextBox5.Text = "0.00"
            TextBox6.Text = "0.00"
            TextBox7.Text = "0.00"
            TextBox5.Text = FormatCurrency(TextBox5.Text, 2, TriState.True, TriState.True)
            TextBox6.Text = FormatCurrency(TextBox6.Text, 2, TriState.True, TriState.True)
            TextBox7.Text = FormatCurrency(TextBox7.Text, 2, TriState.True, TriState.True)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' chiamate delle routine per il calcolo dei totali e formattazione dei dati...
        MethodTotali()
        FormatData()
    End Sub
    Private Sub FormGestione_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ' Riapertura del form e formattazione dati...
        ShowPosition()
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Quesito relativo a richiesta di conferma o annullamento azione...
        ' Risposta positiva, disattivazione di quei comandi che ....
        If MsgBox("Proseguire con l'azione ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) =
        MsgBoxResult.Yes Then
            ToolStripStatusLabel1.Text = "Per aggiungere la scrittura immettere tutti i dati. Per annullare chiudere il form solo dopo aver immesso comunque i dati."
            PreparaAggiunta()
            ' Se la risposta è negativa, allora...
        ElseIf MsgBoxResult.No Then
            ToolStripStatusLabel1.Text = "Azione annullata."
            ShowPosition()
            RipristinaControlli()
        End If
    End Sub

    Private Sub PreparaAggiunta()
        'In modalità aggiunta vengono preparate le textbox per l'inserimento dei dati...
        'Disabilitazione e abilitazione dei controlli necessari...
        TextBox1.Enabled = True
        TextBox1.Text = ""
        DateTimePicker1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button5.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
        Button11.Enabled = False
        Dim v = TextBox1.Focus()
    End Sub
    Private Sub RipristinaControlli()
        ' Ripristino dei controlli a necessità...
        TextBox1.Enabled = False
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = False
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button10.Enabled = True
        Button11.Enabled = True
        DateTimePicker1.Focus()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Connessione al database...
        Using connection As New SqlConnection(connectionString)
            ' Segnaposto per la posizione corrente...
            Dim intPosition As Integer
            ' Creazione del comando per l'inserimento dei dati...
            Dim cmdInsert As New SqlCommand()
            ' Salvataggio della posizione della scrittura corrente..
            intPosition = currencyManager.Position
            cmdInsert.Connection = connection
            cmdInsert.CommandText = "INSERT INTO [dbo].[Table] " &
                        "(Id, Data, Operazione, Descrizione, Importo) " &
                        "VALUES(@Id,@Data,@Operazione,@Descrizione,@Importo)"
            ' Aggiunta dei parametri...
            cmdInsert.Parameters.AddWithValue("@Id", TextBox1.Text).DbType =
                        DbType.Int32
            cmdInsert.Parameters.AddWithValue("@Data", DateTimePicker1.Text).DbType =
                        DbType.DateTime
            cmdInsert.Parameters.AddWithValue("@Operazione", TextBox2.Text).DbType =
                        DbType.String
            cmdInsert.Parameters.AddWithValue("@Descrizione", TextBox3.Text).DbType =
                        DbType.String
            cmdInsert.Parameters.AddWithValue("@Importo", TextBox4.Text).DbType =
                        DbType.Currency
            ' Apertura della connessione...
            connection.Open()
            Try
                ' Esecuzione del comando...
                cmdInsert.ExecuteNonQuery()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            ' Chiusura della connessione...
            connection.Close()
            FillDataSetAndView()
            BindFields()
            currencyManager.Position = intPosition
            ShowPosition()
            ToolStripStatusLabel1.Text = "Scrittura aggiunta al database."
            ' Ripristino dei controlli...
            RipristinaControlli()
        End Using
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Pulizia della ToolStripSatatusLabel1...
        ToolStripStatusLabel1.Text = ""
        ' Quesito relativo a richiesta di conferma o annullamento azione...
        ' Risposta positiva, disattivazione di quei comandi che ....
        If MsgBox("Le modifiche prevedono la pulizia delle caselle interessate. Proseguire con l'azione ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) =
        MsgBoxResult.Yes Then
            ' Modifica della scrittura...
            ModificaScrittura()
            ' Se la risposta è negativa, allora...
        ElseIf MsgBoxResult.No Then
            ToolStripStatusLabel1.Text = "Azione annullata."
            ShowPosition()
            RipristinaControlli()
        End If
    End Sub
    Private Sub ModificaScrittura()
        'connessione al database...
        Using connection As New SqlConnection(connectionString)
            ' Verifica presenza dati nel database e possibiltà di annullamento dell'azione...
            If currencyManager.Count - 1 < 0 Then
                currencyManager.Position = 0
                ToolStripStatusLabel1.Text = "Il database non contiene dati. Azione annullata."
                Exit Sub
            End If
            ToolStripStatusLabel1.Text = ""
            ' Segnaposto per la posizione corrente...
            Dim intPosition As Integer
            Try
                ' Creazione del comando per l'aggiornamento dei dati...
                Using cmdUpdate As New SqlCommand
                    ' Salvataggio della posizione del record corrente...
                    intPosition = currencyManager.Position
                    ' Impostazione delle proprietà dell'oggetto SqlCommand...
                    cmdUpdate.Connection = connection
                    cmdUpdate.CommandText = "UPDATE [dbo].[Table] " &
                        "SET Id = @Id, Data = @Data, Operazione = @Operazione, " &
                        "Descrizione = @Descrizione, Importo = @Importo WHERE Id = @Id"
                    cmdUpdate.CommandType = CommandType.Text
                    ' Aggiunta dei parametri per i segnaposto nella proprietà SQL CommandText...
                    cmdUpdate.Parameters.AddWithValue("@Id", TextBox1.Text).DbType _
                        = DbType.Int32
                    cmdUpdate.Parameters.AddWithValue("@Data", DateTimePicker1.Text).DbType _
                        = DbType.Date
                    cmdUpdate.Parameters.AddWithValue("@Operazione", TextBox2.Text).DbType _
                        = DbType.String
                    cmdUpdate.Parameters.AddWithValue("@Descrizione", TextBox3.Text).DbType _
                        = DbType.String
                    cmdUpdate.Parameters.AddWithValue("@Importo", TextBox4.Text).DbType _
                        = DbType.Currency
                    ' Apertura connessione...
                    connection.Open()
                    ' Esecuzione dell'oggetto SqlCommand per l'aggiornamento dei dati...
                    Dim v = cmdUpdate.ExecuteNonQuery()
                End Using
                ' Chiusura della connessione...
                connection.Close()
                ' Riempimento del DataSet e associazione dei campi...
                FillDataSetAndView()
                BindFields()
                ' Reimpostazione della posizione della scrittura su quella che è stata salvata all'inizio...
                currencyManager.Position = intPosition
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
        ' Visualizzazione della posizione della scrittura corrente...
        ShowPosition()
        ' Visualizzazione del messaggio dell'avvenuto aggiornamento...
        ToolStripStatusLabel1.Text = "Scrittura aggiornata."
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Creazione della connessione al database...
        Using connection As New SqlConnection(connectionString)
            ' Verifica presenza dati nel database e possibiltà di annullamento dell'azione già fin dall'inizio... 
            If currencyManager.Count - 1 < 0 Then
                currencyManager.Position = 0
                ToolStripStatusLabel1.Text = "Il database non contiene dati. Azione annullata."
                Exit Sub
            End If
            ToolStripStatusLabel1.Text = ""
            ' Quesito relativo richiesta conferma/annullamento eliminazione scrittura corrente...
            If MsgBox("Eliminare definitivamente la scrittura corrente ?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo) =
                    MsgBoxResult.Yes Then
                ' Se il quesito ha come esito risposta affermativa...
                ' Dichiarazione oggetti e variabili locali...
                ' Segnaposto per la posizione corrente...
                Dim intPosition As Integer
                Using cmdDelete As New SqlCommand
                    ' Salvataggio della posizione del record corrente a -1...
                    intPosition = BindingContext(dataView).Position - 1
                    ' Riposizionamento a seguito dell'eliminazione dell'ultimo record...
                    If intPosition < 0 Then
                        intPosition = 0
                        TextBox1.Text = ""
                        DateTimePicker1.Text = ""
                        TextBox2.Text = ""
                        TextBox3.Text = ""
                        TextBox4.Text = ""
                    End If
                    ' Impostazione delle proprietà dell'oggetto comando...
                    cmdDelete.Connection = connection
                    cmdDelete.CommandText = "DELETE FROM [dbo].[Table] " &
                    "WHERE Id = @Id"
                    ' Impostazione del parametro per il campo Id...
                    cmdDelete.Parameters.AddWithValue("@Id", CType(BindingContext(dataView).Current("Id"), Integer)).DbType = DbType.Int32
                    'Apertura della connessione...
                    connection.Open()
                    ' Esecuzione dell'oggetto SqlCommand per aggiornare i dati...
                    Dim unused = cmdDelete.ExecuteNonQuery()
                End Using
                ' Chiusura della connessione...
                connection.Close()
                ' Riempimento del DataSet e associazione dei campi...
                FillDataSetAndView()
                BindFields()
                ' Impostazione della posizione della scrittura su quella che è stata salvata all'inizio...
                BindingContext(dataView).Position = intPosition
                ' Visualizzazione della posizione della scrittura corrente...
                ShowPosition()
                ToolStripStatusLabel1.Text = "Scrittura eliminata."
                ' Se il quesito ha come esito risposta negativa...
            ElseIf CBool(MsgBoxResult.No) Then
                ' Visualizzazione del messaggio ddi azione interrotta...
                ToolStripStatusLabel1.Text = "Azione annullata."
            End If
        End Using
    End Sub
End Class