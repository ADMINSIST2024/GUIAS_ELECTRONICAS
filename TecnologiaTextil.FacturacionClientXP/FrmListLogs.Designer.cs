﻿namespace TecnologiaTextil.FacturacionClientXP
{
	partial class FrmListLogs
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Hora = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Tamano = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(203, 201);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(179, 41);
			this.button1.TabIndex = 1;
			this.button1.Text = "Ver Log";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(7, 201);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(190, 41);
			this.button2.TabIndex = 1;
			this.button2.Text = "Cancelar";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(8, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Nombre:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(61, 24);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(321, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(8, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(136, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Lista de Archivos Logs";
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToResizeColumns = false;
			this.dataGridView1.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
			this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Hora,
            this.Tamano});
			this.dataGridView1.Location = new System.Drawing.Point(7, 48);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(375, 150);
			this.dataGridView1.TabIndex = 5;
			// 
			// Nombre
			// 
			this.Nombre.DataPropertyName = "Nombre";
			this.Nombre.HeaderText = "Archivo";
			this.Nombre.Name = "Nombre";
			this.Nombre.ReadOnly = true;
			this.Nombre.Width = 250;
			// 
			// Hora
			// 
			this.Hora.DataPropertyName = "Hora";
			this.Hora.HeaderText = "Hora";
			this.Hora.Name = "Hora";
			this.Hora.ReadOnly = true;
			this.Hora.Width = 50;
			// 
			// Tamano
			// 
			this.Tamano.DataPropertyName = "Tamano";
			this.Tamano.HeaderText = "Tamaño";
			this.Tamano.Name = "Tamano";
			this.Tamano.ReadOnly = true;
			this.Tamano.Width = 50;
			// 
			// FrmListLogs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 249);
			this.ControlBox = false;
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmListLogs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Lista de Logs ";
			this.Load += new System.EventHandler(this.FrmListLogs_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
		private System.Windows.Forms.DataGridViewTextBoxColumn Hora;
		private System.Windows.Forms.DataGridViewTextBoxColumn Tamano;
	}
}