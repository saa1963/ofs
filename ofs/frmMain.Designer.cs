namespace ofs
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBalanceEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadFromExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOfs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBalances = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuF2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSprav = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuClients = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBlines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit2910 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.отчетыToolStripMenuItem,
            this.mnuSprav,
            this.mnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBalanceEditor,
            this.mnuLoadFromExcel,
            this.mnuEdit2910});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItem1.Text = "Балансы";
            // 
            // mnuBalanceEditor
            // 
            this.mnuBalanceEditor.Name = "mnuBalanceEditor";
            this.mnuBalanceEditor.Size = new System.Drawing.Size(172, 22);
            this.mnuBalanceEditor.Text = "Редактор";
            this.mnuBalanceEditor.Click += new System.EventHandler(this.mnuBalanceEditor_Click);
            // 
            // mnuLoadFromExcel
            // 
            this.mnuLoadFromExcel.Name = "mnuLoadFromExcel";
            this.mnuLoadFromExcel.Size = new System.Drawing.Size(172, 22);
            this.mnuLoadFromExcel.Text = "Загрузить из Excel";
            this.mnuLoadFromExcel.Click += new System.EventHandler(this.mnuLoadFromExcel_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOfs,
            this.mnuBalances,
            this.mnuF2});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // mnuOfs
            // 
            this.mnuOfs.Name = "mnuOfs";
            this.mnuOfs.Size = new System.Drawing.Size(252, 22);
            this.mnuOfs.Text = "Оценка финансового состояния";
            this.mnuOfs.Click += new System.EventHandler(this.mnuOfs_Click);
            // 
            // mnuBalances
            // 
            this.mnuBalances.Name = "mnuBalances";
            this.mnuBalances.Size = new System.Drawing.Size(252, 22);
            this.mnuBalances.Text = "Баланс";
            this.mnuBalances.Click += new System.EventHandler(this.mnuBalances_Click);
            // 
            // mnuF2
            // 
            this.mnuF2.Name = "mnuF2";
            this.mnuF2.Size = new System.Drawing.Size(252, 22);
            this.mnuF2.Text = "Форма-2";
            this.mnuF2.Click += new System.EventHandler(this.mnuF2_Click);
            // 
            // mnuSprav
            // 
            this.mnuSprav.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClients,
            this.mnuBlines});
            this.mnuSprav.Name = "mnuSprav";
            this.mnuSprav.Size = new System.Drawing.Size(94, 20);
            this.mnuSprav.Text = "Справочники";
            // 
            // mnuClients
            // 
            this.mnuClients.Name = "mnuClients";
            this.mnuClients.Size = new System.Drawing.Size(159, 22);
            this.mnuClients.Text = "Клиенты";
            this.mnuClients.Click += new System.EventHandler(this.mnuClients_Click);
            // 
            // mnuBlines
            // 
            this.mnuBlines.Name = "mnuBlines";
            this.mnuBlines.Size = new System.Drawing.Size(159, 22);
            this.mnuBlines.Text = "Статьи баланса";
            this.mnuBlines.Click += new System.EventHandler(this.mnuBlines_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(94, 20);
            this.mnuAbout.Text = "О программе";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuEdit2910
            // 
            this.mnuEdit2910.Name = "mnuEdit2910";
            this.mnuEdit2910.Size = new System.Drawing.Size(172, 22);
            this.mnuEdit2910.Text = "Правка 2910";
            this.mnuEdit2910.Visible = false;
            this.mnuEdit2910.Click += new System.EventHandler(this.mnuEdit2910_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 470);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuSprav;
        private System.Windows.Forms.ToolStripMenuItem mnuClients;
        private System.Windows.Forms.ToolStripMenuItem mnuBlines;
        private System.Windows.Forms.ToolStripMenuItem mnuBalanceEditor;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadFromExcel;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOfs;
        private System.Windows.Forms.ToolStripMenuItem mnuBalances;
        private System.Windows.Forms.ToolStripMenuItem mnuF2;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit2910;
    }
}

