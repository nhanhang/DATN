namespace DATN
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.bntX = new System.Windows.Forms.Button();
            this.bntCS = new System.Windows.Forms.Button();
            this.bntT = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnChitiet = new System.Windows.Forms.Button();
            this.cboID = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimkiem = new System.Windows.Forms.TextBox();
            this.bntTimkiem = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(265, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(336, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pet management";
            // 
            // dataView
            // 
            this.dataView.AllowUserToAddRows = false;
            this.dataView.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataView.Location = new System.Drawing.Point(16, 175);
            this.dataView.Name = "dataView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataView.RowHeadersWidth = 51;
            this.dataView.RowTemplate.Height = 24;
            this.dataView.Size = new System.Drawing.Size(778, 206);
            this.dataView.TabIndex = 1;
            // 
            // bntX
            // 
            this.bntX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntX.ForeColor = System.Drawing.Color.Red;
            this.bntX.Location = new System.Drawing.Point(653, 74);
            this.bntX.Name = "bntX";
            this.bntX.Size = new System.Drawing.Size(140, 38);
            this.bntX.TabIndex = 2;
            this.bntX.Text = "Pet deletion";
            this.bntX.UseVisualStyleBackColor = true;
            this.bntX.Click += new System.EventHandler(this.bntX_Click);
            // 
            // bntCS
            // 
            this.bntCS.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntCS.ForeColor = System.Drawing.Color.Red;
            this.bntCS.Location = new System.Drawing.Point(489, 74);
            this.bntCS.Name = "bntCS";
            this.bntCS.Size = new System.Drawing.Size(141, 37);
            this.bntCS.TabIndex = 3;
            this.bntCS.Text = "Update";
            this.bntCS.UseVisualStyleBackColor = true;
            this.bntCS.Click += new System.EventHandler(this.bntCS_Click);
            // 
            // bntT
            // 
            this.bntT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntT.ForeColor = System.Drawing.Color.Red;
            this.bntT.Location = new System.Drawing.Point(322, 74);
            this.bntT.Name = "bntT";
            this.bntT.Size = new System.Drawing.Size(141, 38);
            this.bntT.TabIndex = 4;
            this.bntT.Text = "Pet adding";
            this.bntT.UseVisualStyleBackColor = true;
            this.bntT.Click += new System.EventHandler(this.bntT_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "ID:";
            // 
            // btnChitiet
            // 
            this.btnChitiet.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChitiet.ForeColor = System.Drawing.Color.Red;
            this.btnChitiet.Location = new System.Drawing.Point(653, 129);
            this.btnChitiet.Name = "btnChitiet";
            this.btnChitiet.Size = new System.Drawing.Size(140, 38);
            this.btnChitiet.TabIndex = 14;
            this.btnChitiet.Text = "More detail";
            this.btnChitiet.UseVisualStyleBackColor = true;
            this.btnChitiet.Click += new System.EventHandler(this.btnChitiet_Click);
            // 
            // cboID
            // 
            this.cboID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboID.FormattingEnabled = true;
            this.cboID.Location = new System.Drawing.Point(172, 79);
            this.cboID.Name = "cboID";
            this.cboID.Size = new System.Drawing.Size(129, 28);
            this.cboID.TabIndex = 16;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(489, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 38);
            this.button1.TabIndex = 17;
            this.button1.Text = "Reload";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 32);
            this.label2.TabIndex = 18;
            this.label2.Text = "Word find:";
            // 
            // txtTimkiem
            // 
            this.txtTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimkiem.Location = new System.Drawing.Point(172, 132);
            this.txtTimkiem.Name = "txtTimkiem";
            this.txtTimkiem.Size = new System.Drawing.Size(129, 27);
            this.txtTimkiem.TabIndex = 19;
            // 
            // bntTimkiem
            // 
            this.bntTimkiem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntTimkiem.ForeColor = System.Drawing.Color.Red;
            this.bntTimkiem.Location = new System.Drawing.Point(322, 129);
            this.bntTimkiem.Name = "bntTimkiem";
            this.bntTimkiem.Size = new System.Drawing.Size(141, 38);
            this.bntTimkiem.TabIndex = 20;
            this.bntTimkiem.Text = "Search";
            this.bntTimkiem.UseVisualStyleBackColor = true;
            this.bntTimkiem.Click += new System.EventHandler(this.bntTimkiem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::DATN.Properties.Resources.Remove_bg_ai_1719250621814__2_;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(805, 391);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.bntTimkiem);
            this.Controls.Add(this.txtTimkiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cboID);
            this.Controls.Add(this.btnChitiet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bntT);
            this.Controls.Add(this.bntCS);
            this.Controls.Add(this.bntX);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Pet management";
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.Button bntX;
        private System.Windows.Forms.Button bntCS;
        private System.Windows.Forms.Button bntT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnChitiet;
        private System.Windows.Forms.ComboBox cboID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bntTimkiem;
        private System.Windows.Forms.TextBox txtTimkiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

