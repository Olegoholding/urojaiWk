namespace urojaiWk.secondPage
{
    partial class seconPage
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
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.addBtn = new System.Windows.Forms.Button();
            this.dltBtn = new System.Windows.Forms.Button();
            this.srcEdit = new System.Windows.Forms.TextBox();
            this.srcBtn = new System.Windows.Forms.Button();
            this.infLbl = new System.Windows.Forms.Label();
            this.tablesCm = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Location = new System.Drawing.Point(12, 84);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 51;
            this.dataGrid.RowTemplate.Height = 24;
            this.dataGrid.Size = new System.Drawing.Size(760, 477);
            this.dataGrid.TabIndex = 0;
            // 
            // addBtn
            // 
            this.addBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.addBtn.Location = new System.Drawing.Point(778, 426);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(184, 84);
            this.addBtn.TabIndex = 1;
            this.addBtn.Text = "Добавить";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // dltBtn
            // 
            this.dltBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.dltBtn.Location = new System.Drawing.Point(778, 516);
            this.dltBtn.Name = "dltBtn";
            this.dltBtn.Size = new System.Drawing.Size(184, 45);
            this.dltBtn.TabIndex = 2;
            this.dltBtn.Text = "Удалить";
            this.dltBtn.UseVisualStyleBackColor = true;
            this.dltBtn.Click += new System.EventHandler(this.DltBtn_Click);
            // 
            // srcEdit
            // 
            this.srcEdit.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.srcEdit.Location = new System.Drawing.Point(13, 36);
            this.srcEdit.Name = "srcEdit";
            this.srcEdit.Size = new System.Drawing.Size(759, 42);
            this.srcEdit.TabIndex = 3;
            // 
            // srcBtn
            // 
            this.srcBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.srcBtn.Location = new System.Drawing.Point(778, 37);
            this.srcBtn.Name = "srcBtn";
            this.srcBtn.Size = new System.Drawing.Size(184, 41);
            this.srcBtn.TabIndex = 4;
            this.srcBtn.Text = "Поиск";
            this.srcBtn.UseVisualStyleBackColor = true;
            this.srcBtn.Click += new System.EventHandler(this.SrcBtn_Click);
            // 
            // infLbl
            // 
            this.infLbl.AutoSize = true;
            this.infLbl.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.infLbl.Location = new System.Drawing.Point(12, 8);
            this.infLbl.Name = "infLbl";
            this.infLbl.Size = new System.Drawing.Size(179, 25);
            this.infLbl.TabIndex = 5;
            this.infLbl.Text = "Совершить поиск";
            // 
            // tablesCm
            // 
            this.tablesCm.Font = new System.Drawing.Font("Microsoft YaHei UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.tablesCm.FormattingEnabled = true;
            this.tablesCm.Items.AddRange(new object[] {
            "Спортсмены",
            "Виды спорта",
            "Стадионы",
            "Журнал выступлений"});
            this.tablesCm.Location = new System.Drawing.Point(778, 117);
            this.tablesCm.Name = "tablesCm";
            this.tablesCm.Size = new System.Drawing.Size(184, 44);
            this.tablesCm.TabIndex = 6;
            this.tablesCm.Text = "Спортсмены";
            this.tablesCm.SelectedIndexChanged += new System.EventHandler(this.tablesCm_SelectedIndexChanged);
            this.tablesCm.Click += new System.EventHandler(this.tablesCm_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(777, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Выбрать таблицу";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.8F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(703, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(259, 29);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Упрощённый просмотр";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // seconPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 574);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tablesCm);
            this.Controls.Add(this.infLbl);
            this.Controls.Add(this.srcBtn);
            this.Controls.Add(this.srcEdit);
            this.Controls.Add(this.dltBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.dataGrid);
            this.Name = "seconPage";
            this.Text = "Вторая форма";
            this.Load += new System.EventHandler(this.seconPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button dltBtn;
        private System.Windows.Forms.TextBox srcEdit;
        private System.Windows.Forms.Button srcBtn;
        private System.Windows.Forms.Label infLbl;
        private System.Windows.Forms.ComboBox tablesCm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}