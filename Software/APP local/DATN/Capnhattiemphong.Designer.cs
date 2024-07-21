namespace DATN
{
    partial class Capnhattiemphong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Capnhattiemphong));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboID = new System.Windows.Forms.ComboBox();
            this.cbtCapnhat = new System.Windows.Forms.Button();
            this.txtGhichu = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTP = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVaccine = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataView = new System.Windows.Forms.DataGridView();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboID);
            this.groupBox1.Controls.Add(this.cbtCapnhat);
            this.groupBox1.Controls.Add(this.txtGhichu);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpTP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVaccine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(28, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(707, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Updated information";
            // 
            // cboID
            // 
            this.cboID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboID.FormattingEnabled = true;
            this.cboID.Location = new System.Drawing.Point(23, 71);
            this.cboID.Name = "cboID";
            this.cboID.Size = new System.Drawing.Size(141, 28);
            this.cboID.TabIndex = 9;
            // 
            // cbtCapnhat
            // 
            this.cbtCapnhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbtCapnhat.ForeColor = System.Drawing.Color.Red;
            this.cbtCapnhat.Location = new System.Drawing.Point(535, 83);
            this.cbtCapnhat.Name = "cbtCapnhat";
            this.cbtCapnhat.Size = new System.Drawing.Size(147, 50);
            this.cbtCapnhat.TabIndex = 8;
            this.cbtCapnhat.Text = "Update";
            this.cbtCapnhat.UseVisualStyleBackColor = true;
            this.cbtCapnhat.Click += new System.EventHandler(this.bntCapnhat_Click);
            // 
            // txtGhichu
            // 
            this.txtGhichu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGhichu.Location = new System.Drawing.Point(298, 160);
            this.txtGhichu.Name = "txtGhichu";
            this.txtGhichu.Size = new System.Drawing.Size(172, 27);
            this.txtGhichu.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(293, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Note";
            // 
            // dtpTP
            // 
            this.dtpTP.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTP.CustomFormat = "dd/MM/yyyy";
            this.dtpTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTP.Location = new System.Drawing.Point(298, 69);
            this.dtpTP.Name = "dtpTP";
            this.dtpTP.ShowUpDown = true;
            this.dtpTP.Size = new System.Drawing.Size(172, 27);
            this.dtpTP.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(293, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vaccination time";
            // 
            // txtVaccine
            // 
            this.txtVaccine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVaccine.Location = new System.Drawing.Point(23, 160);
            this.txtVaccine.Name = "txtVaccine";
            this.txtVaccine.Size = new System.Drawing.Size(141, 27);
            this.txtVaccine.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(18, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Vaccine type";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(18, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // dataView
            // 
            this.dataView.AllowUserToAddRows = false;
            this.dataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataView.Location = new System.Drawing.Point(28, 271);
            this.dataView.Name = "dataView";
            this.dataView.RowHeadersWidth = 51;
            this.dataView.RowTemplate.Height = 24;
            this.dataView.Size = new System.Drawing.Size(707, 194);
            this.dataView.TabIndex = 1;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(211, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(336, 38);
            this.label5.TabIndex = 28;
            this.label5.Text = "Vaccination Updates";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DATN.Properties.Resources.Remove_bg_ai_1719250621814__2_;
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // Capnhattiemphong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(747, 477);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataView);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Capnhattiemphong";
            this.Text = "Pet management";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataView;
        private System.Windows.Forms.DateTimePicker dtpTP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVaccine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGhichu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cbtCapnhat;
        private System.Windows.Forms.ComboBox cboID;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}