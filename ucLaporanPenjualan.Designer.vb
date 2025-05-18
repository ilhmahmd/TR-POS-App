<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucLaporanPenjualan
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
        Me.dgvLaporan = New System.Windows.Forms.DataGridView()
        Me.cbFilterMetodeBayar = New System.Windows.Forms.ComboBox()
        Me.cbFilterKasir = New System.Windows.Forms.ComboBox()
        Me.cbUrutkanTransaksi = New System.Windows.Forms.ComboBox()
        Me.dtpTanggalAwal = New System.Windows.Forms.DateTimePicker()
        Me.dtpTanggalAkhir = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnFilterTanggal = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTotalTransaksi = New System.Windows.Forms.Label()
        Me.lblTotalPenjualan = New System.Windows.Forms.Label()
        Me.btnExportCetak = New System.Windows.Forms.Button()
        Me.btnExportExcel = New System.Windows.Forms.Button()
        CType(Me.dgvLaporan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label2.Location = New System.Drawing.Point(-7, 143)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(18, 25)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "|"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(38, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(178, 25)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Laporan Penjualan"
        '
        'dgvLaporan
        '
        Me.dgvLaporan.BackgroundColor = System.Drawing.Color.White
        Me.dgvLaporan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLaporan.Location = New System.Drawing.Point(43, 124)
        Me.dgvLaporan.Name = "dgvLaporan"
        Me.dgvLaporan.Size = New System.Drawing.Size(900, 324)
        Me.dgvLaporan.TabIndex = 6
        '
        'cbFilterMetodeBayar
        '
        Me.cbFilterMetodeBayar.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbFilterMetodeBayar.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbFilterMetodeBayar.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterMetodeBayar.FormattingEnabled = True
        Me.cbFilterMetodeBayar.Location = New System.Drawing.Point(43, 85)
        Me.cbFilterMetodeBayar.Name = "cbFilterMetodeBayar"
        Me.cbFilterMetodeBayar.Size = New System.Drawing.Size(111, 25)
        Me.cbFilterMetodeBayar.TabIndex = 7
        '
        'cbFilterKasir
        '
        Me.cbFilterKasir.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbFilterKasir.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbFilterKasir.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbFilterKasir.FormattingEnabled = True
        Me.cbFilterKasir.Location = New System.Drawing.Point(170, 85)
        Me.cbFilterKasir.Name = "cbFilterKasir"
        Me.cbFilterKasir.Size = New System.Drawing.Size(111, 25)
        Me.cbFilterKasir.TabIndex = 8
        '
        'cbUrutkanTransaksi
        '
        Me.cbUrutkanTransaksi.BackColor = System.Drawing.Color.WhiteSmoke
        Me.cbUrutkanTransaksi.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.cbUrutkanTransaksi.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbUrutkanTransaksi.FormattingEnabled = True
        Me.cbUrutkanTransaksi.Location = New System.Drawing.Point(296, 85)
        Me.cbUrutkanTransaksi.Name = "cbUrutkanTransaksi"
        Me.cbUrutkanTransaksi.Size = New System.Drawing.Size(111, 25)
        Me.cbUrutkanTransaksi.TabIndex = 9
        '
        'dtpTanggalAwal
        '
        Me.dtpTanggalAwal.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTanggalAwal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalAwal.Location = New System.Drawing.Point(674, 84)
        Me.dtpTanggalAwal.Name = "dtpTanggalAwal"
        Me.dtpTanggalAwal.Size = New System.Drawing.Size(96, 23)
        Me.dtpTanggalAwal.TabIndex = 11
        '
        'dtpTanggalAkhir
        '
        Me.dtpTanggalAkhir.Font = New System.Drawing.Font("Segoe UI Semibold", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTanggalAkhir.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTanggalAkhir.Location = New System.Drawing.Point(793, 84)
        Me.dtpTanggalAkhir.Name = "dtpTanggalAkhir"
        Me.dtpTanggalAkhir.Size = New System.Drawing.Size(96, 23)
        Me.dtpTanggalAkhir.TabIndex = 12
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(775, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 21)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "-"
        '
        'btnFilterTanggal
        '
        Me.btnFilterTanggal.BackColor = System.Drawing.Color.Red
        Me.btnFilterTanggal.FlatAppearance.BorderSize = 0
        Me.btnFilterTanggal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFilterTanggal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFilterTanggal.ForeColor = System.Drawing.Color.White
        Me.btnFilterTanggal.Location = New System.Drawing.Point(898, 83)
        Me.btnFilterTanggal.Name = "btnFilterTanggal"
        Me.btnFilterTanggal.Size = New System.Drawing.Size(45, 24)
        Me.btnFilterTanggal.TabIndex = 14
        Me.btnFilterTanggal.Text = "Filter"
        Me.btnFilterTanggal.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(39, 461)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(116, 20)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "Total Transaksi :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(272, 461)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(121, 20)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Total Penjualan :"
        '
        'lblTotalTransaksi
        '
        Me.lblTotalTransaksi.AutoSize = True
        Me.lblTotalTransaksi.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTransaksi.Location = New System.Drawing.Point(161, 461)
        Me.lblTotalTransaksi.Name = "lblTotalTransaksi"
        Me.lblTotalTransaksi.Size = New System.Drawing.Size(196, 32)
        Me.lblTotalTransaksi.TabIndex = 17
        Me.lblTotalTransaksi.Text = "Total Transaksi :"
        '
        'lblTotalPenjualan
        '
        Me.lblTotalPenjualan.AutoSize = True
        Me.lblTotalPenjualan.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPenjualan.Location = New System.Drawing.Point(391, 461)
        Me.lblTotalPenjualan.Name = "lblTotalPenjualan"
        Me.lblTotalPenjualan.Size = New System.Drawing.Size(196, 32)
        Me.lblTotalPenjualan.TabIndex = 18
        Me.lblTotalPenjualan.Text = "Total Transaksi :"
        '
        'btnExportCetak
        '
        Me.btnExportCetak.BackColor = System.Drawing.Color.Red
        Me.btnExportCetak.FlatAppearance.BorderSize = 0
        Me.btnExportCetak.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportCetak.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportCetak.ForeColor = System.Drawing.Color.White
        Me.btnExportCetak.Location = New System.Drawing.Point(879, 461)
        Me.btnExportCetak.Name = "btnExportCetak"
        Me.btnExportCetak.Size = New System.Drawing.Size(64, 32)
        Me.btnExportCetak.TabIndex = 19
        Me.btnExportCetak.Text = "Print"
        Me.btnExportCetak.UseVisualStyleBackColor = False
        '
        'btnExportExcel
        '
        Me.btnExportExcel.BackColor = System.Drawing.Color.Green
        Me.btnExportExcel.FlatAppearance.BorderSize = 0
        Me.btnExportExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExportExcel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportExcel.ForeColor = System.Drawing.Color.White
        Me.btnExportExcel.Location = New System.Drawing.Point(801, 461)
        Me.btnExportExcel.Name = "btnExportExcel"
        Me.btnExportExcel.Size = New System.Drawing.Size(64, 32)
        Me.btnExportExcel.TabIndex = 20
        Me.btnExportExcel.Text = "Export "
        Me.btnExportExcel.UseVisualStyleBackColor = False
        '
        'ucLaporanPenjualan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.btnExportExcel)
        Me.Controls.Add(Me.btnExportCetak)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblTotalPenjualan)
        Me.Controls.Add(Me.lblTotalTransaksi)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnFilterTanggal)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.dtpTanggalAkhir)
        Me.Controls.Add(Me.dtpTanggalAwal)
        Me.Controls.Add(Me.cbUrutkanTransaksi)
        Me.Controls.Add(Me.cbFilterKasir)
        Me.Controls.Add(Me.cbFilterMetodeBayar)
        Me.Controls.Add(Me.dgvLaporan)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ucLaporanPenjualan"
        Me.Size = New System.Drawing.Size(993, 535)
        CType(Me.dgvLaporan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvLaporan As DataGridView
    Friend WithEvents cbFilterMetodeBayar As ComboBox
    Friend WithEvents cbFilterKasir As ComboBox
    Friend WithEvents cbUrutkanTransaksi As ComboBox
    Friend WithEvents dtpTanggalAwal As DateTimePicker
    Friend WithEvents dtpTanggalAkhir As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents btnFilterTanggal As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblTotalTransaksi As Label
    Friend WithEvents lblTotalPenjualan As Label
    Friend WithEvents btnExportCetak As Button
    Friend WithEvents btnExportExcel As Button
End Class
