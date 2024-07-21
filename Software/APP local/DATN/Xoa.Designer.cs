namespace DATN
{
    partial class Xoa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Xoa));
            this.bntXoa = new System.Windows.Forms.Button();
            this.cboID = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bntXem = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // bntXoa
            // 
            this.bntXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntXoa.ForeColor = System.Drawing.Color.Red;
            this.bntXoa.Location = new System.Drawing.Point(82, 134);
            this.bntXoa.Name = "bntXoa";
            this.bntXoa.Size = new System.Drawing.Size(124, 37);
            this.bntXoa.TabIndex = 0;
            this.bntXoa.Text = "Delete";
            this.bntXoa.UseVisualStyleBackColor = true;
            this.bntXoa.Click += new System.EventHandler(this.bntXoa_Click);
            // 
            // cboID
            // 
            this.cboID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboID.FormattingEnabled = true;
            this.cboID.Location = new System.Drawing.Point(186, 82);
            this.cboID.Name = "cboID";
            this.cboID.Size = new System.Drawing.Size(159, 28);
            this.cboID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 29);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID:";
            // 
            // bntXem
            // 
            this.bntXem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntXem.ForeColor = System.Drawing.Color.Red;
            this.bntXem.Location = new System.Drawing.Point(212, 134);
            this.bntXem.Name = "bntXem";
            this.bntXem.Size = new System.Drawing.Size(133, 37);
            this.bntXem.TabIndex = 3;
            this.bntXem.Text = "Details";
            this.bntXem.UseVisualStyleBackColor = true;
            this.bntXem.Click += new System.EventHandler(this.bntXem_Click);
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
            this.label5.Location = new System.Drawing.Point(121, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 38);
            this.label5.TabIndex = 28;
            this.label5.Text = "Pet deletion";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = global::DATN.Properties.Resources.Remove_bg_ai_1719250621814__2_;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // Xoa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(434, 183);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bntXem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboID);
            this.Controls.Add(this.bntXoa);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Xoa";
            this.Text = "Pet management";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bntXoa;
        private System.Windows.Forms.ComboBox cboID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bntXem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}