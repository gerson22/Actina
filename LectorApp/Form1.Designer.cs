namespace LectorApp
{
    partial class Form1
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
            this.rtbEventos = new System.Windows.Forms.RichTextBox();
            this.bSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbServerStatus = new System.Windows.Forms.TextBox();
            this.bIniciarServidor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tbMensaje = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.bIniciarCliente = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbEventos
            // 
            this.rtbEventos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbEventos.Location = new System.Drawing.Point(0, 131);
            this.rtbEventos.Name = "rtbEventos";
            this.rtbEventos.Size = new System.Drawing.Size(643, 159);
            this.rtbEventos.TabIndex = 0;
            this.rtbEventos.Text = "";
            // 
            // bSalir
            // 
            this.bSalir.Location = new System.Drawing.Point(556, 12);
            this.bSalir.Name = "bSalir";
            this.bSalir.Size = new System.Drawing.Size(75, 23);
            this.bSalir.TabIndex = 1;
            this.bSalir.Text = "Salir";
            this.bSalir.UseVisualStyleBackColor = true;
            this.bSalir.Click += new System.EventHandler(this.bSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Servidor:";
            // 
            // tbServerStatus
            // 
            this.tbServerStatus.Location = new System.Drawing.Point(67, 14);
            this.tbServerStatus.Name = "tbServerStatus";
            this.tbServerStatus.ReadOnly = true;
            this.tbServerStatus.Size = new System.Drawing.Size(100, 20);
            this.tbServerStatus.TabIndex = 3;
            this.tbServerStatus.Text = "Apagado";
            // 
            // bIniciarServidor
            // 
            this.bIniciarServidor.Enabled = false;
            this.bIniciarServidor.Location = new System.Drawing.Point(173, 12);
            this.bIniciarServidor.Name = "bIniciarServidor";
            this.bIniciarServidor.Size = new System.Drawing.Size(75, 23);
            this.bIniciarServidor.TabIndex = 4;
            this.bIniciarServidor.Text = "Iniciar";
            this.bIniciarServidor.UseVisualStyleBackColor = true;
            this.bIniciarServidor.Click += new System.EventHandler(this.bIniciar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Mandar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbMensaje
            // 
            this.tbMensaje.Location = new System.Drawing.Point(67, 66);
            this.tbMensaje.Name = "tbMensaje";
            this.tbMensaje.Size = new System.Drawing.Size(100, 20);
            this.tbMensaje.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Cliente:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 40);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "Apagado";
            // 
            // bIniciarCliente
            // 
            this.bIniciarCliente.Location = new System.Drawing.Point(173, 38);
            this.bIniciarCliente.Name = "bIniciarCliente";
            this.bIniciarCliente.Size = new System.Drawing.Size(75, 23);
            this.bIniciarCliente.TabIndex = 9;
            this.bIniciarCliente.Text = "Iniciar";
            this.bIniciarCliente.UseVisualStyleBackColor = true;
            this.bIniciarCliente.Click += new System.EventHandler(this.bIniciarCliente_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 290);
            this.Controls.Add(this.bIniciarCliente);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbMensaje);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bIniciarServidor);
            this.Controls.Add(this.tbServerStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bSalir);
            this.Controls.Add(this.rtbEventos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "LectorApp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbEventos;
        private System.Windows.Forms.Button bSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbServerStatus;
        private System.Windows.Forms.Button bIniciarServidor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tbMensaje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button bIniciarCliente;

    }
}

