namespace TecnologiaTextil.FacturacionClient
{
    partial class FrmEnvio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEnvio));
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.gunaGradientPanel1 = new Guna.UI.WinForms.GunaGradientPanel();
            this.gunaLabel7 = new Guna.UI.WinForms.GunaLabel();
            this.txtticket = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaButton1 = new Guna.UI.WinForms.GunaButton();
            this.gunaLabel8 = new Guna.UI.WinForms.GunaLabel();
            this.btnReintentar = new Guna.UI.WinForms.GunaGradientButton();
            this.txtproceso = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaLabel6 = new Guna.UI.WinForms.GunaLabel();
            this.txtcoderror = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtestado = new Guna.UI.WinForms.GunaLineTextBox();
            this.txtdoc = new Guna.UI.WinForms.GunaLineTextBox();
            this.txttip_doc = new Guna.UI.WinForms.GunaLineTextBox();
            this.gunaLabel5 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel4 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtdescripcionerror = new Guna.UI.WinForms.GunaTextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.gunaGradientPanel2 = new Guna.UI.WinForms.GunaGradientPanel();
            this.gunaElipse1 = new Guna.UI.WinForms.GunaElipse(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.gunaGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.gunaGradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(293, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 43);
            this.label2.TabIndex = 3;
            this.label2.Text = "Espere un momento por favor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label3.Location = new System.Drawing.Point(584, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 56);
            this.label3.TabIndex = 6;
            this.label3.Text = "01";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.linkLabel2.Location = new System.Drawing.Point(55, 103);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(35, 13);
            this.linkLabel2.TabIndex = 14;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Cerrar";
            this.linkLabel2.Visible = false;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(19, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(223, 56);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // gunaGradientPanel1
            // 
            this.gunaGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel1.BackgroundImage")));
            this.gunaGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel1.Controls.Add(this.gunaLabel7);
            this.gunaGradientPanel1.Controls.Add(this.txtticket);
            this.gunaGradientPanel1.Controls.Add(this.gunaButton1);
            this.gunaGradientPanel1.Controls.Add(this.gunaLabel8);
            this.gunaGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gunaGradientPanel1.GradientColor1 = System.Drawing.Color.LightSeaGreen;
            this.gunaGradientPanel1.GradientColor2 = System.Drawing.Color.DarkTurquoise;
            this.gunaGradientPanel1.GradientColor3 = System.Drawing.Color.LightSeaGreen;
            this.gunaGradientPanel1.GradientColor4 = System.Drawing.Color.White;
            this.gunaGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gunaGradientPanel1.Name = "gunaGradientPanel1";
            this.gunaGradientPanel1.Size = new System.Drawing.Size(735, 60);
            this.gunaGradientPanel1.TabIndex = 0;
            this.gunaGradientPanel1.Text = "gunaGradientPanel1";
            // 
            // gunaLabel7
            // 
            this.gunaLabel7.AutoSize = true;
            this.gunaLabel7.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gunaLabel7.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel7.ForeColor = System.Drawing.Color.White;
            this.gunaLabel7.Location = new System.Drawing.Point(20, 21);
            this.gunaLabel7.Name = "gunaLabel7";
            this.gunaLabel7.Size = new System.Drawing.Size(140, 20);
            this.gunaLabel7.TabIndex = 16;
            this.gunaLabel7.Text = "Enviando Documento";
            this.gunaLabel7.Click += new System.EventHandler(this.gunaLabel7_Click);
            // 
            // txtticket
            // 
            this.txtticket.BackColor = System.Drawing.Color.White;
            this.txtticket.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtticket.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtticket.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtticket.LineColor = System.Drawing.Color.Gainsboro;
            this.txtticket.Location = new System.Drawing.Point(552, 16);
            this.txtticket.Name = "txtticket";
            this.txtticket.PasswordChar = '\0';
            this.txtticket.SelectedText = "";
            this.txtticket.Size = new System.Drawing.Size(71, 26);
            this.txtticket.TabIndex = 18;
            this.txtticket.Visible = false;
            // 
            // gunaButton1
            // 
            this.gunaButton1.AnimationHoverSpeed = 0.07F;
            this.gunaButton1.AnimationSpeed = 0.03F;
            this.gunaButton1.BackColor = System.Drawing.Color.Transparent;
            this.gunaButton1.BaseColor = System.Drawing.Color.Orange;
            this.gunaButton1.BorderColor = System.Drawing.Color.Ivory;
            this.gunaButton1.BorderSize = 1;
            this.gunaButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaButton1.ForeColor = System.Drawing.Color.White;
            this.gunaButton1.Image = null;
            this.gunaButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaButton1.Location = new System.Drawing.Point(671, 10);
            this.gunaButton1.Name = "gunaButton1";
            this.gunaButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaButton1.OnHoverImage = null;
            this.gunaButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaButton1.Radius = 15;
            this.gunaButton1.Size = new System.Drawing.Size(41, 33);
            this.gunaButton1.TabIndex = 15;
            this.gunaButton1.Text = "X";
            this.gunaButton1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gunaButton1.Click += new System.EventHandler(this.gunaButton1_Click);
            // 
            // gunaLabel8
            // 
            this.gunaLabel8.AutoSize = true;
            this.gunaLabel8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel8.Location = new System.Drawing.Point(456, 23);
            this.gunaLabel8.Name = "gunaLabel8";
            this.gunaLabel8.Size = new System.Drawing.Size(77, 20);
            this.gunaLabel8.TabIndex = 17;
            this.gunaLabel8.Text = "Nro Ticket";
            this.gunaLabel8.Visible = false;
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
            this.btnReintentar.BorderSize = 1;
            this.btnReintentar.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnReintentar.FocusedColor = System.Drawing.Color.Empty;
            this.btnReintentar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReintentar.ForeColor = System.Drawing.Color.White;
            this.btnReintentar.Image = ((System.Drawing.Image)(resources.GetObject("btnReintentar.Image")));
            this.btnReintentar.ImageSize = new System.Drawing.Size(30, 30);
            this.btnReintentar.Location = new System.Drawing.Point(560, 264);
            this.btnReintentar.Name = "btnReintentar";
            this.btnReintentar.OnHoverBaseColor1 = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.OnHoverBaseColor2 = System.Drawing.Color.DarkTurquoise;
            this.btnReintentar.OnHoverBorderColor = System.Drawing.Color.LightSeaGreen;
            this.btnReintentar.OnHoverForeColor = System.Drawing.Color.White;
            this.btnReintentar.OnHoverImage = null;
            this.btnReintentar.OnPressedColor = System.Drawing.Color.Black;
            this.btnReintentar.Radius = 10;
            this.btnReintentar.Size = new System.Drawing.Size(144, 42);
            this.btnReintentar.TabIndex = 34;
            this.btnReintentar.Text = "Reintentar";
            this.btnReintentar.Visible = false;
            this.btnReintentar.Click += new System.EventHandler(this.btnReintentar_Click);
            // 
            // txtproceso
            // 
            this.txtproceso.BackColor = System.Drawing.Color.White;
            this.txtproceso.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtproceso.Enabled = false;
            this.txtproceso.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtproceso.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproceso.LineColor = System.Drawing.Color.Gainsboro;
            this.txtproceso.Location = new System.Drawing.Point(560, 182);
            this.txtproceso.Name = "txtproceso";
            this.txtproceso.PasswordChar = '\0';
            this.txtproceso.SelectedText = "";
            this.txtproceso.Size = new System.Drawing.Size(117, 26);
            this.txtproceso.TabIndex = 30;
            // 
            // gunaLabel6
            // 
            this.gunaLabel6.AutoSize = true;
            this.gunaLabel6.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel6.Location = new System.Drawing.Point(561, 156);
            this.gunaLabel6.Name = "gunaLabel6";
            this.gunaLabel6.Size = new System.Drawing.Size(59, 15);
            this.gunaLabel6.TabIndex = 29;
            this.gunaLabel6.Text = "Proceso :";
            // 
            // txtcoderror
            // 
            this.txtcoderror.BackColor = System.Drawing.Color.White;
            this.txtcoderror.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtcoderror.Enabled = false;
            this.txtcoderror.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtcoderror.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcoderror.LineColor = System.Drawing.Color.Gainsboro;
            this.txtcoderror.Location = new System.Drawing.Point(19, 243);
            this.txtcoderror.Name = "txtcoderror";
            this.txtcoderror.PasswordChar = '\0';
            this.txtcoderror.SelectedText = "";
            this.txtcoderror.Size = new System.Drawing.Size(118, 26);
            this.txtcoderror.TabIndex = 27;
            // 
            // txtestado
            // 
            this.txtestado.BackColor = System.Drawing.Color.White;
            this.txtestado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtestado.Enabled = false;
            this.txtestado.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtestado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtestado.LineColor = System.Drawing.Color.Gainsboro;
            this.txtestado.Location = new System.Drawing.Point(426, 182);
            this.txtestado.Name = "txtestado";
            this.txtestado.PasswordChar = '\0';
            this.txtestado.SelectedText = "";
            this.txtestado.Size = new System.Drawing.Size(117, 26);
            this.txtestado.TabIndex = 26;
            // 
            // txtdoc
            // 
            this.txtdoc.BackColor = System.Drawing.Color.White;
            this.txtdoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdoc.Enabled = false;
            this.txtdoc.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtdoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdoc.LineColor = System.Drawing.Color.Gainsboro;
            this.txtdoc.Location = new System.Drawing.Point(166, 182);
            this.txtdoc.Name = "txtdoc";
            this.txtdoc.PasswordChar = '\0';
            this.txtdoc.SelectedText = "";
            this.txtdoc.Size = new System.Drawing.Size(232, 26);
            this.txtdoc.TabIndex = 25;
            // 
            // txttip_doc
            // 
            this.txttip_doc.BackColor = System.Drawing.Color.White;
            this.txttip_doc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txttip_doc.Enabled = false;
            this.txttip_doc.FocusedLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txttip_doc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttip_doc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txttip_doc.LineColor = System.Drawing.Color.Gainsboro;
            this.txttip_doc.Location = new System.Drawing.Point(20, 182);
            this.txttip_doc.Name = "txttip_doc";
            this.txttip_doc.PasswordChar = '\0';
            this.txttip_doc.SelectedText = "";
            this.txttip_doc.Size = new System.Drawing.Size(117, 26);
            this.txttip_doc.TabIndex = 24;
            // 
            // gunaLabel5
            // 
            this.gunaLabel5.AutoSize = true;
            this.gunaLabel5.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel5.Location = new System.Drawing.Point(165, 220);
            this.gunaLabel5.Name = "gunaLabel5";
            this.gunaLabel5.Size = new System.Drawing.Size(109, 15);
            this.gunaLabel5.TabIndex = 23;
            this.gunaLabel5.Text = "Descripcion Error :";
            // 
            // gunaLabel4
            // 
            this.gunaLabel4.AutoSize = true;
            this.gunaLabel4.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel4.Location = new System.Drawing.Point(16, 220);
            this.gunaLabel4.Name = "gunaLabel4";
            this.gunaLabel4.Size = new System.Drawing.Size(83, 15);
            this.gunaLabel4.TabIndex = 22;
            this.gunaLabel4.Text = "Codigo Error :";
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.AutoSize = true;
            this.gunaLabel3.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.Location = new System.Drawing.Point(423, 156);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(52, 15);
            this.gunaLabel3.TabIndex = 21;
            this.gunaLabel3.Text = "Estado :";
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.Location = new System.Drawing.Point(162, 156);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(100, 15);
            this.gunaLabel2.TabIndex = 20;
            this.gunaLabel2.Text = "Nro Documento :";
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel1.Location = new System.Drawing.Point(16, 156);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(104, 15);
            this.gunaLabel1.TabIndex = 19;
            this.gunaLabel1.Text = "Tipo Documento :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::TecnologiaTextil.FacturacionClient.Properties.Resources.load2;
            this.pictureBox1.Location = new System.Drawing.Point(529, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 142);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtdescripcionerror
            // 
            this.txtdescripcionerror.BaseColor = System.Drawing.Color.White;
            this.txtdescripcionerror.BorderColor = System.Drawing.Color.Silver;
            this.txtdescripcionerror.BorderSize = 1;
            this.txtdescripcionerror.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtdescripcionerror.FocusedBaseColor = System.Drawing.Color.White;
            this.txtdescripcionerror.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtdescripcionerror.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtdescripcionerror.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescripcionerror.Location = new System.Drawing.Point(166, 243);
            this.txtdescripcionerror.Multiline = true;
            this.txtdescripcionerror.Name = "txtdescripcionerror";
            this.txtdescripcionerror.PasswordChar = '\0';
            this.txtdescripcionerror.SelectedText = "";
            this.txtdescripcionerror.Size = new System.Drawing.Size(377, 98);
            this.txtdescripcionerror.TabIndex = 35;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(529, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(158, 142);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 36;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(529, 5);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(158, 142);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 37;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // gunaGradientPanel2
            // 
            this.gunaGradientPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gunaGradientPanel2.BackgroundImage")));
            this.gunaGradientPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.gunaGradientPanel2.Controls.Add(this.pictureBox4);
            this.gunaGradientPanel2.Controls.Add(this.pictureBox3);
            this.gunaGradientPanel2.Controls.Add(this.txtdescripcionerror);
            this.gunaGradientPanel2.Controls.Add(this.label3);
            this.gunaGradientPanel2.Controls.Add(this.label2);
            this.gunaGradientPanel2.Controls.Add(this.pictureBox1);
            this.gunaGradientPanel2.Controls.Add(this.pictureBox2);
            this.gunaGradientPanel2.Controls.Add(this.linkLabel2);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel1);
            this.gunaGradientPanel2.Controls.Add(this.btnReintentar);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel2);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel3);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel4);
            this.gunaGradientPanel2.Controls.Add(this.txtproceso);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel5);
            this.gunaGradientPanel2.Controls.Add(this.gunaLabel6);
            this.gunaGradientPanel2.Controls.Add(this.txttip_doc);
            this.gunaGradientPanel2.Controls.Add(this.txtdoc);
            this.gunaGradientPanel2.Controls.Add(this.txtcoderror);
            this.gunaGradientPanel2.Controls.Add(this.txtestado);
            this.gunaGradientPanel2.GradientColor1 = System.Drawing.SystemColors.MenuBar;
            this.gunaGradientPanel2.GradientColor2 = System.Drawing.Color.Teal;
            this.gunaGradientPanel2.GradientColor3 = System.Drawing.Color.White;
            this.gunaGradientPanel2.GradientColor4 = System.Drawing.Color.Linen;
            this.gunaGradientPanel2.Location = new System.Drawing.Point(0, 61);
            this.gunaGradientPanel2.Name = "gunaGradientPanel2";
            this.gunaGradientPanel2.Size = new System.Drawing.Size(734, 353);
            this.gunaGradientPanel2.TabIndex = 38;
            this.gunaGradientPanel2.Text = "gunaGradientPanel2";
            // 
            // gunaElipse1
            // 
            this.gunaElipse1.Radius = 26;
            this.gunaElipse1.TargetControl = this;
            // 
            // FrmEnvio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(735, 414);
            this.ControlBox = false;
            this.Controls.Add(this.gunaGradientPanel2);
            this.Controls.Add(this.gunaGradientPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEnvio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.gunaGradientPanel1.ResumeLayout(false);
            this.gunaGradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.gunaGradientPanel2.ResumeLayout(false);
            this.gunaGradientPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
		public System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel linkLabel2;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel1;
        private Guna.UI.WinForms.GunaLabel gunaLabel7;
        private Guna.UI.WinForms.GunaLineTextBox txtticket;
        private Guna.UI.WinForms.GunaButton gunaButton1;
        private Guna.UI.WinForms.GunaLabel gunaLabel8;
        private Guna.UI.WinForms.GunaGradientButton btnReintentar;
        private Guna.UI.WinForms.GunaLineTextBox txtproceso;
        private Guna.UI.WinForms.GunaLabel gunaLabel6;
        private Guna.UI.WinForms.GunaLineTextBox txtcoderror;
        private Guna.UI.WinForms.GunaLineTextBox txtestado;
        private Guna.UI.WinForms.GunaLineTextBox txtdoc;
        private Guna.UI.WinForms.GunaLineTextBox txttip_doc;
        private Guna.UI.WinForms.GunaLabel gunaLabel5;
        private Guna.UI.WinForms.GunaLabel gunaLabel4;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI.WinForms.GunaTextBox txtdescripcionerror;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private Guna.UI.WinForms.GunaGradientPanel gunaGradientPanel2;
        private Guna.UI.WinForms.GunaElipse gunaElipse1;
    }
}

