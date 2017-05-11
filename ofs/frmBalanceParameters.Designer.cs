namespace ofs
{
    partial class frmBalanceParameters
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
            this.tbClient = new System.Windows.Forms.ComboBox();
            this.btnClient = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbYear = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbQuater = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Клиент";
            // 
            // tbClient
            // 
            this.tbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbClient.FormattingEnabled = true;
            this.tbClient.Location = new System.Drawing.Point(27, 35);
            this.tbClient.Name = "tbClient";
            this.tbClient.Size = new System.Drawing.Size(305, 21);
            this.tbClient.TabIndex = 1;
            // 
            // btnClient
            // 
            this.btnClient.Location = new System.Drawing.Point(338, 29);
            this.btnClient.Name = "btnClient";
            this.btnClient.Size = new System.Drawing.Size(37, 30);
            this.btnClient.TabIndex = 2;
            this.btnClient.Text = "(...)";
            this.btnClient.UseVisualStyleBackColor = true;
            this.btnClient.Click += new System.EventHandler(this.btnClient_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Год";
            // 
            // tbYear
            // 
            this.tbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbYear.FormattingEnabled = true;
            this.tbYear.Location = new System.Drawing.Point(27, 75);
            this.tbYear.Name = "tbYear";
            this.tbYear.Size = new System.Drawing.Size(71, 21);
            this.tbYear.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(114, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Квартал";
            // 
            // tbQuater
            // 
            this.tbQuater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbQuater.FormattingEnabled = true;
            this.tbQuater.Location = new System.Drawing.Point(117, 75);
            this.tbQuater.Name = "tbQuater";
            this.tbQuater.Size = new System.Drawing.Size(71, 21);
            this.tbQuater.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(100, 134);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 28);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(227, 134);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 28);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmBalanceParameters
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 198);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbQuater);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClient);
            this.Controls.Add(this.tbClient);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBalanceParameters";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox tbClient;
        private System.Windows.Forms.Button btnClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tbYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox tbQuater;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}