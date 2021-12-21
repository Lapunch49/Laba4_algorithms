
namespace Laba4_algorithms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Matrix = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_no_cycles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_aboutPr = new System.Windows.Forms.Button();
            this.btn_task = new System.Windows.Forms.Button();
            this.btn_del_vert = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Matrix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Matrix
            // 
            this.Matrix.BackgroundColor = System.Drawing.Color.LightBlue;
            this.Matrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Matrix.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.Matrix.Location = new System.Drawing.Point(12, 320);
            this.Matrix.Name = "Matrix";
            this.Matrix.RowHeadersWidth = 70;
            this.Matrix.RowTemplate.Height = 29;
            this.Matrix.Size = new System.Drawing.Size(576, 201);
            this.Matrix.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "1";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(742, 289);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_clear.Location = new System.Drawing.Point(594, 320);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(160, 36);
            this.btn_clear.TabIndex = 2;
            this.btn_clear.Text = "Clear";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_no_cycles
            // 
            this.btn_no_cycles.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_no_cycles.Location = new System.Drawing.Point(594, 460);
            this.btn_no_cycles.Name = "btn_no_cycles";
            this.btn_no_cycles.Size = new System.Drawing.Size(160, 61);
            this.btn_no_cycles.TabIndex = 3;
            this.btn_no_cycles.Text = "Acyclicity check";
            this.btn_no_cycles.UseVisualStyleBackColor = false;
            this.btn_no_cycles.Click += new System.EventHandler(this.btn_no_cycles_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(592, 359);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 98);
            this.label1.TabIndex = 4;
            // 
            // btn_aboutPr
            // 
            this.btn_aboutPr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aboutPr.Location = new System.Drawing.Point(12, 528);
            this.btn_aboutPr.Name = "btn_aboutPr";
            this.btn_aboutPr.Size = new System.Drawing.Size(285, 29);
            this.btn_aboutPr.TabIndex = 5;
            this.btn_aboutPr.Text = "About program";
            this.btn_aboutPr.UseVisualStyleBackColor = true;
            this.btn_aboutPr.Click += new System.EventHandler(this.btn_aboutPr_Click);
            // 
            // btn_task
            // 
            this.btn_task.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_task.Location = new System.Drawing.Point(303, 528);
            this.btn_task.Name = "btn_task";
            this.btn_task.Size = new System.Drawing.Size(285, 29);
            this.btn_task.TabIndex = 6;
            this.btn_task.Text = "Task for work";
            this.btn_task.UseVisualStyleBackColor = true;
            this.btn_task.Click += new System.EventHandler(this.btn_task_Click);
            // 
            // btn_del_vert
            // 
            this.btn_del_vert.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_del_vert.Location = new System.Drawing.Point(594, 528);
            this.btn_del_vert.Name = "btn_del_vert";
            this.btn_del_vert.Size = new System.Drawing.Size(160, 29);
            this.btn_del_vert.TabIndex = 7;
            this.btn_del_vert.Text = "Delete vertex";
            this.btn_del_vert.UseVisualStyleBackColor = false;
            this.btn_del_vert.Visible = false;
            this.btn_del_vert.Click += new System.EventHandler(this.btn_del_vert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(763, 563);
            this.Controls.Add(this.btn_del_vert);
            this.Controls.Add(this.btn_task);
            this.Controls.Add(this.btn_aboutPr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_no_cycles);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Matrix);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.Matrix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Matrix;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button btn_no_cycles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_aboutPr;
        private System.Windows.Forms.Button btn_task;
        private System.Windows.Forms.Button btn_del_vert;
    }
}

