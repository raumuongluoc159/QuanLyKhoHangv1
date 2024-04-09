namespace WindowsFormsApp1
{
    partial class frmThemPhieuXuat
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dvtBox = new System.Windows.Forms.ComboBox();
            this.hanghoaBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtsoLuong = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(719, 342);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(159, 23);
            this.simpleButton1.TabIndex = 49;
            this.simpleButton1.Text = "Thêm sản phẩm";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // dvtBox
            // 
            this.dvtBox.FormattingEnabled = true;
            this.dvtBox.Location = new System.Drawing.Point(495, 282);
            this.dvtBox.Name = "dvtBox";
            this.dvtBox.Size = new System.Drawing.Size(162, 21);
            this.dvtBox.TabIndex = 48;
            this.dvtBox.DropDown += new System.EventHandler(this.dvtBox_DropDown);
            // 
            // hanghoaBox
            // 
            this.hanghoaBox.FormattingEnabled = true;
            this.hanghoaBox.Location = new System.Drawing.Point(222, 282);
            this.hanghoaBox.Name = "hanghoaBox";
            this.hanghoaBox.Size = new System.Drawing.Size(162, 21);
            this.hanghoaBox.TabIndex = 47;
            this.hanghoaBox.DropDown += new System.EventHandler(this.hanghoaBox_DropDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(406, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 18);
            this.label6.TabIndex = 46;
            this.label6.Text = "Đơn Vị Tính";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(701, 283);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 18);
            this.label8.TabIndex = 45;
            this.label8.Text = "Số Lượng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(60, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 18);
            this.label2.TabIndex = 44;
            this.label2.Text = "Tên Hàng Hóa";
            // 
            // txtsoLuong
            // 
            this.txtsoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtsoLuong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsoLuong.Location = new System.Drawing.Point(801, 280);
            this.txtsoLuong.Name = "txtsoLuong";
            this.txtsoLuong.Size = new System.Drawing.Size(96, 27);
            this.txtsoLuong.TabIndex = 43;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(921, 186);
            this.listView1.TabIndex = 39;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(222, 342);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(159, 23);
            this.simpleButton3.TabIndex = 52;
            this.simpleButton3.Text = "Tạo Phiếu";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(477, 342);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(159, 23);
            this.simpleButton2.TabIndex = 51;
            this.simpleButton2.Text = "Hủy Phiếu";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Location = new System.Drawing.Point(33, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 186);
            this.panel1.TabIndex = 50;
            // 
            // frmThemPhieuXuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 410);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.dvtBox);
            this.Controls.Add(this.hanghoaBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtsoLuong);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.panel1);
            this.Name = "frmThemPhieuXuat";
            this.Text = "frmThemPhieuXuat";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.ComboBox dvtBox;
        private System.Windows.Forms.ComboBox hanghoaBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtsoLuong;
        private System.Windows.Forms.ListView listView1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Panel panel1;
    }
}