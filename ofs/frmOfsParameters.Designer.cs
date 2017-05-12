namespace ofs
{
    partial class frmOfsParameters
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tbQuater = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClient = new System.Windows.Forms.Button();
            this.tbClient = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(226, 140);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 28);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(99, 140);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 28);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click_1);
            // 
            // tbQuater
            // 
            this.tbQuater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbQuater.FormattingEnabled = true;
            this.tbQuater.Location = new System.Drawing.Point(26, 83);
            this.tbQuater.Name = "tbQuater";
            this.tbQuater.Size = new System.Drawing.Size(71, 21);
            this.tbQuater.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Квартал";
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(337, 35);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(37, 30);
            this.btnClient.TabIndex = 11;
            this.btnClient.Text = "(...)";
            this.btnClient.UseVisualStyleBackColor = true;
            // 
            // tbClient
            // 
            this.tbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbClient.FormattingEnabled = true;
            this.tbClient.Location = new System.Drawing.Point(26, 41);
            this.tbClient.Name = "tbClient";
            this.tbClient.Size = new System.Drawing.Size(305, 21);
            this.tbClient.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Клиент";
            // 
            // frmOfsParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 196);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbQuater);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.tbClient);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOfsParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox tbQuater;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.ComboBox tbClient;
        private System.Windows.Forms.Label label1;
    }
}