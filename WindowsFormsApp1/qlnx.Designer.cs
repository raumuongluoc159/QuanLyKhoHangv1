namespace WindowsFormsApp1
{
    partial class qlnx
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.inphieunhap = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.soLuong = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.donviBox = new System.Windows.Forms.ComboBox();
            this.hanghoaBox = new System.Windows.Forms.ComboBox();
            this.nhanvienBox = new System.Windows.Forms.ComboBox();
            this.dvtBox = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(287, 466);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "Tạo Phiếu Nhập";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tên Hàng Hóa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(414, 302);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tên NV";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 500);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 18);
            this.label7.TabIndex = 12;
            // 
            // inphieunhap
            // 
            this.inphieunhap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.inphieunhap.Location = new System.Drawing.Point(560, 466);
            this.inphieunhap.Name = "inphieunhap";
            this.inphieunhap.Size = new System.Drawing.Size(105, 42);
            this.inphieunhap.TabIndex = 14;
            this.inphieunhap.Text = "In Phiếu Nhập";
            this.inphieunhap.UseVisualStyleBackColor = true;
            this.inphieunhap.Click += new System.EventHandler(this.inphieunhap_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 268);
            this.panel1.TabIndex = 15;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(976, 268);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // soLuong
            // 
            this.soLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.soLuong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soLuong.Location = new System.Drawing.Point(805, 299);
            this.soLuong.Name = "soLuong";
            this.soLuong.Size = new System.Drawing.Size(96, 27);
            this.soLuong.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(705, 302);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "Số Lượng";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(414, 366);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "Đơn Vị Tính";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 303);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 18);
            this.label1.TabIndex = 22;
            this.label1.Text = "Đơn vị";
            // 
            // donviBox
            // 
            this.donviBox.FormattingEnabled = true;
            this.donviBox.Location = new System.Drawing.Point(230, 303);
            this.donviBox.Name = "donviBox";
            this.donviBox.Size = new System.Drawing.Size(162, 21);
            this.donviBox.TabIndex = 23;
            this.donviBox.DropDown += new System.EventHandler(this.donviBox_DropDown);
            // 
            // hanghoaBox
            // 
            this.hanghoaBox.FormattingEnabled = true;
            this.hanghoaBox.Location = new System.Drawing.Point(230, 367);
            this.hanghoaBox.Name = "hanghoaBox";
            this.hanghoaBox.Size = new System.Drawing.Size(162, 21);
            this.hanghoaBox.TabIndex = 24;
            this.hanghoaBox.DropDown += new System.EventHandler(this.hanghoaBox_DropDown);
            // 
            // nhanvienBox
            // 
            this.nhanvienBox.FormattingEnabled = true;
            this.nhanvienBox.Location = new System.Drawing.Point(499, 302);
            this.nhanvienBox.Name = "nhanvienBox";
            this.nhanvienBox.Size = new System.Drawing.Size(166, 21);
            this.nhanvienBox.TabIndex = 25;
            this.nhanvienBox.DropDown += new System.EventHandler(this.nhanvienBox_DropDown);
            // 
            // dvtBox
            // 
            this.dvtBox.FormattingEnabled = true;
            this.dvtBox.Location = new System.Drawing.Point(503, 367);
            this.dvtBox.Name = "dvtBox";
            this.dvtBox.Size = new System.Drawing.Size(162, 21);
            this.dvtBox.TabIndex = 26;
            this.dvtBox.DropDown += new System.EventHandler(this.dvtBox_DropDown);
            // 
            // qlnx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 563);
            this.Controls.Add(this.dvtBox);
            this.Controls.Add(this.nhanvienBox);
            this.Controls.Add(this.hanghoaBox);
            this.Controls.Add(this.donviBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.inphieunhap);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.soLuong);
            this.Controls.Add(this.button1);
            this.Name = "qlnx";
            this.Text = "qlnx";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button inphieunhap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TextBox soLuong;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox donviBox;
        private System.Windows.Forms.ComboBox hanghoaBox;
        private System.Windows.Forms.ComboBox nhanvienBox;
        private System.Windows.Forms.ComboBox dvtBox;
    }
}