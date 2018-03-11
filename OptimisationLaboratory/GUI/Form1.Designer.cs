namespace GUI
{
    partial class Form1
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
            this.Matr1 = new System.Windows.Forms.DataGridView();
            this.Matr1N = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Matr1M = new System.Windows.Forms.TextBox();
            this.Matr2 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.Matr2M = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Matr2N = new System.Windows.Forms.TextBox();
            this.MulButton = new System.Windows.Forms.Button();
            this.SubButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DivButton = new System.Windows.Forms.Button();
            this.AnswerMatr = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Matr1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Matr2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnswerMatr)).BeginInit();
            this.SuspendLayout();
            // 
            // Matr1
            // 
            this.Matr1.AllowUserToAddRows = false;
            this.Matr1.AllowUserToDeleteRows = false;
            this.Matr1.AllowUserToResizeColumns = false;
            this.Matr1.AllowUserToResizeRows = false;
            this.Matr1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Matr1.Location = new System.Drawing.Point(12, 51);
            this.Matr1.MultiSelect = false;
            this.Matr1.Name = "Matr1";
            this.Matr1.Size = new System.Drawing.Size(240, 150);
            this.Matr1.TabIndex = 0;
            this.Matr1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Matr1_CellValueChanged);
            // 
            // Matr1N
            // 
            this.Matr1N.Location = new System.Drawing.Point(34, 25);
            this.Matr1N.Name = "Matr1N";
            this.Matr1N.Size = new System.Drawing.Size(53, 20);
            this.Matr1N.TabIndex = 1;
            this.Matr1N.TextChanged += new System.EventHandler(this.Matr1N_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Количество строк";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Количество столбцов";
            // 
            // Matr1M
            // 
            this.Matr1M.Location = new System.Drawing.Point(158, 25);
            this.Matr1M.Name = "Matr1M";
            this.Matr1M.Size = new System.Drawing.Size(53, 20);
            this.Matr1M.TabIndex = 3;
            this.Matr1M.TextChanged += new System.EventHandler(this.Matr1M_TextChanged);
            // 
            // Matr2
            // 
            this.Matr2.AllowUserToAddRows = false;
            this.Matr2.AllowUserToDeleteRows = false;
            this.Matr2.AllowUserToResizeColumns = false;
            this.Matr2.AllowUserToResizeRows = false;
            this.Matr2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Matr2.Location = new System.Drawing.Point(258, 51);
            this.Matr2.MultiSelect = false;
            this.Matr2.Name = "Matr2";
            this.Matr2.Size = new System.Drawing.Size(240, 150);
            this.Matr2.TabIndex = 5;
            this.Matr2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Matr2_CellValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Количество столбцов";
            // 
            // Matr2M
            // 
            this.Matr2M.Location = new System.Drawing.Point(404, 25);
            this.Matr2M.Name = "Matr2M";
            this.Matr2M.Size = new System.Drawing.Size(53, 20);
            this.Matr2M.TabIndex = 8;
            this.Matr2M.TextChanged += new System.EventHandler(this.Matr2M_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(255, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Количество строк";
            // 
            // Matr2N
            // 
            this.Matr2N.Location = new System.Drawing.Point(277, 25);
            this.Matr2N.Name = "Matr2N";
            this.Matr2N.Size = new System.Drawing.Size(53, 20);
            this.Matr2N.TabIndex = 6;
            this.Matr2N.TextChanged += new System.EventHandler(this.Matr2N_TextChanged);
            // 
            // MulButton
            // 
            this.MulButton.Location = new System.Drawing.Point(12, 207);
            this.MulButton.Name = "MulButton";
            this.MulButton.Size = new System.Drawing.Size(75, 23);
            this.MulButton.TabIndex = 10;
            this.MulButton.Text = "Multiply";
            this.MulButton.UseVisualStyleBackColor = true;
            this.MulButton.Click += new System.EventHandler(this.MulButton_Click);
            // 
            // SubButton
            // 
            this.SubButton.Location = new System.Drawing.Point(93, 207);
            this.SubButton.Name = "SubButton";
            this.SubButton.Size = new System.Drawing.Size(75, 23);
            this.SubButton.TabIndex = 11;
            this.SubButton.Text = "Subtract";
            this.SubButton.UseVisualStyleBackColor = true;
            this.SubButton.Click += new System.EventHandler(this.SubButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(174, 207);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 12;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DivButton
            // 
            this.DivButton.Location = new System.Drawing.Point(255, 207);
            this.DivButton.Name = "DivButton";
            this.DivButton.Size = new System.Drawing.Size(75, 23);
            this.DivButton.TabIndex = 13;
            this.DivButton.Text = "Divide";
            this.DivButton.UseVisualStyleBackColor = true;
            this.DivButton.Click += new System.EventHandler(this.DivButton_Click);
            // 
            // AnswerMatr
            // 
            this.AnswerMatr.AllowUserToAddRows = false;
            this.AnswerMatr.AllowUserToDeleteRows = false;
            this.AnswerMatr.AllowUserToResizeColumns = false;
            this.AnswerMatr.AllowUserToResizeRows = false;
            this.AnswerMatr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnswerMatr.Location = new System.Drawing.Point(12, 236);
            this.AnswerMatr.MultiSelect = false;
            this.AnswerMatr.Name = "AnswerMatr";
            this.AnswerMatr.ReadOnly = true;
            this.AnswerMatr.Size = new System.Drawing.Size(240, 150);
            this.AnswerMatr.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 401);
            this.Controls.Add(this.AnswerMatr);
            this.Controls.Add(this.DivButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.SubButton);
            this.Controls.Add(this.MulButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Matr2M);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Matr2N);
            this.Controls.Add(this.Matr2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Matr1M);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Matr1N);
            this.Controls.Add(this.Matr1);
            this.Name = "Form1";
            this.Text = "Matrices";
            ((System.ComponentModel.ISupportInitialize)(this.Matr1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Matr2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnswerMatr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView Matr1;
        private System.Windows.Forms.TextBox Matr1N;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Matr1M;
        private System.Windows.Forms.DataGridView Matr2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Matr2M;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Matr2N;
        private System.Windows.Forms.Button MulButton;
        private System.Windows.Forms.Button SubButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DivButton;
        private System.Windows.Forms.DataGridView AnswerMatr;
    }
}

