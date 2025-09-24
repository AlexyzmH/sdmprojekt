namespace Fahrzeug_Datenbank
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.Fahrzeug_ComboBox = new System.Windows.Forms.ComboBox();
            this.Daten_laden_Button = new System.Windows.Forms.Button();
            this.FahrzeugeSuchen_Button = new System.Windows.Forms.Button();
            this.FahrzeugAuswählen_Button = new System.Windows.Forms.Button();
            this.DatenSpeichern_Button = new System.Windows.Forms.Button();
            this.Fahrzeuge_ListBox1 = new System.Windows.Forms.ListBox();
            this.Fahrzeuge_ListBox2 = new System.Windows.Forms.ListBox();
            this.Infos_Button1 = new System.Windows.Forms.Button();
            this.Infos_Button2 = new System.Windows.Forms.Button();
            this.Fahrzeugdaten_TextBox = new System.Windows.Forms.TextBox();
            this.Fahrzeugdaten_Label = new System.Windows.Forms.Label();
            this.Beenden_Button = new System.Windows.Forms.Button();
            this.ListBox1_Label = new System.Windows.Forms.Label();
            this.ListBox2_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Fahrzeug_ComboBox
            // 
            this.Fahrzeug_ComboBox.FormattingEnabled = true;
            this.Fahrzeug_ComboBox.Items.AddRange(new object[] {
            "alle",
            "Auto",
            "Motorrad"});
            this.Fahrzeug_ComboBox.Location = new System.Drawing.Point(12, 92);
            this.Fahrzeug_ComboBox.Name = "Fahrzeug_ComboBox";
            this.Fahrzeug_ComboBox.Size = new System.Drawing.Size(111, 21);
            this.Fahrzeug_ComboBox.TabIndex = 0;
            // 
            // Daten_laden_Button
            // 
            this.Daten_laden_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Daten_laden_Button.Location = new System.Drawing.Point(2, 142);
            this.Daten_laden_Button.Name = "Daten_laden_Button";
            this.Daten_laden_Button.Size = new System.Drawing.Size(121, 48);
            this.Daten_laden_Button.TabIndex = 1;
            this.Daten_laden_Button.Text = "Daten laden";
            this.Daten_laden_Button.UseVisualStyleBackColor = true;
            this.Daten_laden_Button.Click += new System.EventHandler(this.Daten_laden_Button_Click);
            // 
            // FahrzeugeSuchen_Button
            // 
            this.FahrzeugeSuchen_Button.Location = new System.Drawing.Point(162, 92);
            this.FahrzeugeSuchen_Button.Name = "FahrzeugeSuchen_Button";
            this.FahrzeugeSuchen_Button.Size = new System.Drawing.Size(111, 23);
            this.FahrzeugeSuchen_Button.TabIndex = 2;
            this.FahrzeugeSuchen_Button.Text = "Fahrzeuge suchen";
            this.FahrzeugeSuchen_Button.UseVisualStyleBackColor = true;
            this.FahrzeugeSuchen_Button.Click += new System.EventHandler(this.FahrzeugeSuchen_Button_Click);
            // 
            // FahrzeugAuswählen_Button
            // 
            this.FahrzeugAuswählen_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FahrzeugAuswählen_Button.Location = new System.Drawing.Point(335, 141);
            this.FahrzeugAuswählen_Button.Name = "FahrzeugAuswählen_Button";
            this.FahrzeugAuswählen_Button.Size = new System.Drawing.Size(129, 49);
            this.FahrzeugAuswählen_Button.TabIndex = 4;
            this.FahrzeugAuswählen_Button.Text = "Fahrzeug auswählen";
            this.FahrzeugAuswählen_Button.UseVisualStyleBackColor = true;
            this.FahrzeugAuswählen_Button.Click += new System.EventHandler(this.FahrzeugAuswählen_Button_Click);
            // 
            // DatenSpeichern_Button
            // 
            this.DatenSpeichern_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatenSpeichern_Button.Location = new System.Drawing.Point(675, 141);
            this.DatenSpeichern_Button.Name = "DatenSpeichern_Button";
            this.DatenSpeichern_Button.Size = new System.Drawing.Size(121, 49);
            this.DatenSpeichern_Button.TabIndex = 5;
            this.DatenSpeichern_Button.Text = "Daten speichern";
            this.DatenSpeichern_Button.UseVisualStyleBackColor = true;
            this.DatenSpeichern_Button.Click += new System.EventHandler(this.DatenSpeichern_Button_Click);
            // 
            // Fahrzeuge_ListBox1
            // 
            this.Fahrzeuge_ListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.Fahrzeuge_ListBox1.FormattingEnabled = true;
            this.Fahrzeuge_ListBox1.Location = new System.Drawing.Point(129, 142);
            this.Fahrzeuge_ListBox1.Name = "Fahrzeuge_ListBox1";
            this.Fahrzeuge_ListBox1.Size = new System.Drawing.Size(186, 290);
            this.Fahrzeuge_ListBox1.TabIndex = 6;
            // 
            // Fahrzeuge_ListBox2
            // 
            this.Fahrzeuge_ListBox2.FormattingEnabled = true;
            this.Fahrzeuge_ListBox2.Location = new System.Drawing.Point(483, 141);
            this.Fahrzeuge_ListBox2.Name = "Fahrzeuge_ListBox2";
            this.Fahrzeuge_ListBox2.Size = new System.Drawing.Size(186, 290);
            this.Fahrzeuge_ListBox2.TabIndex = 7;
            // 
            // Infos_Button1
            // 
            this.Infos_Button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Infos_Button1.Location = new System.Drawing.Point(2, 383);
            this.Infos_Button1.Name = "Infos_Button1";
            this.Infos_Button1.Size = new System.Drawing.Size(121, 48);
            this.Infos_Button1.TabIndex = 9;
            this.Infos_Button1.Text = "Infos";
            this.Infos_Button1.UseVisualStyleBackColor = true;
            this.Infos_Button1.Click += new System.EventHandler(this.Infos_Button1_Click);
            // 
            // Infos_Button2
            // 
            this.Infos_Button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Infos_Button2.Location = new System.Drawing.Point(675, 383);
            this.Infos_Button2.Name = "Infos_Button2";
            this.Infos_Button2.Size = new System.Drawing.Size(120, 49);
            this.Infos_Button2.TabIndex = 10;
            this.Infos_Button2.Text = "Infos";
            this.Infos_Button2.UseVisualStyleBackColor = true;
            this.Infos_Button2.Click += new System.EventHandler(this.Infos_Button2_Click);
            // 
            // Fahrzeugdaten_TextBox
            // 
            this.Fahrzeugdaten_TextBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Fahrzeugdaten_TextBox.ForeColor = System.Drawing.SystemColors.Window;
            this.Fahrzeugdaten_TextBox.Location = new System.Drawing.Point(12, 57);
            this.Fahrzeugdaten_TextBox.Name = "Fahrzeugdaten_TextBox";
            this.Fahrzeugdaten_TextBox.ReadOnly = true;
            this.Fahrzeugdaten_TextBox.Size = new System.Drawing.Size(776, 20);
            this.Fahrzeugdaten_TextBox.TabIndex = 11;
            // 
            // Fahrzeugdaten_Label
            // 
            this.Fahrzeugdaten_Label.AutoSize = true;
            this.Fahrzeugdaten_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fahrzeugdaten_Label.Location = new System.Drawing.Point(340, 38);
            this.Fahrzeugdaten_Label.Name = "Fahrzeugdaten_Label";
            this.Fahrzeugdaten_Label.Size = new System.Drawing.Size(111, 16);
            this.Fahrzeugdaten_Label.TabIndex = 12;
            this.Fahrzeugdaten_Label.Text = "Fahrzeugdaten";
            // 
            // Beenden_Button
            // 
            this.Beenden_Button.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Beenden_Button.Cursor = System.Windows.Forms.Cursors.Default;
            this.Beenden_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Beenden_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Beenden_Button.Location = new System.Drawing.Point(343, 383);
            this.Beenden_Button.Name = "Beenden_Button";
            this.Beenden_Button.Size = new System.Drawing.Size(108, 49);
            this.Beenden_Button.TabIndex = 13;
            this.Beenden_Button.Text = "Beenden";
            this.Beenden_Button.UseVisualStyleBackColor = false;
            this.Beenden_Button.Click += new System.EventHandler(this.Beenden_Button_Click);
            // 
            // ListBox1_Label
            // 
            this.ListBox1_Label.AutoSize = true;
            this.ListBox1_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox1_Label.Location = new System.Drawing.Point(129, 126);
            this.ListBox1_Label.Name = "ListBox1_Label";
            this.ListBox1_Label.Size = new System.Drawing.Size(97, 16);
            this.ListBox1_Label.TabIndex = 14;
            this.ListBox1_Label.Text = "alle Fahrzeuge";
            // 
            // ListBox2_Label
            // 
            this.ListBox2_Label.AutoSize = true;
            this.ListBox2_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox2_Label.Location = new System.Drawing.Point(483, 124);
            this.ListBox2_Label.Name = "ListBox2_Label";
            this.ListBox2_Label.Size = new System.Drawing.Size(129, 16);
            this.ListBox2_Label.TabIndex = 15;
            this.ListBox2_Label.Text = "gewählte Fahrzeuge";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ListBox2_Label);
            this.Controls.Add(this.ListBox1_Label);
            this.Controls.Add(this.Beenden_Button);
            this.Controls.Add(this.Fahrzeugdaten_Label);
            this.Controls.Add(this.Fahrzeugdaten_TextBox);
            this.Controls.Add(this.Infos_Button2);
            this.Controls.Add(this.Infos_Button1);
            this.Controls.Add(this.Fahrzeuge_ListBox2);
            this.Controls.Add(this.Fahrzeuge_ListBox1);
            this.Controls.Add(this.DatenSpeichern_Button);
            this.Controls.Add(this.FahrzeugAuswählen_Button);
            this.Controls.Add(this.FahrzeugeSuchen_Button);
            this.Controls.Add(this.Daten_laden_Button);
            this.Controls.Add(this.Fahrzeug_ComboBox);
            this.Name = "Form1";
            this.Text = "Fahrzeug-Datenbank";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox Fahrzeug_ComboBox;
        private System.Windows.Forms.Button Daten_laden_Button;
        private System.Windows.Forms.Button FahrzeugeSuchen_Button;
        private System.Windows.Forms.Button FahrzeugAuswählen_Button;
        private System.Windows.Forms.Button DatenSpeichern_Button;
        private System.Windows.Forms.ListBox Fahrzeuge_ListBox1;
        private System.Windows.Forms.ListBox Fahrzeuge_ListBox2;
        private System.Windows.Forms.Button Infos_Button1;
        private System.Windows.Forms.Button Infos_Button2;
        private System.Windows.Forms.TextBox Fahrzeugdaten_TextBox;
        private System.Windows.Forms.Label Fahrzeugdaten_Label;
        private System.Windows.Forms.Button Beenden_Button;
        private System.Windows.Forms.Label ListBox1_Label;
        private System.Windows.Forms.Label ListBox2_Label;
    }
}

