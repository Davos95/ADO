namespace ProyectoADO.Conectado
{
    partial class Form06Procedimientos
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtEnfermo = new System.Windows.Forms.TextBox();
            this.lstEnfermos = new System.Windows.Forms.ListBox();
            this.btnEliminarEnfermo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enfermo: ";
            // 
            // txtEnfermo
            // 
            this.txtEnfermo.Location = new System.Drawing.Point(70, 36);
            this.txtEnfermo.Name = "txtEnfermo";
            this.txtEnfermo.Size = new System.Drawing.Size(100, 20);
            this.txtEnfermo.TabIndex = 1;
            // 
            // lstEnfermos
            // 
            this.lstEnfermos.FormattingEnabled = true;
            this.lstEnfermos.Location = new System.Drawing.Point(12, 129);
            this.lstEnfermos.Name = "lstEnfermos";
            this.lstEnfermos.Size = new System.Drawing.Size(166, 238);
            this.lstEnfermos.TabIndex = 2;
            this.lstEnfermos.SelectedIndexChanged += new System.EventHandler(this.lstEnfermos_SelectedIndexChanged);
            // 
            // btnEliminarEnfermo
            // 
            this.btnEliminarEnfermo.Location = new System.Drawing.Point(15, 76);
            this.btnEliminarEnfermo.Name = "btnEliminarEnfermo";
            this.btnEliminarEnfermo.Size = new System.Drawing.Size(166, 23);
            this.btnEliminarEnfermo.TabIndex = 3;
            this.btnEliminarEnfermo.Text = "Eliminar enfermo";
            this.btnEliminarEnfermo.UseVisualStyleBackColor = true;
            this.btnEliminarEnfermo.Click += new System.EventHandler(this.btnEliminarEnfermo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lista de enfermos";
            // 
            // Form06Procedimientos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 381);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnEliminarEnfermo);
            this.Controls.Add(this.lstEnfermos);
            this.Controls.Add(this.txtEnfermo);
            this.Controls.Add(this.label1);
            this.Name = "Form06Procedimientos";
            this.Text = "Form06Procedimientos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEnfermo;
        private System.Windows.Forms.ListBox lstEnfermos;
        private System.Windows.Forms.Button btnEliminarEnfermo;
        private System.Windows.Forms.Label label2;
    }
}