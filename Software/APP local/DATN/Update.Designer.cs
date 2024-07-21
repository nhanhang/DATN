namespace DATN
{
    partial class Update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Update));
            this.dataView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboDCT = new System.Windows.Forms.ComboBox();
            this.bntChonanh = new System.Windows.Forms.Button();
            this.cboTT = new System.Windows.Forms.ComboBox();
            this.dtpNT = new System.Windows.Forms.DateTimePicker();
            this.txtChu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVatnuoi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVatnuoi = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChu = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bntTiemphong = new System.Windows.Forms.Button();
            this.bntUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboID = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataView1
            // 
            this.dataView1.AllowUserToAddRows = false;
            this.dataView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView1.Location = new System.Drawing.Point(10, 302);
            this.dataView1.Name = "dataView1";
            this.dataView1.RowHeadersWidth = 51;
            this.dataView1.RowTemplate.Height = 24;
            this.dataView1.Size = new System.Drawing.Size(761, 227);
            this.dataView1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboDCT);
            this.groupBox1.Controls.Add(this.bntChonanh);
            this.groupBox1.Controls.Add(this.cboTT);
            this.groupBox1.Controls.Add(this.dtpNT);
            this.groupBox1.Controls.Add(this.txtChu);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtVatnuoi);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboVatnuoi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboChu);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(267, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(504, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Update information";
            // 
            // cboDCT
            // 
            this.cboDCT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDCT.ForeColor = System.Drawing.Color.Black;
            this.cboDCT.FormattingEnabled = true;
            this.cboDCT.Location = new System.Drawing.Point(286, 150);
            this.cboDCT.Name = "cboDCT";
            this.cboDCT.Size = new System.Drawing.Size(182, 28);
            this.cboDCT.TabIndex = 17;
            this.cboDCT.Visible = false;
            // 
            // bntChonanh
            // 
            this.bntChonanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntChonanh.ForeColor = System.Drawing.Color.Black;
            this.bntChonanh.Location = new System.Drawing.Point(205, 151);
            this.bntChonanh.Name = "bntChonanh";
            this.bntChonanh.Size = new System.Drawing.Size(40, 27);
            this.bntChonanh.TabIndex = 16;
            this.bntChonanh.Text = "...";
            this.bntChonanh.UseVisualStyleBackColor = true;
            this.bntChonanh.Visible = false;
            this.bntChonanh.Click += new System.EventHandler(this.bntChonanh_Click);
            // 
            // cboTT
            // 
            this.cboTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTT.ForeColor = System.Drawing.Color.Black;
            this.cboTT.FormattingEnabled = true;
            this.cboTT.Location = new System.Drawing.Point(25, 150);
            this.cboTT.Name = "cboTT";
            this.cboTT.Size = new System.Drawing.Size(174, 28);
            this.cboTT.TabIndex = 15;
            this.cboTT.Visible = false;
            // 
            // dtpNT
            // 
            this.dtpNT.CustomFormat = "dd/MM/yyyy";
            this.dtpNT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNT.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNT.Location = new System.Drawing.Point(25, 151);
            this.dtpNT.Name = "dtpNT";
            this.dtpNT.Size = new System.Drawing.Size(174, 27);
            this.dtpNT.TabIndex = 14;
            this.dtpNT.Visible = false;
            // 
            // txtChu
            // 
            this.txtChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChu.Location = new System.Drawing.Point(286, 151);
            this.txtChu.Name = "txtChu";
            this.txtChu.Size = new System.Drawing.Size(170, 27);
            this.txtChu.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(282, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(194, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Owner\'s new information";
            // 
            // txtVatnuoi
            // 
            this.txtVatnuoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVatnuoi.Location = new System.Drawing.Point(25, 151);
            this.txtVatnuoi.Name = "txtVatnuoi";
            this.txtVatnuoi.Size = new System.Drawing.Size(170, 27);
            this.txtVatnuoi.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(21, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(170, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Pet\'s new information";
            // 
            // cboVatnuoi
            // 
            this.cboVatnuoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboVatnuoi.ForeColor = System.Drawing.Color.Black;
            this.cboVatnuoi.FormattingEnabled = true;
            this.cboVatnuoi.Location = new System.Drawing.Point(25, 66);
            this.cboVatnuoi.Name = "cboVatnuoi";
            this.cboVatnuoi.Size = new System.Drawing.Size(170, 28);
            this.cboVatnuoi.TabIndex = 6;
            this.cboVatnuoi.SelectedIndexChanged += new System.EventHandler(this.cboVatnuoi_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(282, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(182, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Owner information type";
            // 
            // cboChu
            // 
            this.cboChu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboChu.ForeColor = System.Drawing.Color.Black;
            this.cboChu.FormattingEnabled = true;
            this.cboChu.Location = new System.Drawing.Point(286, 66);
            this.cboChu.Name = "cboChu";
            this.cboChu.Size = new System.Drawing.Size(182, 28);
            this.cboChu.TabIndex = 4;
            this.cboChu.SelectedIndexChanged += new System.EventHandler(this.cboChu_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(21, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pet information type";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sqLiteCommand1
            // 
            this.sqLiteCommand1.CommandText = null;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bntTiemphong);
            this.groupBox3.Controls.Add(this.bntUpdate);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cboID);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Blue;
            this.groupBox3.Location = new System.Drawing.Point(10, 101);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(251, 195);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Updating type";
            // 
            // bntTiemphong
            // 
            this.bntTiemphong.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntTiemphong.ForeColor = System.Drawing.Color.Red;
            this.bntTiemphong.Location = new System.Drawing.Point(35, 140);
            this.bntTiemphong.Name = "bntTiemphong";
            this.bntTiemphong.Size = new System.Drawing.Size(175, 36);
            this.bntTiemphong.TabIndex = 18;
            this.bntTiemphong.Text = "Update vaccination";
            this.bntTiemphong.UseVisualStyleBackColor = true;
            this.bntTiemphong.Click += new System.EventHandler(this.bntTiemphong_Click);
            // 
            // bntUpdate
            // 
            this.bntUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntUpdate.ForeColor = System.Drawing.Color.Red;
            this.bntUpdate.Location = new System.Drawing.Point(35, 94);
            this.bntUpdate.Name = "bntUpdate";
            this.bntUpdate.Size = new System.Drawing.Size(175, 38);
            this.bntUpdate.TabIndex = 23;
            this.bntUpdate.Text = "Update information";
            this.bntUpdate.UseVisualStyleBackColor = true;
            this.bntUpdate.Click += new System.EventHandler(this.bntUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(31, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 20);
            this.label1.TabIndex = 22;
            this.label1.Text = "ID:";
            // 
            // cboID
            // 
            this.cboID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboID.FormattingEnabled = true;
            this.cboID.Location = new System.Drawing.Point(35, 58);
            this.cboID.Name = "cboID";
            this.cboID.Size = new System.Drawing.Size(175, 28);
            this.cboID.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(245, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(331, 38);
            this.label6.TabIndex = 22;
            this.label6.Text = "Information updating";
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::DATN.Properties.Resources.Remove_bg_ai_1719250621814__2_;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(783, 541);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataView1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Update";
            this.Text = "Pet management";
            ((System.ComponentModel.ISupportInitialize)(this.dataView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboVatnuoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVatnuoi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpNT;
        private System.Windows.Forms.ComboBox cboTT;
        private System.Windows.Forms.Button bntChonanh;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cboDCT;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bntUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboID;
        private System.Windows.Forms.Button bntTiemphong;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}