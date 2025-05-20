Imports System.Data.Odbc

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucProduk
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dgvProduk = New System.Windows.Forms.DataGridView()
        Me.cbTipe = New System.Windows.Forms.ComboBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        CType(Me.dgvProduk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(39, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Data Produk"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(-7, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 25)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "|"
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(858, 73)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 34)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "Tambah"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dgvProduk
        '
        Me.dgvProduk.BackgroundColor = System.Drawing.Color.White
        Me.dgvProduk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduk.Location = New System.Drawing.Point(51, 119)
        Me.dgvProduk.Name = "dgvProduk"
        Me.dgvProduk.Size = New System.Drawing.Size(881, 357)
        Me.dgvProduk.TabIndex = 5
        '
        'cbTipe
        '
        Me.cbTipe.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbTipe.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbTipe.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbTipe.FormattingEnabled = True
        Me.cbTipe.Location = New System.Drawing.Point(53, 82)
        Me.cbTipe.Name = "cbTipe"
        Me.cbTipe.Size = New System.Drawing.Size(111, 25)
        Me.cbTipe.TabIndex = 11
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.TR_POS_App.My.Resources.Resources.pattern
        Me.PictureBox7.Location = New System.Drawing.Point(953, 217)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(177, 184)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 13
        Me.PictureBox7.TabStop = False
        '
        'ucProduk
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.cbTipe)
        Me.Controls.Add(Me.dgvProduk)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ucProduk"
        Me.Size = New System.Drawing.Size(993, 535)
        CType(Me.dgvProduk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents dgvProduk As DataGridView
    Friend WithEvents cbTipe As ComboBox
    Friend WithEvents PictureBox7 As PictureBox
End Class
