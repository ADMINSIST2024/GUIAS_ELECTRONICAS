namespace TecnologiaTextil.FacturacionClient
{
    partial class FrmObservaciones
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmObservaciones));
            this.gunaElipse1 = new Guna.UI.WinForms.GunaElipse(this.components);
            this.gunaGradientPanel1 = new Guna.UI.WinForms.GunaGradientPanel();
            this.gunaLabel7 = new Guna.UI.WinForms.GunaLabel();
            this.gunaButton1 = new Guna.UI.WinForms.GunaButton();
            this.gunaLabel8 = new Guna.UI.WinForms.GunaLabel();
            this.txtticket = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel4 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel5 = new Guna.UI.WinForms.GunaLabel();
            this.txttip_doc = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtdoc = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtestado = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtcoderror = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtdescripcionerror = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaLabel6 = new Guna.UI.WinForms.GunaLabel();
            this.txtproceso = new Guna.UI.WinForms.GunaLineTextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnObtenerCDR = new Guna.UI.WinForms.GunaGradientButton();
            this.btnObtenerXML = new Guna.UI.WinForms.GunaGradientButton();
            this.btnObtenerPDF = new Guna.UI.WinForms.GunaGradientButton();
            this.btnReintentar = new Guna.UI.WinForms.GunaGradientButton();
            this.gunaGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gunaElipse1
            // 
            this.gunaElipse1.TargetControl = this;
            // 
            // gunaGradientPanel1
            // 
            this.gunaGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel1.BackgroundImage")));
            this.gunaGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel1.Controls.Add(this.gunaLabel7);
            this.gunaGradientPanel1.Controls.Add(this.gunaButton1);
            this.gunaGradientPanel1.Controls.Add(this.gunaLabel8);
            this.gunaGradientPanel1.Controls.Add(this.txtticket);
            this.gunaGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gunaGradientPanel1.GradientColor1 = System.Drawing.Color.LightSeaGreen;
            this.gunaGradientPanel1.GradientColor2 = System.Drawing.Color.DarkTurquoise;
            this.gunaGradientPanel1.GradientColor3 = System.Drawing.Color.LightSeaGreen;
            this.gunaGradientPanel1.GradientColor4 = System.Drawing.Color.White;
            this.gunaGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gunaGradientPanel1.Name = "gunaGradientPanel1";
            this.gunaGradientPanel1.Size = new System.Drawing.Size(810, 56);
            this.gunaGradientPanel1.TabIndex = 0;
            this.gunaGradientPanel1.Text = "gunaGradientPanel1";
            // 
            // gunaLabel7
            // 
            this.gunaLabel7.AutoSize = true;
            this.gunaLabel7.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gunaLabel7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel7.ForeColor = System.Drawing.Color.White;
            this.gunaLabel7.Location = new System.Drawing.Point(23, 21);
            this.gunaLabel7.Name = "gunaLabel7";
            this.gunaLabel7.Size = new System.Drawing.Size(106, 21);
            this.gunaLabel7.TabIndex = 13;
            this.gunaLabel7.Text = "Observacion";
            // 
            // gunaButton1
            // 
            this.gunaButton1.AnimationHoverSpeed = 0.07F;
            this.gunaButton1.AnimationSpeed = 0.03F;
            this.gunaButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton1.BaseColor = System.Drawing.Color.Orange;
            this.gunaButton1.BorderColor = System.Drawing.Color.Black;
            this.gunaButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton1.ForeColor = System.Drawing.Color.White;
            this.gunaButton1.Image = null;
            this.gunaButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton1.Location = new System.Drawing.Point(754, 12);
            this.gunaButton1.Name = "gunaButton1";
            this.gunaButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton1.OnHoverImage = null;
            this.gunaButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton1.Radius = 15;
            this.gunaButton1.Size = new System.Drawing.Size(41, 33);
            this.gunaButton1.TabIndex = 1;
            this.gunaButton1.Text = "X";
            this.gunaButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton1.Click += new System.EventHandler(this.gunaButton1_Click);
            // 
            // gunaLabel8
            // 
            this.gunaLabel8.AutoSize = true;
            this.gunaLabel8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel8.Location = new System.Drawing.Point(139, 21);
            this.gunaLabel8.Name = "gunaLabel8";
            this.gunaLabel8.Size = new System.Drawing.Size(77, 20);
            this.gunaLabel8.TabIndex = 13;
            this.gunaLabel8.Text = "Nro Ticket";
            this.gunaLabel8.Visible = false;
            // 
            // txtticket
            // 
            this.txtticket.BackColor = System.Drawing.Color.White;
            this.txtticket.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtticket.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtticket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtticket.LineColor = System.Drawing.Color.Gainsboro;
            this.txtticket.Location = new System.Drawing.Point(236, 19);
            this.txtticket.Name = "txtticket";
            this.txtticket.PasswordChar = '\0';
            this.txtticket.SelectedText = "";
            this.txtticket.Size = new System.Drawing.Size(403, 26);
            this.txtticket.TabIndex = 14;
            this.txtticket.Visible = false;
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.Location = new System.Drawing.Point(36, 86);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(121, 20);
            this.gunaLabel1.TabIndex = 1;
            this.gunaLabel1.Text = "Tipo Documento";
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.Location = new System.Drawing.Point(211, 86);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(116, 20);
            this.gunaLabel2.TabIndex = 2;
            this.gunaLabel2.Text = "Nro Documento";
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.AutoSize = true;
            this.gunaLabel3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.Location = new System.Drawing.Point(482, 86);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(54, 20);
            this.gunaLabel3.TabIndex = 3;
            this.gunaLabel3.Text = "Estado";
            // 
            // gunaLabel4
            // 
            this.gunaLabel4.AutoSize = true;
            this.gunaLabel4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel4.Location = new System.Drawing.Point(36, 186);
            this.gunaLabel4.Name = "gunaLabel4";
            this.gunaLabel4.Size = new System.Drawing.Size(94, 20);
            this.gunaLabel4.TabIndex = 4;
            this.gunaLabel4.Text = "Codigo Error";
            // 
            // gunaLabel5
            // 
            this.gunaLabel5.AutoSize = true;
            this.gunaLabel5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel5.Location = new System.Drawing.Point(211, 186);
            this.gunaLabel5.Name = "gunaLabel5";
            this.gunaLabel5.Size = new System.Drawing.Size(123, 20);
            this.gunaLabel5.TabIndex = 5;
            this.gunaLabel5.Text = "Descripcion Error";
            // 
            // txttip_doc
            // 
            this.txttip_doc.BackColor = System.Drawing.Color.White;
            this.txttip_doc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttip_doc.Enabled = false;
            this.txttip_doc.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txttip_doc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txttip_doc.LineColor = System.Drawing.Color.Gainsboro;
            this.txttip_doc.Location = new System.Drawing.Point(40, 118);
            this.txttip_doc.Name = "txttip_doc";
            this.txttip_doc.PasswordChar = '\0';
            this.txttip_doc.SelectedText = "";
            this.txttip_doc.Size = new System.Drawing.Size(117, 26);
            this.txttip_doc.TabIndex = 6;
            // 
            // txtdoc
            // 
            this.txtdoc.BackColor = System.Drawing.Color.White;
            this.txtdoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdoc.Enabled = false;
            this.txtdoc.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtdoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtdoc.LineColor = System.Drawing.Color.Gainsboro;
            this.txtdoc.Location = new System.Drawing.Point(215, 118);
            this.txtdoc.Name = "txtdoc";
            this.txtdoc.PasswordChar = '\0';
            this.txtdoc.SelectedText = "";
            this.txtdoc.Size = new System.Drawing.Size(232, 26);
            this.txtdoc.TabIndex = 7;
            // 
            // txtestado
            // 
            this.txtestado.BackColor = System.Drawing.Color.White;
            this.txtestado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtestado.Enabled = false;
            this.txtestado.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtestado.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtestado.LineColor = System.Drawing.Color.Gainsboro;
            this.txtestado.Location = new System.Drawing.Point(486, 118);
            this.txtestado.Name = "txtestado";
            this.txtestado.PasswordChar = '\0';
            this.txtestado.SelectedText = "";
            this.txtestado.Size = new System.Drawing.Size(117, 26);
            this.txtestado.TabIndex = 8;
            // 
            // txtcoderror
            // 
            this.txtcoderror.BackColor = System.Drawing.Color.White;
            this.txtcoderror.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtcoderror.Enabled = false;
            this.txtcoderror.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtcoderror.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtcoderror.LineColor = System.Drawing.Color.Gainsboro;
            this.txtcoderror.Location = new System.Drawing.Point(40, 221);
            this.txtcoderror.Name = "txtcoderror";
            this.txtcoderror.PasswordChar = '\0';
            this.txtcoderror.SelectedText = "";
            this.txtcoderror.Size = new System.Drawing.Size(117, 26);
            this.txtcoderror.TabIndex = 9;
            // 
            // txtdescripcionerror
            // 
            this.txtdescripcionerror.BackColor = System.Drawing.Color.White;
            this.txtdescripcionerror.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdescripcionerror.Enabled = false;
            this.txtdescripcionerror.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtdescripcionerror.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtdescripcionerror.LineColor = System.Drawing.Color.Gainsboro;
            this.txtdescripcionerror.Location = new System.Drawing.Point(215, 221);
            this.txtdescripcionerror.Name = "txtdescripcionerror";
            this.txtdescripcionerror.PasswordChar = '\0';
            this.txtdescripcionerror.SelectedText = "";
            this.txtdescripcionerror.Size = new System.Drawing.Size(563, 75);
            this.txtdescripcionerror.TabIndex = 10;
            // 
            // gunaLabel6
            // 
            this.gunaLabel6.AutoSize = true;
            this.gunaLabel6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel6.Location = new System.Drawing.Point(657, 86);
            this.gunaLabel6.Name = "gunaLabel6";
            this.gunaLabel6.Size = new System.Drawing.Size(61, 20);
            this.gunaLabel6.TabIndex = 11;
            this.gunaLabel6.Text = "Proceso";
            // 
            // txtproceso
            // 
            this.txtproceso.BackColor = System.Drawing.Color.White;
            this.txtproceso.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtproceso.Enabled = false;
            this.txtproceso.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtproceso.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtproceso.LineColor = System.Drawing.Color.Gainsboro;
            this.txtproceso.Location = new System.Drawing.Point(661, 118);
            this.txtproceso.Name = "txtproceso";
            this.txtproceso.PasswordChar = '\0';
            this.txtproceso.SelectedText = "";
            this.txtproceso.Size = new System.Drawing.Size(117, 26);
            this.txtproceso.TabIndex = 12;
            // 
            // btnObtenerCDR
            // 
            this.btnObtenerCDR.Animated = true;
            this.btnObtenerCDR.AnimationHoverSpeed = 0.07F;
            this.btnObtenerCDR.AnimationSpeed = 0.03F;
            this.btnObtenerCDR.BackColor = System.Drawing.Color.Transparent;
            this.btnObtenerCDR.BaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerCDR.BaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerCDR.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerCDR.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnObtenerCDR.FocusedColor = System.Drawing.Color.Empty;
            this.btnObtenerCDR.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnObtenerCDR.ForeColor = System.Drawing.Color.White;
            this.btnObtenerCDR.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerCDR.Image")));
            this.btnObtenerCDR.ImageSize = new System.Drawing.Size(30, 30);
            this.btnObtenerCDR.Location = new System.Drawing.Point(236, 327);
            this.btnObtenerCDR.Name = "btnObtenerCDR";
            this.btnObtenerCDR.OnHoverBaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerCDR.OnHoverBaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerCDR.OnHoverBorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerCDR.OnHoverForeColor = System.Drawing.Color.White;
            this.btnObtenerCDR.OnHoverImage = null;
            this.btnObtenerCDR.OnPressedColor = System.Drawing.Color.Black;
            this.btnObtenerCDR.Radius = 10;
            this.btnObtenerCDR.Size = new System.Drawing.Size(144, 42);
            this.btnObtenerCDR.TabIndex = 15;
            this.btnObtenerCDR.Text = "Obtener CDR";
            this.btnObtenerCDR.Visible = false;
            this.btnObtenerCDR.Click += new System.EventHandler(this.btnObtenerCDR_Click);
            // 
            // btnObtenerXML
            // 
            this.btnObtenerXML.Animated = true;
            this.btnObtenerXML.AnimationHoverSpeed = 0.07F;
            this.btnObtenerXML.AnimationSpeed = 0.03F;
            this.btnObtenerXML.BackColor = System.Drawing.Color.Transparent;
            this.btnObtenerXML.BaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerXML.BaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerXML.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerXML.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnObtenerXML.FocusedColor = System.Drawing.Color.Empty;
            this.btnObtenerXML.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnObtenerXML.ForeColor = System.Drawing.Color.White;
            this.btnObtenerXML.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerXML.Image")));
            this.btnObtenerXML.ImageSize = new System.Drawing.Size(30, 30);
            this.btnObtenerXML.Location = new System.Drawing.Point(386, 327);
            this.btnObtenerXML.Name = "btnObtenerXML";
            this.btnObtenerXML.OnHoverBaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerXML.OnHoverBaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerXML.OnHoverBorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerXML.OnHoverForeColor = System.Drawing.Color.White;
            this.btnObtenerXML.OnHoverImage = null;
            this.btnObtenerXML.OnPressedColor = System.Drawing.Color.Black;
            this.btnObtenerXML.Radius = 10;
            this.btnObtenerXML.Size = new System.Drawing.Size(144, 42);
            this.btnObtenerXML.TabIndex = 16;
            this.btnObtenerXML.Text = "Obtener XML";
            this.btnObtenerXML.Visible = false;
            this.btnObtenerXML.Click += new System.EventHandler(this.btnObtenerXML_Click);
            // 
            // btnObtenerPDF
            // 
            this.btnObtenerPDF.Animated = true;
            this.btnObtenerPDF.AnimationHoverSpeed = 0.07F;
            this.btnObtenerPDF.AnimationSpeed = 0.03F;
            this.btnObtenerPDF.BackColor = System.Drawing.Color.Transparent;
            this.btnObtenerPDF.BaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerPDF.BaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerPDF.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerPDF.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnObtenerPDF.FocusedColor = System.Drawing.Color.Empty;
            this.btnObtenerPDF.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnObtenerPDF.ForeColor = System.Drawing.Color.White;
            this.btnObtenerPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnObtenerPDF.Image")));
            this.btnObtenerPDF.ImageSize = new System.Drawing.Size(30, 30);
            this.btnObtenerPDF.Location = new System.Drawing.Point(536, 327);
            this.btnObtenerPDF.Name = "btnObtenerPDF";
            this.btnObtenerPDF.OnHoverBaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerPDF.OnHoverBaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnObtenerPDF.OnHoverBorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnObtenerPDF.OnHoverForeColor = System.Drawing.Color.White;
            this.btnObtenerPDF.OnHoverImage = null;
            this.btnObtenerPDF.OnPressedColor = System.Drawing.Color.Black;
            this.btnObtenerPDF.Radius = 10;
            this.btnObtenerPDF.Size = new System.Drawing.Size(144, 42);
            this.btnObtenerPDF.TabIndex = 17;
            this.btnObtenerPDF.Text = "Obtener PDF";
            this.btnObtenerPDF.Visible = false;
            this.btnObtenerPDF.Click += new System.EventHandler(this.btnObtenerPDF_Click);
            // 
            // btnReintentar
            // 
            this.btnReintentar.Animated = true;
            this.btnReintentar.AnimationHoverSpeed = 0.07F;
            this.btnReintentar.AnimationSpeed = 0.03F;
            this.btnReintentar.BackColor = System.Drawing.Color.Transparent;
            this.btnReintentar.BaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.BaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnReintentar.BorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReintentar.FocusedColor = System.Drawing.Color.Empty;
            this.btnReintentar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReintentar.ForeColor = System.Drawing.Color.White;
            this.btnReintentar.Image = ((System.Drawing.Image)(resources.GetObject("btnReintentar.Image")));
            this.btnReintentar.ImageSize = new System.Drawing.Size(30, 30);
            this.btnReintentar.Location = new System.Drawing.Point(40, 327);
            this.btnReintentar.Name = "btnReintentar";
            this.btnReintentar.OnHoverBaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.OnHoverBaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnReintentar.OnHoverBorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.OnHoverForeColor = System.Drawing.Color.White;
            this.btnReintentar.OnHoverImage = null;
            this.btnReintentar.OnPressedColor = System.Drawing.Color.Black;
            this.btnReintentar.Radius = 10;
            this.btnReintentar.Size = new System.Drawing.Size(144, 42);
            this.btnReintentar.TabIndex = 18;
            this.btnReintentar.Text = "Reintentar";
            this.btnReintentar.Click += new System.EventHandler(this.btnReintentar_Click);
            // 
            // FrmObservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 396);
            this.Controls.Add(this.btnReintentar);
            this.Controls.Add(this.btnObtenerPDF);
            this.Controls.Add(this.btnObtenerXML);
            this.Controls.Add(this.btnObtenerCDR);
            this.Controls.Add(this.txtproceso);
            this.Controls.Add(this.gunaLabel6);
            this.Controls.Add(this.txtdescripcionerror);
            this.Controls.Add(this.txtcoderror);
            this.Controls.Add(this.txtestado);
            this.Controls.Add(this.txtdoc);
            this.Controls.Add(this.txttip_doc);
            this.Controls.Add(this.gunaLabel5);
            this.Controls.Add(this.gunaLabel4);
            this.Controls.Add(this.gunaLabel3);
            this.Controls.Add(this.gunaLabel2);
            this.Controls.Add(this.gunaLabel1);
            this.Controls.Add(this.gunaGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmObservaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmObservaciones";
            this.Load += new System.EventHandler(this.FrmObservaciones_Load);
            this.gunaGradientPanel1.ResumeLayout(false);
            this.gunaGradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private Guna.UI.WinForms.GunaElipse gunaElipse1;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel1;
        private Guna.UI.WinForms.GunaButton gunaButton1;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaLabel gunaLabel5;
        private Guna.UI.WinForms.GunaLabel gunaLabel4;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaLineTextBox txtdescripcionerror;
        private Guna.UI.WinForms.GunaLineTextBox txtcoderror;
        private Guna.UI.WinForms.GunaLineTextBox txtestado;
        private Guna.UI.WinForms.GunaLineTextBox txtdoc;
        private Guna.UI.WinForms.GunaLineTextBox txttip_doc;
        private Guna.UI.WinForms.GunaLineTextBox txtproceso;
        private Guna.UI.WinForms.GunaLabel gunaLabel6;
        private Guna.UI.WinForms.GunaLabel gunaLabel7;
        private Guna.UI.WinForms.GunaLabel gunaLabel8;
        private Guna.UI.WinForms.GunaLineTextBox txtticket;
        private Guna.UI.WinForms.GunaGradientButton btnObtenerCDR;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Guna.UI.WinForms.GunaGradientButton btnObtenerPDF;
        private Guna.UI.WinForms.GunaGradientButton btnObtenerXML;
        private Guna.UI.WinForms.GunaGradientButton btnReintentar;
    }
}