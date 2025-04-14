<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMenu
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMenu))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GestioneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ScrittureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntrateToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsciteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnteprimaStampaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EsciToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InformazioniToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.InformazioniToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GestioneToolStripMenuItem, Me.ScrittureToolStripMenuItem, Me.EntrateToolStripMenuItem, Me.UsciteToolStripMenuItem, Me.AnteprimaStampaToolStripMenuItem, Me.EsciToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'GestioneToolStripMenuItem
        '
        Me.GestioneToolStripMenuItem.Name = "GestioneToolStripMenuItem"
        Me.GestioneToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.GestioneToolStripMenuItem.Text = "Gestione"
        '
        'ScrittureToolStripMenuItem
        '
        Me.ScrittureToolStripMenuItem.Name = "ScrittureToolStripMenuItem"
        Me.ScrittureToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ScrittureToolStripMenuItem.Text = "Scritture"
        '
        'EntrateToolStripMenuItem
        '
        Me.EntrateToolStripMenuItem.Name = "EntrateToolStripMenuItem"
        Me.EntrateToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.EntrateToolStripMenuItem.Text = "Entrate"
        '
        'UsciteToolStripMenuItem
        '
        Me.UsciteToolStripMenuItem.Name = "UsciteToolStripMenuItem"
        Me.UsciteToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.UsciteToolStripMenuItem.Text = "Uscite"
        '
        'AnteprimaStampaToolStripMenuItem
        '
        Me.AnteprimaStampaToolStripMenuItem.Name = "AnteprimaStampaToolStripMenuItem"
        Me.AnteprimaStampaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.AnteprimaStampaToolStripMenuItem.Text = "Anteprima/Stampa"
        '
        'EsciToolStripMenuItem
        '
        Me.EsciToolStripMenuItem.Name = "EsciToolStripMenuItem"
        Me.EsciToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.EsciToolStripMenuItem.Text = "Esci"
        '
        'InformazioniToolStripMenuItem
        '
        Me.InformazioniToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2})
        Me.InformazioniToolStripMenuItem.Name = "InformazioniToolStripMenuItem"
        Me.InformazioniToolStripMenuItem.Size = New System.Drawing.Size(86, 20)
        Me.InformazioniToolStripMenuItem.Text = "Informazioni"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(180, 22)
        Me.ToolStripMenuItem2.Text = "?"
        '
        'FormMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FormMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GestioneToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ScrittureToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EntrateToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents UsciteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnteprimaStampaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EsciToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents InformazioniToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As ToolStripMenuItem
End Class
