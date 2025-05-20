<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucTrans
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvProduk = New System.Windows.Forms.DataGridView()
        Me.dgvKeranjang = New System.Windows.Forms.DataGridView()
        Me.lblTotalHarga = New System.Windows.Forms.Label()
        Me.btnBayar = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnTunai = New System.Windows.Forms.Button()
        Me.btnQRIS = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        CType(Me.dgvProduk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvKeranjang, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(-7, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 25)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "|"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 25)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Transaksi"
        '
        'dgvProduk
        '
        Me.dgvProduk.BackgroundColor = System.Drawing.Color.White
        Me.dgvProduk.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvProduk.Location = New System.Drawing.Point(48, 88)
        Me.dgvProduk.Name = "dgvProduk"
        Me.dgvProduk.Size = New System.Drawing.Size(541, 390)
        Me.dgvProduk.TabIndex = 6
        '
        'dgvKeranjang
        '
        Me.dgvKeranjang.BackgroundColor = System.Drawing.Color.White
        Me.dgvKeranjang.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvKeranjang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvKeranjang.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.dgvKeranjang.Location = New System.Drawing.Point(606, 123)
        Me.dgvKeranjang.Name = "dgvKeranjang"
        Me.dgvKeranjang.Size = New System.Drawing.Size(333, 222)
        Me.dgvKeranjang.TabIndex = 7
        '
        'lblTotalHarga
        '
        Me.lblTotalHarga.AutoSize = True
        Me.lblTotalHarga.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalHarga.Location = New System.Drawing.Point(614, 445)
        Me.lblTotalHarga.Name = "lblTotalHarga"
        Me.lblTotalHarga.Size = New System.Drawing.Size(68, 25)
        Me.lblTotalHarga.TabIndex = 8
        Me.lblTotalHarga.Text = "Label3"
        '
        'btnBayar
        '
        Me.btnBayar.BackgroundImage = Global.TR_POS_App.My.Resources.Resources.butlog
        Me.btnBayar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBayar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBayar.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBayar.ForeColor = System.Drawing.Color.White
        Me.btnBayar.Location = New System.Drawing.Point(840, 434)
        Me.btnBayar.Name = "btnBayar"
        Me.btnBayar.Size = New System.Drawing.Size(99, 44)
        Me.btnBayar.TabIndex = 9
        Me.btnBayar.Text = "Bayar"
        Me.btnBayar.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(603, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 17)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Keranjang"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(604, 358)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(135, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Metode Pembayaran"
        '
        'btnTunai
        '
        Me.btnTunai.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTunai.Location = New System.Drawing.Point(606, 387)
        Me.btnTunai.Name = "btnTunai"
        Me.btnTunai.Size = New System.Drawing.Size(104, 34)
        Me.btnTunai.TabIndex = 13
        Me.btnTunai.Text = "Tunai"
        Me.btnTunai.UseVisualStyleBackColor = True
        '
        'btnQRIS
        '
        Me.btnQRIS.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQRIS.Location = New System.Drawing.Point(720, 387)
        Me.btnQRIS.Name = "btnQRIS"
        Me.btnQRIS.Size = New System.Drawing.Size(104, 34)
        Me.btnQRIS.TabIndex = 14
        Me.btnQRIS.Text = "QRIS"
        Me.btnQRIS.UseVisualStyleBackColor = True
        '
        'btnTransfer
        '
        Me.btnTransfer.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTransfer.Location = New System.Drawing.Point(835, 387)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(104, 34)
        Me.btnTransfer.TabIndex = 15
        Me.btnTransfer.Text = "Transfer"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.TR_POS_App.My.Resources.Resources.pattern
        Me.PictureBox7.Location = New System.Drawing.Point(762, -89)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(177, 184)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 16
        Me.PictureBox7.TabStop = False
        '
        'ucTrans
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PictureBox7)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.btnQRIS)
        Me.Controls.Add(Me.btnTunai)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnBayar)
        Me.Controls.Add(Me.lblTotalHarga)
        Me.Controls.Add(Me.dgvKeranjang)
        Me.Controls.Add(Me.dgvProduk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ucTrans"
        Me.Size = New System.Drawing.Size(993, 535)
        CType(Me.dgvProduk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvKeranjang, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvProduk As DataGridView
    Friend WithEvents dgvKeranjang As DataGridView
    Friend WithEvents lblTotalHarga As Label
    Friend WithEvents btnBayar As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnTunai As Button
    Friend WithEvents btnQRIS As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents PictureBox7 As PictureBox
End Class
