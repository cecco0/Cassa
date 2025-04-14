
Public Class FormMenu
    ' This form is the main menu of the application...
    ' It contains a status strip at the bottom and a menu strip at the top...
    Private statusStrip1 As StatusStrip
    Private toolStripStatusLabel1 As ToolStripStatusLabel
    Private toolStripStatusLabel2 As ToolStripStatusLabel

    Public Sub New()
        ' This call is required by the designer...
        InitializeComponent()
    End Sub

    <STAThread()>
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.Run(New Form1())
    End Sub

    Private Sub InitializeComponent(green As Color)
        ' This method initializes the components of the form...
        statusStrip1 = New StatusStrip()
        toolStripStatusLabel1 = New ToolStripStatusLabel()
        toolStripStatusLabel2 = New ToolStripStatusLabel()
        statusStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' The following code example demonstrates the syntax for setting
        ' various StatusStrip properties.
        statusStrip1.Dock = DockStyle.Bottom
        statusStrip1.GripStyle = ToolStripGripStyle.Visible
        statusStrip1.Items.AddRange(New ToolStripItem() {toolStripStatusLabel1})
        statusStrip1.Items.AddRange(New ToolStripItem() {toolStripStatusLabel2})
        statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
        statusStrip1.Location = New Point(0, 0)
        statusStrip1.Name = "statusStrip1"
        statusStrip1.ShowItemToolTips = True
        statusStrip1.Size = New Size(1092, 22)
        statusStrip1.SizingGrip = False
        statusStrip1.Stretch = False
        statusStrip1.TabIndex = 0
        'Dim unused As New Color
        statusStrip1.BackColor = green
        statusStrip1.Text = "statusStrip1"
        ' 
        ' toolStripStatusLabel1 and toolStripProgressBar
        '
        With toolStripStatusLabel1
            .Name = "toolStripStatusLabel1"
            .Size = New Size(0, 0)
            .Text = CStr(Today)
        End With
        With toolStripStatusLabel2
            .Name = "toolStripStatusLabel2"
            .Size = New Size(0, 0)
            .Text = CStr(TimeOfDay)
        End With
        ' 
        ' Form1
        ' 
        ClientSize = New Size(1106, 624)
        Controls.Add(statusStrip1)
        Name = "Menù"
        statusStrip1.ResumeLayout(False)
        statusStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeComponent(Color.PowderBlue)
    End Sub

    Private Sub GestioneToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GestioneToolStripMenuItem.Click
        ' This method is called when the "Gestione" menu item is clicked...
        Dim DialogResult = FormGestione.ShowDialog
    End Sub

    Private Sub ScrittureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ScrittureToolStripMenuItem.Click
        ' This method is called when the "Scritture" menu item is clicked...
        Dim DialogResult1 = FormScritture.ShowDialog
    End Sub

    Private Sub EntrateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EntrateToolStripMenuItem.Click
        ' This method is called when the "Entrate" menu item is clicked...
        Dim DialogResult2 = FormEntrate.ShowDialog
    End Sub

    Private Sub UsciteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsciteToolStripMenuItem.Click
        ' This method is called when the "Uscite" menu item is clicked...
        Dim DialogResult3 = FormUscite.ShowDialog
    End Sub

    Private Sub AnteprimaStampaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AnteprimaStampaToolStripMenuItem.Click
        ' This method is called when the "Anteprima Stampa" menu item is clicked...
        FormAnteprima.Show()
    End Sub

    Private Sub EsciToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EsciToolStripMenuItem.Click
        ' This method is called when the "Esci" menu item is clicked...
        If MsgBox("Chiudere l'applicazione ?", MsgBoxStyle.YesNo) =
                        MsgBoxResult.Yes Then
            Close()
            End
        ElseIf MsgBoxResult.No Then
            Dim msgBoxResult = MsgBox("Azione annullata.", MsgBoxStyle.Information)
        End If
    End Sub
    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        ' This method is called when the "ToolStripMenuItem2" menu item is clicked...
        AboutBoxCassa.ShowDialog()
    End Sub
    Private Sub FormMenu_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        End
    End Sub

End Class