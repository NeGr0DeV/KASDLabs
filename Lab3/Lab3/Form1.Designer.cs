namespace WindowsFormsApp7
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
            this.components = new System.ComponentModel.Container();
            this.ArrayTypeCombobox = new System.Windows.Forms.ComboBox();
            this.start = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.button3 = new System.Windows.Forms.Button();
            this.GroupCombobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // ArrayTypeCombobox
            // 
            this.ArrayTypeCombobox.FormattingEnabled = true;
            this.ArrayTypeCombobox.Items.AddRange(new object[] {
            "Массив случайных чисел",
            "Разбитые на подмассивы",
            "Массивы с перестановками",
            "Массивы с повторением"});
            this.ArrayTypeCombobox.Location = new System.Drawing.Point(77, 76);
            this.ArrayTypeCombobox.Margin = new System.Windows.Forms.Padding(4);
            this.ArrayTypeCombobox.Name = "ArrayTypeCombobox";
            this.ArrayTypeCombobox.Size = new System.Drawing.Size(160, 24);
            this.ArrayTypeCombobox.TabIndex = 0;
            this.ArrayTypeCombobox.SelectedIndexChanged += new System.EventHandler(this.SelectArrayType);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(265, 261);
            this.start.Margin = new System.Windows.Forms.Padding(4);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(121, 57);
            this.start.TabIndex = 1;
            this.start.Text = "Запустить тесты";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.VisGraph);
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Location = new System.Drawing.Point(412, 15);
            this.zedGraphControl1.Margin = new System.Windows.Forms.Padding(5);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(741, 524);
            this.zedGraphControl1.TabIndex = 2;
            this.zedGraphControl1.UseExtendedPrintDialog = true;
            this.zedGraphControl1.Load += new System.EventHandler(this.zedGraphControl1_Load);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(265, 471);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 53);
            this.button3.TabIndex = 3;
            this.button3.Text = "Сохранить результаты";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Export);
            // 
            // GroupCombobox
            // 
            this.GroupCombobox.FormattingEnabled = true;
            this.GroupCombobox.Items.AddRange(new object[] {
            "Первая группа",
            "Вторая группа",
            "Третья группа"});
            this.GroupCombobox.Location = new System.Drawing.Point(77, 144);
            this.GroupCombobox.Margin = new System.Windows.Forms.Padding(4);
            this.GroupCombobox.Name = "GroupCombobox";
            this.GroupCombobox.Size = new System.Drawing.Size(160, 24);
            this.GroupCombobox.TabIndex = 4;
            this.GroupCombobox.SelectedIndexChanged += new System.EventHandler(this.SelectGroup);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 554);
            this.Controls.Add(this.GroupCombobox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.zedGraphControl1);
            this.Controls.Add(this.start);
            this.Controls.Add(this.ArrayTypeCombobox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ArrayTypeCombobox;
        private System.Windows.Forms.Button start;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox GroupCombobox;
    }
}

