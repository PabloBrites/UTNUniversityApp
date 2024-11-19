namespace BaseDeDatos
{
    partial class Cursos
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
            this.dgvCursos = new System.Windows.Forms.DataGridView();
            this.btnCargarMateria = new System.Windows.Forms.Button();
            this.cmbAlumno = new System.Windows.Forms.ComboBox();
            this.cmbMateria = new System.Windows.Forms.ComboBox();
            this.lblAlumno = new System.Windows.Forms.Label();
            this.lblMateria = new System.Windows.Forms.Label();
            this.rbtnAlumno = new System.Windows.Forms.RadioButton();
            this.rbtnMateria = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblFiltro = new System.Windows.Forms.Label();
            this.tbxFiltro = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCursos
            // 
            this.dgvCursos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCursos.Location = new System.Drawing.Point(56, 85);
            this.dgvCursos.Name = "dgvCursos";
            this.dgvCursos.Size = new System.Drawing.Size(675, 238);
            this.dgvCursos.TabIndex = 0;
            // 
            // btnCargarMateria
            // 
            this.btnCargarMateria.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCargarMateria.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCargarMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarMateria.Location = new System.Drawing.Point(856, 85);
            this.btnCargarMateria.Name = "btnCargarMateria";
            this.btnCargarMateria.Size = new System.Drawing.Size(130, 65);
            this.btnCargarMateria.TabIndex = 2;
            this.btnCargarMateria.Text = "Cargar Materia";
            this.btnCargarMateria.UseVisualStyleBackColor = false;
            this.btnCargarMateria.Click += new System.EventHandler(this.btnCargarMateria_Click);
            // 
            // cmbAlumno
            // 
            this.cmbAlumno.FormattingEnabled = true;
            this.cmbAlumno.Location = new System.Drawing.Point(56, 402);
            this.cmbAlumno.Name = "cmbAlumno";
            this.cmbAlumno.Size = new System.Drawing.Size(295, 21);
            this.cmbAlumno.TabIndex = 3;
            // 
            // cmbMateria
            // 
            this.cmbMateria.FormattingEnabled = true;
            this.cmbMateria.Location = new System.Drawing.Point(387, 402);
            this.cmbMateria.Name = "cmbMateria";
            this.cmbMateria.Size = new System.Drawing.Size(319, 21);
            this.cmbMateria.TabIndex = 4;
            this.cmbMateria.SelectedIndexChanged += new System.EventHandler(this.cmbMateria_SelectedIndexChanged);
            // 
            // lblAlumno
            // 
            this.lblAlumno.AutoSize = true;
            this.lblAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlumno.Location = new System.Drawing.Point(53, 381);
            this.lblAlumno.Name = "lblAlumno";
            this.lblAlumno.Size = new System.Drawing.Size(62, 18);
            this.lblAlumno.TabIndex = 5;
            this.lblAlumno.Text = "Alumno:";
            // 
            // lblMateria
            // 
            this.lblMateria.AutoSize = true;
            this.lblMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMateria.Location = new System.Drawing.Point(384, 381);
            this.lblMateria.Name = "lblMateria";
            this.lblMateria.Size = new System.Drawing.Size(57, 18);
            this.lblMateria.TabIndex = 6;
            this.lblMateria.Text = "Materia";
            // 
            // rbtnAlumno
            // 
            this.rbtnAlumno.AutoSize = true;
            this.rbtnAlumno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAlumno.Location = new System.Drawing.Point(344, 49);
            this.rbtnAlumno.Name = "rbtnAlumno";
            this.rbtnAlumno.Size = new System.Drawing.Size(75, 22);
            this.rbtnAlumno.TabIndex = 7;
            this.rbtnAlumno.TabStop = true;
            this.rbtnAlumno.Text = "alumno";
            this.rbtnAlumno.UseVisualStyleBackColor = true;
            // 
            // rbtnMateria
            // 
            this.rbtnMateria.AutoSize = true;
            this.rbtnMateria.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMateria.Location = new System.Drawing.Point(470, 49);
            this.rbtnMateria.Name = "rbtnMateria";
            this.rbtnMateria.Size = new System.Drawing.Size(75, 22);
            this.rbtnMateria.TabIndex = 8;
            this.rbtnMateria.TabStop = true;
            this.rbtnMateria.Text = "materia";
            this.rbtnMateria.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(435, 437);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1, 20);
            this.textBox1.TabIndex = 9;
            // 
            // lblFiltro
            // 
            this.lblFiltro.AutoSize = true;
            this.lblFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFiltro.Location = new System.Drawing.Point(53, 28);
            this.lblFiltro.Name = "lblFiltro";
            this.lblFiltro.Size = new System.Drawing.Size(49, 18);
            this.lblFiltro.TabIndex = 10;
            this.lblFiltro.Text = "Filtrar:";
            // 
            // tbxFiltro
            // 
            this.tbxFiltro.Location = new System.Drawing.Point(56, 49);
            this.tbxFiltro.Multiline = true;
            this.tbxFiltro.Name = "tbxFiltro";
            this.tbxFiltro.Size = new System.Drawing.Size(246, 30);
            this.tbxFiltro.TabIndex = 11;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::BaseDeDatos.Properties.Resources.agregar_usuario4;
            this.pictureBox2.Location = new System.Drawing.Point(794, 85);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(46, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BaseDeDatos.Properties.Resources.aami8_27_5121;
            this.pictureBox1.Location = new System.Drawing.Point(913, 452);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(108, 84);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Cursos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 523);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.tbxFiltro);
            this.Controls.Add(this.lblFiltro);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.rbtnMateria);
            this.Controls.Add(this.rbtnAlumno);
            this.Controls.Add(this.lblMateria);
            this.Controls.Add(this.lblAlumno);
            this.Controls.Add(this.cmbMateria);
            this.Controls.Add(this.cmbAlumno);
            this.Controls.Add(this.btnCargarMateria);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgvCursos);
            this.Name = "Cursos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cursos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCursos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCursos;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCargarMateria;
        private System.Windows.Forms.ComboBox cmbAlumno;
        private System.Windows.Forms.ComboBox cmbMateria;
        private System.Windows.Forms.Label lblAlumno;
        private System.Windows.Forms.Label lblMateria;
        private System.Windows.Forms.RadioButton rbtnAlumno;
        private System.Windows.Forms.RadioButton rbtnMateria;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblFiltro;
        private System.Windows.Forms.TextBox tbxFiltro;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}