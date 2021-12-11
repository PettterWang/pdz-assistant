
namespace pdz助手
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox_主面板 = new System.Windows.Forms.GroupBox();
            this.checkBox_不存储png = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_pdf存储目录 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_png存储目录 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_总页数 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_一键转换 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_后台 = new System.Windows.Forms.RadioButton();
            this.radioButton_前台 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_常规 = new System.Windows.Forms.RadioButton();
            this.radioButton_最大化 = new System.Windows.Forms.RadioButton();
            this.radioButton_不存在 = new System.Windows.Forms.RadioButton();
            this.radioButton_最小化 = new System.Windows.Forms.RadioButton();
            this.button_查看缓存器 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label_气泡 = new System.Windows.Forms.Label();
            this.进程扫描器 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label_ssReader状态 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_当前系统用户名 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_进度 = new System.Windows.Forms.Label();
            this.progressBar_进度条 = new System.Windows.Forms.ProgressBar();
            this.气泡控制器 = new System.Windows.Forms.Timer(this.components);
            this.最大化控制器 = new System.Windows.Forms.Timer(this.components);
            this.groupBox_主面板.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_主面板
            // 
            this.groupBox_主面板.Controls.Add(this.checkBox_不存储png);
            this.groupBox_主面板.Controls.Add(this.groupBox6);
            this.groupBox_主面板.Controls.Add(this.textBox_pdf存储目录);
            this.groupBox_主面板.Controls.Add(this.label9);
            this.groupBox_主面板.Controls.Add(this.textBox_png存储目录);
            this.groupBox_主面板.Controls.Add(this.label8);
            this.groupBox_主面板.Controls.Add(this.textBox_总页数);
            this.groupBox_主面板.Controls.Add(this.label5);
            this.groupBox_主面板.Controls.Add(this.button_一键转换);
            this.groupBox_主面板.Controls.Add(this.groupBox3);
            this.groupBox_主面板.Controls.Add(this.button_查看缓存器);
            this.groupBox_主面板.Controls.Add(this.button1);
            this.groupBox_主面板.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_主面板.Location = new System.Drawing.Point(4, 4);
            this.groupBox_主面板.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox_主面板.Name = "groupBox_主面板";
            this.groupBox_主面板.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox_主面板.Size = new System.Drawing.Size(761, 360);
            this.groupBox_主面板.TabIndex = 0;
            this.groupBox_主面板.TabStop = false;
            // 
            // checkBox_不存储png
            // 
            this.checkBox_不存储png.AutoSize = true;
            this.checkBox_不存储png.Location = new System.Drawing.Point(475, 162);
            this.checkBox_不存储png.Name = "checkBox_不存储png";
            this.checkBox_不存储png.Size = new System.Drawing.Size(99, 20);
            this.checkBox_不存储png.TabIndex = 15;
            this.checkBox_不存储png.Text = "不存储png";
            this.checkBox_不存储png.UseVisualStyleBackColor = true;
            this.checkBox_不存储png.CheckedChanged += new System.EventHandler(this.checkBox_不存储png_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Location = new System.Drawing.Point(8, 242);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(745, 111);
            this.groupBox6.TabIndex = 14;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "使用说明";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(544, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "[3]最后在ssReader设置合适的[页面大小]，然后点击上方[一键转换]按钮。\r\n";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(456, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "[1]使用ssReader打开你要转换的pdz文件，并进入[阅读模式]。\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(304, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "[2]确保ssReader窗口状态处于[最大化]。\r\n";
            // 
            // textBox_pdf存储目录
            // 
            this.textBox_pdf存储目录.Location = new System.Drawing.Point(8, 211);
            this.textBox_pdf存储目录.Name = "textBox_pdf存储目录";
            this.textBox_pdf存储目录.Size = new System.Drawing.Size(461, 25);
            this.textBox_pdf存储目录.TabIndex = 13;
            this.textBox_pdf存储目录.Text = "C:\\Users\\chord\\Desktop\\1.pdf";
            this.textBox_pdf存储目录.TextChanged += new System.EventHandler(this.textBox_pdf存储目录_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(208, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "设置pdf存储目录、文件名：";
            // 
            // textBox_png存储目录
            // 
            this.textBox_png存储目录.Location = new System.Drawing.Point(8, 160);
            this.textBox_png存储目录.Name = "textBox_png存储目录";
            this.textBox_png存储目录.Size = new System.Drawing.Size(461, 25);
            this.textBox_png存储目录.TabIndex = 11;
            this.textBox_png存储目录.Text = "C:\\Users\\chord\\Desktop\\test";
            this.textBox_png存储目录.TextChanged += new System.EventHandler(this.textBox_png存储目录_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(5, 138);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "设置png存储目录：";
            // 
            // textBox_总页数
            // 
            this.textBox_总页数.Enabled = false;
            this.textBox_总页数.Location = new System.Drawing.Point(8, 105);
            this.textBox_总页数.Name = "textBox_总页数";
            this.textBox_总页数.Size = new System.Drawing.Size(121, 25);
            this.textBox_总页数.TabIndex = 7;
            this.textBox_总页数.Text = "1";
            this.textBox_总页数.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_总页数_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(304, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "总页数(总页数=前言页+目录页+正文页)：";
            // 
            // button_一键转换
            // 
            this.button_一键转换.Location = new System.Drawing.Point(120, 26);
            this.button_一键转换.Name = "button_一键转换";
            this.button_一键转换.Size = new System.Drawing.Size(101, 42);
            this.button_一键转换.TabIndex = 4;
            this.button_一键转换.Text = " 一键转换";
            this.button_一键转换.UseVisualStyleBackColor = true;
            this.button_一键转换.Click += new System.EventHandler(this.button_一键转换_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(577, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 166);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ssReader窗口状态";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_后台);
            this.groupBox5.Controls.Add(this.radioButton_前台);
            this.groupBox5.Location = new System.Drawing.Point(98, 24);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(70, 101);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // radioButton_后台
            // 
            this.radioButton_后台.AutoSize = true;
            this.radioButton_后台.Enabled = false;
            this.radioButton_后台.Location = new System.Drawing.Point(6, 44);
            this.radioButton_后台.Name = "radioButton_后台";
            this.radioButton_后台.Size = new System.Drawing.Size(58, 20);
            this.radioButton_后台.TabIndex = 1;
            this.radioButton_后台.TabStop = true;
            this.radioButton_后台.Text = "后台";
            this.radioButton_后台.UseVisualStyleBackColor = true;
            // 
            // radioButton_前台
            // 
            this.radioButton_前台.AutoSize = true;
            this.radioButton_前台.Enabled = false;
            this.radioButton_前台.Location = new System.Drawing.Point(6, 18);
            this.radioButton_前台.Name = "radioButton_前台";
            this.radioButton_前台.Size = new System.Drawing.Size(58, 20);
            this.radioButton_前台.TabIndex = 0;
            this.radioButton_前台.TabStop = true;
            this.radioButton_前台.Text = "前台";
            this.radioButton_前台.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_常规);
            this.groupBox4.Controls.Add(this.radioButton_最大化);
            this.groupBox4.Controls.Add(this.radioButton_不存在);
            this.groupBox4.Controls.Add(this.radioButton_最小化);
            this.groupBox4.Location = new System.Drawing.Point(6, 24);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(86, 129);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // radioButton_常规
            // 
            this.radioButton_常规.AutoSize = true;
            this.radioButton_常规.Enabled = false;
            this.radioButton_常规.Location = new System.Drawing.Point(6, 18);
            this.radioButton_常规.Name = "radioButton_常规";
            this.radioButton_常规.Size = new System.Drawing.Size(58, 20);
            this.radioButton_常规.TabIndex = 5;
            this.radioButton_常规.Text = "常规";
            this.radioButton_常规.UseVisualStyleBackColor = true;
            // 
            // radioButton_最大化
            // 
            this.radioButton_最大化.AutoSize = true;
            this.radioButton_最大化.Enabled = false;
            this.radioButton_最大化.Location = new System.Drawing.Point(6, 44);
            this.radioButton_最大化.Name = "radioButton_最大化";
            this.radioButton_最大化.Size = new System.Drawing.Size(74, 20);
            this.radioButton_最大化.TabIndex = 4;
            this.radioButton_最大化.Text = "最大化";
            this.radioButton_最大化.UseVisualStyleBackColor = true;
            // 
            // radioButton_不存在
            // 
            this.radioButton_不存在.AutoSize = true;
            this.radioButton_不存在.Enabled = false;
            this.radioButton_不存在.Location = new System.Drawing.Point(6, 96);
            this.radioButton_不存在.Name = "radioButton_不存在";
            this.radioButton_不存在.Size = new System.Drawing.Size(74, 20);
            this.radioButton_不存在.TabIndex = 3;
            this.radioButton_不存在.Text = "不存在";
            this.radioButton_不存在.UseVisualStyleBackColor = true;
            // 
            // radioButton_最小化
            // 
            this.radioButton_最小化.AutoSize = true;
            this.radioButton_最小化.Enabled = false;
            this.radioButton_最小化.Location = new System.Drawing.Point(6, 70);
            this.radioButton_最小化.Name = "radioButton_最小化";
            this.radioButton_最小化.Size = new System.Drawing.Size(74, 20);
            this.radioButton_最小化.TabIndex = 2;
            this.radioButton_最小化.Text = "最小化";
            this.radioButton_最小化.UseVisualStyleBackColor = true;
            // 
            // button_查看缓存器
            // 
            this.button_查看缓存器.Location = new System.Drawing.Point(8, 25);
            this.button_查看缓存器.Name = "button_查看缓存器";
            this.button_查看缓存器.Size = new System.Drawing.Size(106, 43);
            this.button_查看缓存器.TabIndex = 1;
            this.button_查看缓存器.Text = "查看缓冲器";
            this.button_查看缓存器.UseVisualStyleBackColor = true;
            this.button_查看缓存器.Click += new System.EventHandler(this.button_查看缓存器_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(227, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label_气泡
            // 
            this.label_气泡.AutoSize = true;
            this.label_气泡.Location = new System.Drawing.Point(6, 14);
            this.label_气泡.Name = "label_气泡";
            this.label_气泡.Size = new System.Drawing.Size(32, 16);
            this.label_气泡.TabIndex = 4;
            this.label_气泡.Text = "[*]";
            // 
            // 进程扫描器
            // 
            this.进程扫描器.Enabled = true;
            this.进程扫描器.Tick += new System.EventHandler(this.进程扫描器_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(599, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ssReader状态：";
            // 
            // label_ssReader状态
            // 
            this.label_ssReader状态.AutoSize = true;
            this.label_ssReader状态.ForeColor = System.Drawing.Color.Red;
            this.label_ssReader状态.Location = new System.Drawing.Point(717, 17);
            this.label_ssReader状态.Name = "label_ssReader状态";
            this.label_ssReader状态.Size = new System.Drawing.Size(24, 16);
            this.label_ssReader状态.TabIndex = 1;
            this.label_ssReader状态.Text = "关";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox_主面板, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.04255F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(769, 500);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_气泡);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 371);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 36);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_当前系统用户名);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label_ssReader状态);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 453);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(763, 44);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // label_当前系统用户名
            // 
            this.label_当前系统用户名.AutoSize = true;
            this.label_当前系统用户名.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label_当前系统用户名.Location = new System.Drawing.Point(108, 17);
            this.label_当前系统用户名.Name = "label_当前系统用户名";
            this.label_当前系统用户名.Size = new System.Drawing.Size(40, 16);
            this.label_当前系统用户名.TabIndex = 3;
            this.label_当前系统用户名.Text = "null";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "当前用户名:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_进度);
            this.panel1.Controls.Add(this.progressBar_进度条);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 413);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(763, 34);
            this.panel1.TabIndex = 2;
            // 
            // label_进度
            // 
            this.label_进度.Location = new System.Drawing.Point(699, 9);
            this.label_进度.Name = "label_进度";
            this.label_进度.Size = new System.Drawing.Size(47, 16);
            this.label_进度.TabIndex = 1;
            this.label_进度.Text = "0%";
            this.label_进度.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar_进度条
            // 
            this.progressBar_进度条.Location = new System.Drawing.Point(0, 5);
            this.progressBar_进度条.Name = "progressBar_进度条";
            this.progressBar_进度条.Size = new System.Drawing.Size(693, 23);
            this.progressBar_进度条.TabIndex = 0;
            // 
            // 气泡控制器
            // 
            this.气泡控制器.Interval = 3000;
            this.气泡控制器.Tick += new System.EventHandler(this.气泡控制器_Tick);
            // 
            // 最大化控制器
            // 
            this.最大化控制器.Interval = 10;
            this.最大化控制器.Tick += new System.EventHandler(this.最大化控制器_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 500);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("幼圆", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pdz助手(pdz to pdf) by 千秋 20211211";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox_主面板.ResumeLayout(false);
            this.groupBox_主面板.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_主面板;
        private System.Windows.Forms.Timer 进程扫描器;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_ssReader状态;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_查看缓存器;
        private System.Windows.Forms.Label label_当前系统用户名;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton_最小化;
        private System.Windows.Forms.RadioButton radioButton_后台;
        private System.Windows.Forms.RadioButton radioButton_前台;
        private System.Windows.Forms.RadioButton radioButton_不存在;
        private System.Windows.Forms.RadioButton radioButton_最大化;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_常规;
        private System.Windows.Forms.Label label_气泡;
        private System.Windows.Forms.Timer 气泡控制器;
        private System.Windows.Forms.Button button_一键转换;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_总页数;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_png存储目录;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_pdf存储目录;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_进度;
        private System.Windows.Forms.ProgressBar progressBar_进度条;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox_不存储png;
        private System.Windows.Forms.Timer 最大化控制器;
    }
}

