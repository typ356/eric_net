namespace netcfg
{
    partial class netForm_Eth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(netForm_Eth));
            this.label1 = new System.Windows.Forms.Label();
            this.edIP = new System.Windows.Forms.TextBox();
            this.edMask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edGaway = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.edBord = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.edDomain = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.edPort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.edheattime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.edheatdata = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edregdata = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.edTimeout = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.edLink = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmIPmode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cmProtocol = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cmRegMode = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.edlocalport = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.eddnsBack = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.eddns = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.cmhbType = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.cmObj = new System.Windows.Forms.ComboBox();
            this.ck_reg = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel_client = new System.Windows.Forms.Panel();
            this.ck_heath = new System.Windows.Forms.CheckBox();
            this.panel_baudRate = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel_client.SuspendLayout();
            this.panel_baudRate.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "本地静态IP地址";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edIP
            // 
            this.edIP.Location = new System.Drawing.Point(134, 40);
            this.edIP.Name = "edIP";
            this.edIP.Size = new System.Drawing.Size(161, 21);
            this.edIP.TabIndex = 1;
            // 
            // edMask
            // 
            this.edMask.Location = new System.Drawing.Point(134, 67);
            this.edMask.Name = "edMask";
            this.edMask.Size = new System.Drawing.Size(161, 21);
            this.edMask.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "子网掩码";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edGaway
            // 
            this.edGaway.Location = new System.Drawing.Point(134, 94);
            this.edGaway.Name = "edGaway";
            this.edGaway.Size = new System.Drawing.Size(161, 21);
            this.edGaway.TabIndex = 5;
            this.edGaway.TextChanged += new System.EventHandler(this.edGaway_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(35, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "静态网关";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edBord
            // 
            this.edBord.Location = new System.Drawing.Point(101, 5);
            this.edBord.Name = "edBord";
            this.edBord.Size = new System.Drawing.Size(109, 21);
            this.edBord.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "串口波特率";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edDomain
            // 
            this.edDomain.Location = new System.Drawing.Point(134, 160);
            this.edDomain.Name = "edDomain";
            this.edDomain.Size = new System.Drawing.Size(161, 21);
            this.edDomain.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(11, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "目标IP/域名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edPort
            // 
            this.edPort.Location = new System.Drawing.Point(445, 129);
            this.edPort.Name = "edPort";
            this.edPort.Size = new System.Drawing.Size(134, 21);
            this.edPort.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(344, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 6;
            this.label6.Text = "目标端口";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edheattime
            // 
            this.edheattime.Location = new System.Drawing.Point(455, 32);
            this.edheattime.Name = "edheattime";
            this.edheattime.Size = new System.Drawing.Size(134, 21);
            this.edheattime.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(330, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 19);
            this.label7.TabIndex = 22;
            this.label7.Text = "心跳周期";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edheatdata
            // 
            this.edheatdata.Location = new System.Drawing.Point(145, 61);
            this.edheatdata.Multiline = true;
            this.edheatdata.Name = "edheatdata";
            this.edheatdata.Size = new System.Drawing.Size(397, 21);
            this.edheatdata.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(2, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 19);
            this.label8.TabIndex = 20;
            this.label8.Text = "自定义心跳";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // edregdata
            // 
            this.edregdata.Location = new System.Drawing.Point(145, 114);
            this.edregdata.Name = "edregdata";
            this.edregdata.Size = new System.Drawing.Size(397, 21);
            this.edregdata.TabIndex = 19;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(0, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 19);
            this.label9.TabIndex = 18;
            this.label9.Text = "自定义注册包";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edTimeout
            // 
            this.edTimeout.Location = new System.Drawing.Point(444, 198);
            this.edTimeout.Name = "edTimeout";
            this.edTimeout.Size = new System.Drawing.Size(134, 21);
            this.edTimeout.TabIndex = 17;
            this.edTimeout.TextChanged += new System.EventHandler(this.edTimeout_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(343, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 19);
            this.label10.TabIndex = 16;
            this.label10.Text = "超时重启";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edLink
            // 
            this.edLink.Location = new System.Drawing.Point(145, 7);
            this.edLink.Name = "edLink";
            this.edLink.Size = new System.Drawing.Size(161, 21);
            this.edLink.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(28, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 14;
            this.label11.Text = "短连接开关";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(284, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 19);
            this.label12.TabIndex = 12;
            this.label12.Text = "串口参数";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmIPmode
            // 
            this.cmIPmode.FormattingEnabled = true;
            this.cmIPmode.Items.AddRange(new object[] {
            "静态IP",
            "动态IP"});
            this.cmIPmode.Location = new System.Drawing.Point(134, 14);
            this.cmIPmode.Name = "cmIPmode";
            this.cmIPmode.Size = new System.Drawing.Size(161, 20);
            this.cmIPmode.TabIndex = 24;
            this.cmIPmode.SelectedIndexChanged += new System.EventHandler(this.cmIPmode_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(17, 13);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(112, 19);
            this.label13.TabIndex = 25;
            this.label13.Text = "本地地址";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(14, 198);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 19);
            this.label14.TabIndex = 27;
            this.label14.Text = "网络协议";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmProtocol
            // 
            this.cmProtocol.FormattingEnabled = true;
            this.cmProtocol.Items.AddRange(new object[] {
            "TCP Client",
            "TCP Server",
            "UDP Client",
            "UDP Server"});
            this.cmProtocol.Location = new System.Drawing.Point(133, 197);
            this.cmProtocol.Name = "cmProtocol";
            this.cmProtocol.Size = new System.Drawing.Size(161, 20);
            this.cmProtocol.TabIndex = 26;
            this.cmProtocol.SelectedIndexChanged += new System.EventHandler(this.cmProtocol_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 89);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(129, 19);
            this.label15.TabIndex = 29;
            this.label15.Text = "注册包类型";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmRegMode
            // 
            this.cmRegMode.FormattingEnabled = true;
            this.cmRegMode.Items.AddRange(new object[] {
            "关闭",
            "连接时发送MAC",
            "连接时发送自定义数据",
            "每包数据发送MAC",
            "每包数据发送自定义数据"});
            this.cmRegMode.Location = new System.Drawing.Point(145, 88);
            this.cmRegMode.Name = "cmRegMode";
            this.cmRegMode.Size = new System.Drawing.Size(161, 20);
            this.cmRegMode.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 437);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 30;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(360, 437);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // edlocalport
            // 
            this.edlocalport.Location = new System.Drawing.Point(445, 15);
            this.edlocalport.Name = "edlocalport";
            this.edlocalport.Size = new System.Drawing.Size(134, 21);
            this.edlocalport.TabIndex = 40;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(323, 15);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(108, 19);
            this.label28.TabIndex = 39;
            this.label28.Text = "本地端口";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eddnsBack
            // 
            this.eddnsBack.Location = new System.Drawing.Point(445, 68);
            this.eddnsBack.Name = "eddnsBack";
            this.eddnsBack.Size = new System.Drawing.Size(134, 21);
            this.eddnsBack.TabIndex = 38;
            // 
            // label29
            // 
            this.label29.Location = new System.Drawing.Point(315, 68);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(116, 19);
            this.label29.TabIndex = 37;
            this.label29.Text = "静态备用DNS服务器";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // eddns
            // 
            this.eddns.Location = new System.Drawing.Point(445, 41);
            this.eddns.Name = "eddns";
            this.eddns.Size = new System.Drawing.Size(134, 21);
            this.eddns.TabIndex = 36;
            // 
            // label30
            // 
            this.label30.Location = new System.Drawing.Point(315, 41);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(116, 19);
            this.label30.TabIndex = 35;
            this.label30.Text = "静态DNS服务器";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.Location = new System.Drawing.Point(26, 36);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(106, 19);
            this.label31.TabIndex = 42;
            this.label31.Text = "心跳类型";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmhbType
            // 
            this.cmhbType.FormattingEnabled = true;
            this.cmhbType.Items.AddRange(new object[] {
            "网络心跳包",
            "串口心跳包"});
            this.cmhbType.Location = new System.Drawing.Point(144, 35);
            this.cmhbType.Name = "cmhbType";
            this.cmhbType.Size = new System.Drawing.Size(161, 20);
            this.cmhbType.TabIndex = 41;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(-2, 130);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(131, 19);
            this.label32.TabIndex = 44;
            this.label32.Text = "目标地址";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmObj
            // 
            this.cmObj.FormattingEnabled = true;
            this.cmObj.Items.AddRange(new object[] {
            "目标IP",
            "域名"});
            this.cmObj.Location = new System.Drawing.Point(134, 130);
            this.cmObj.Name = "cmObj";
            this.cmObj.Size = new System.Drawing.Size(161, 20);
            this.cmObj.TabIndex = 43;
            // 
            // ck_reg
            // 
            this.ck_reg.AutoSize = true;
            this.ck_reg.Checked = true;
            this.ck_reg.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_reg.Location = new System.Drawing.Point(548, 117);
            this.ck_reg.Name = "ck_reg";
            this.ck_reg.Size = new System.Drawing.Size(60, 16);
            this.ck_reg.TabIndex = 45;
            this.ck_reg.Text = "16进制";
            this.ck_reg.UseVisualStyleBackColor = true;
            this.ck_reg.CheckedChanged += new System.EventHandler(this.ck_reg_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "8N1",
            "8O1",
            "8E1"});
            this.comboBox1.Location = new System.Drawing.Point(398, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 20);
            this.comboBox1.TabIndex = 47;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(329, 8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(113, 20);
            this.label18.TabIndex = 49;
            this.label18.Text = "清除换存数据";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "关闭",
            "打开"});
            this.comboBox2.Location = new System.Drawing.Point(455, 8);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(134, 20);
            this.comboBox2.TabIndex = 48;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Location = new System.Drawing.Point(0, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 438);
            this.tabControl1.TabIndex = 50;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel_client);
            this.tabPage1.Controls.Add(this.panel_baudRate);
            this.tabPage1.Controls.Add(this.edTimeout);
            this.tabPage1.Controls.Add(this.edlocalport);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label28);
            this.tabPage1.Controls.Add(this.edIP);
            this.tabPage1.Controls.Add(this.edGaway);
            this.tabPage1.Controls.Add(this.cmObj);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.edPort);
            this.tabPage1.Controls.Add(this.edMask);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.edDomain);
            this.tabPage1.Controls.Add(this.label32);
            this.tabPage1.Controls.Add(this.cmProtocol);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.eddnsBack);
            this.tabPage1.Controls.Add(this.label29);
            this.tabPage1.Controls.Add(this.eddns);
            this.tabPage1.Controls.Add(this.label30);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.cmIPmode);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 412);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网口配置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel_client
            // 
            this.panel_client.Controls.Add(this.label7);
            this.panel_client.Controls.Add(this.ck_heath);
            this.panel_client.Controls.Add(this.comboBox2);
            this.panel_client.Controls.Add(this.cmhbType);
            this.panel_client.Controls.Add(this.edheattime);
            this.panel_client.Controls.Add(this.label31);
            this.panel_client.Controls.Add(this.label9);
            this.panel_client.Controls.Add(this.label11);
            this.panel_client.Controls.Add(this.edregdata);
            this.panel_client.Controls.Add(this.edLink);
            this.panel_client.Controls.Add(this.cmRegMode);
            this.panel_client.Controls.Add(this.label15);
            this.panel_client.Controls.Add(this.label18);
            this.panel_client.Controls.Add(this.edheatdata);
            this.panel_client.Controls.Add(this.ck_reg);
            this.panel_client.Controls.Add(this.label8);
            this.panel_client.Location = new System.Drawing.Point(-11, 220);
            this.panel_client.Name = "panel_client";
            this.panel_client.Size = new System.Drawing.Size(622, 140);
            this.panel_client.TabIndex = 52;
            // 
            // ck_heath
            // 
            this.ck_heath.AutoSize = true;
            this.ck_heath.Checked = true;
            this.ck_heath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_heath.Location = new System.Drawing.Point(548, 66);
            this.ck_heath.Name = "ck_heath";
            this.ck_heath.Size = new System.Drawing.Size(60, 16);
            this.ck_heath.TabIndex = 50;
            this.ck_heath.Text = "16进制";
            this.ck_heath.UseVisualStyleBackColor = true;
            this.ck_heath.CheckedChanged += new System.EventHandler(this.ck_heath_CheckedChanged);
            // 
            // panel_baudRate
            // 
            this.panel_baudRate.Controls.Add(this.label4);
            this.panel_baudRate.Controls.Add(this.comboBox1);
            this.panel_baudRate.Controls.Add(this.edBord);
            this.panel_baudRate.Controls.Add(this.label12);
            this.panel_baudRate.Location = new System.Drawing.Point(33, 361);
            this.panel_baudRate.Name = "panel_baudRate";
            this.panel_baudRate.Size = new System.Drawing.Size(546, 27);
            this.panel_baudRate.TabIndex = 51;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 412);
            this.tabPage2.TabIndex = 2;
            this.tabPage2.Text = "无线配置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(636, 427);
            this.panel1.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 100;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // netForm_Eth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 462);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "netForm_Eth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "参数配置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form2_KeyPress);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel_client.ResumeLayout(false);
            this.panel_client.PerformLayout();
            this.panel_baudRate.ResumeLayout(false);
            this.panel_baudRate.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox edIP;
        private System.Windows.Forms.TextBox edMask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edBord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox edDomain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox edPort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox edheattime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox edheatdata;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox edregdata;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox edTimeout;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edLink;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmIPmode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmProtocol;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmRegMode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox edlocalport;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox eddnsBack;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox eddns;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.ComboBox cmhbType;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ComboBox cmObj;
        private System.Windows.Forms.CheckBox ck_reg;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox edGaway;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox ck_heath;
        private System.Windows.Forms.Panel panel_baudRate;
        private System.Windows.Forms.Panel panel_client;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
    }
}