namespace ofs
{
    partial class frmBlinesEdit
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
            this.lblCode = new System.Windows.Forms.Label();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.tbNameLine = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbIsNegative = new System.Windows.Forms.CheckBox();
            this.tbCalculated = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbCodeSort = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.tbCodeSort)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(28, 28);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(26, 13);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Код";
            // 
            // tbCode
            // 
            this.tbCode.Location = new System.Drawing.Point(78, 25);
            this.tbCode.MaxLength = 4;
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(46, 20);
            this.tbCode.TabIndex = 1;
            // 
            // tbNameLine
            // 
            this.tbNameLine.Location = new System.Drawing.Point(31, 80);
            this.tbNameLine.MaxLength = 200;
            this.tbNameLine.Name = "tbNameLine";
            this.tbNameLine.Size = new System.Drawing.Size(430, 20);
            this.tbNameLine.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Наименование";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(122, 176);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(86, 26);
            this.btnOk.TabIndex = 9;
            this.btnOk.Text = "ОК";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 26);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tbIsNegative
            // 
            this.tbIsNegative.AutoSize = true;
            this.tbIsNegative.Location = new System.Drawing.Point(357, 27);
            this.tbIsNegative.Name = "tbIsNegative";
            this.tbIsNegative.Size = new System.Drawing.Size(104, 17);
            this.tbIsNegative.TabIndex = 4;
            this.tbIsNegative.Text = "Отрицательное";
            this.tbIsNegative.UseVisualStyleBackColor = true;
            // 
            // tbCalculated
            // 
            this.tbCalculated.Location = new System.Drawing.Point(31, 118);
            this.tbCalculated.MaxLength = 200;
            this.tbCalculated.Name = "tbCalculated";
            this.tbCalculated.Size = new System.Drawing.Size(430, 20);
            this.tbCalculated.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Формула";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(146, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Код для сортировки";
            // 
            // tbCodeSort
            // 
            this.tbCodeSort.Location = new System.Drawing.Point(261, 26);
            this.tbCodeSort.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.tbCodeSort.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.tbCodeSort.Name = "tbCodeSort";
            this.tbCodeSort.Size = new System.Drawing.Size(75, 20);
            this.tbCodeSort.TabIndex = 3;
            // 
            // frmBlinesEdit
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(489, 234);
            this.Controls.Add(this.tbCodeSort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbCalculated);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbIsNegative);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tbNameLine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbCode);
            this.Controls.Add(this.lblCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBlinesEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.tbCodeSort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCode;
        public System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.TextBox tbNameLine;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox tbIsNegative;
        private System.Windows.Forms.TextBox tbCalculated;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown tbCodeSort;
    }
}