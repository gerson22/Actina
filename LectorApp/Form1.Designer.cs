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
            this.SuspendLayout();
            // 
            // rtbEventos
            // 
            this.rtbEventos.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbEventos.Location = new System.Drawing.Point(0, 50);
            this.rtbEventos.Name = "rtbEventos";
            this.rtbEventos.Size = new System.Drawing.Size(643, 204);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 254);
            this.Controls.Add(this.bSalir);
            this.Controls.Add(this.rtbEventos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "LectorApp";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbEventos;
        private System.Windows.Forms.Button bSalir;

    }
}

