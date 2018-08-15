using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Runtime.InteropServices;


namespace netcfg
{

    public partial class netForm_Eth : Form
    {
        public netForm tForm;
        //private void tt()
        //{
        //   // tForm.doClearList;
        //}



        [DllImport("ws2_32.dll")]
        static extern int ntohs(int netshort);
        //netForm_Universal form ;
        public object Form { get; set; }

        public IPEndPoint IPEndPoint { set; get; }
        //  ArrayList form;
        public string _Mac { get; set; }
        public netdata _Netdata { get; set; }
       // public int Language { get; set; }
        RF Rf = new RF();
        Label[] label = new Label[10];
        TextBox[] textBox = new TextBox[4];
        ComboBox[] comboBox = new ComboBox[9];
        bool submenuFlag = true;
        private void ChangeItems(ComboBox obj,string items){
            obj.Items.Clear();
            string[] xlist = System.Text.RegularExpressions.Regex.Split(items, "\r\n"); //items.Split("\r\n", StringSplitOptions.None);
            foreach (string x in xlist)
                obj.Items.Add(x);
        }

        //netForm parent
        public netForm_Eth()
        {
            InitializeComponent();

            //tForm = parent;
            byte[] RF_Seting = new byte[20];
            this.Text = translate.getInfo("form2", "caption");
            button1.Text = translate.getInfo("form2", "btn_ok");
            button2.Text = translate.getInfo("form2", "btn_cancel");
            for (int i = 1; i < 33; i++)
            {
                try
                {
                    Label _label = (Label)Controls.Find("label" + i.ToString(), true)[0];
                    _label.Text = translate.getInfo("form2", "label" + i.ToString());
                }
                catch
                {
                }
            }
            ck_heath.Text = translate.getInfo("form2", "ck_heath");
            ck_reg.Text = translate.getInfo("form2", "ck_reg");
            tabPage1.Text= translate.getInfo("form2", "tabPage1");
            tabPage2.Text = translate.getInfo("form2", "tabPage2");
            ChangeItems(cmIPmode,translate.getInfo("form2", "cmIPmode_items"));
            ChangeItems(cmObj, translate.getInfo("form2", "cmObj_items"));
            ChangeItems(cmProtocol, translate.getInfo("form2", "cmProtocol_items"));
            ChangeItems(comboBox1, translate.getInfo("form2", "comboBox1_items"));
            ChangeItems(cmRegMode, translate.getInfo("form2", "cmRegMode_items"));
            ChangeItems(comboBox2, translate.getInfo("form2", "comboBox2_items"));
            ChangeItems(cmhbType, translate.getInfo("form2", "cmhbType_items"));

            toolTip1.SetToolTip(cmIPmode, translate.getInfo("form2", "cmIPmode"));
            toolTip1.SetToolTip(edIP, translate.getInfo("form2", "edIP"));
            toolTip1.SetToolTip(edMask, translate.getInfo("form2", "edMask"));
            toolTip1.SetToolTip(edGaway, translate.getInfo("form2", "edGaway"));
            toolTip1.SetToolTip(eddns, translate.getInfo("form2", "eddns"));
            toolTip1.SetToolTip(eddnsBack, translate.getInfo("form2", "eddnsBack"));
            toolTip1.SetToolTip(edlocalport, translate.getInfo("form2", "edlocalport"));
            toolTip1.SetToolTip(edPort, translate.getInfo("form2", "edPort"));
            toolTip1.SetToolTip(cmObj, translate.getInfo("form2", "cmObj"));
            toolTip1.SetToolTip(edDomain, translate.getInfo("form2", "edDomain"));
            toolTip1.SetToolTip(cmProtocol, translate.getInfo("form2", "cmProtocol"));
            toolTip1.SetToolTip(edBord, translate.getInfo("form2", "edBord"));
            toolTip1.SetToolTip(comboBox1, translate.getInfo("form2", "comboBox1"));
            toolTip1.SetToolTip(edLink, translate.getInfo("form2", "edLink"));
            toolTip1.SetToolTip(edTimeout, translate.getInfo("form2", "edTimeout"));
            toolTip1.SetToolTip(cmRegMode, translate.getInfo("form2", "cmRegMode"));
            toolTip1.SetToolTip(edregdata, translate.getInfo("form2", "edregdata"));
            toolTip1.SetToolTip(edheatdata, translate.getInfo("form2", "edheatdata"));
            toolTip1.SetToolTip(edheattime, translate.getInfo("form2", "edheattime"));
            toolTip1.SetToolTip(cmhbType, translate.getInfo("form2", "cmhbType"));
        }

       // public void AddForm(object form)
        private void Form2_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
        public void SetInfo(netdata data)
        {
            _Netdata=data;
            if (data.Version < 4 )
            {
                panel_baudRate.Enabled = true;
                tabPage2.Parent = null;
            }
            else
            {
                panel_baudRate.Enabled = false;
            }

            if (data.Version == 8)
            {
                tabPage2.Parent = null;
                panel_baudRate.Enabled = false;
            }
            this.cmIPmode.SelectedIndex = data.DHCP;
            this.edIP.Text = funs.ByteArrayToIPAddress(data.IP);
            this.edMask.Text = funs.ByteArrayToIPAddress(data.mask);
            this.edGaway.Text = funs.ByteArrayToIPAddress(data.gateway);
            this.eddns.Text = funs.ByteArrayToIPAddress(data.dnsServer);
            this.eddnsBack.Text = funs.ByteArrayToIPAddress(data.dnsServerBack);
            this.edlocalport.Text = ntohs((short)data.localport).ToString();
            this.edPort.Text = ntohs((short)data.objport).ToString();
            this.cmObj.SelectedIndex = data.domain_type;
            this.edDomain.Text = System.Text.Encoding.Default.GetString(data.desc);
            if (data.domain_type == 0)
            {
                byte[] x = new byte[4];
                x[0] = data.desc[0];
                x[1] = data.desc[1];
                x[2] = data.desc[2];
                x[3] = data.desc[3];
                this.edDomain.Text = funs.ByteArrayToIPAddress(x);
            }
            else
            {
                this.edDomain.Text = System.Text.Encoding.Default.GetString(data.desc);
            }
            this.cmProtocol.SelectedIndex = data.protocol;
            this.edBord.Text = funs.byte3_str(data.boad);// ntohs((short)data.boad).ToString();
            this.comboBox1.SelectedIndex = data.com;
            this.edLink.Text = data.link.ToString();
            this.edTimeout.Text = ntohs((short)data.time_root).ToString();
            this.cmRegMode.SelectedIndex = data.reg_send_mode;

            this.edregdata.Text = funs.byteToHexStr(funs.SubByte(data.reg_data, 0, data.regLen));
            this.edheatdata.Text = funs.byteToHexStr(funs.SubByte(data.Heartbeat, 0, data.hbLen));
            this.edheattime.Text = ntohs((short)data.HeartTime).ToString();
            this.cmhbType.SelectedIndex = data.hbType;
            comboBox2.SelectedIndex = data.clear; 
        }
        public void  E830_BaudVsibleSet( bool value )
        {
            if (value == false)
            {
                this.edBord.Visible = true;
                panel_baudRate.Visible = true;
                comboBox1.Visible = true;
            }
            else
            {
                this.edBord.Visible = false;
                panel_baudRate.Visible = false;
                comboBox1.Visible = false;
            }
        }
        public netdata GetInfo()
        {
            netdata xdata = _Netdata;
            
            xdata.hbLen = 255;
            xdata.DHCP = (byte) cmIPmode.SelectedIndex;
            if (!funs.IsIP(this.edIP.Text))
            {
                this.edIP.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg0"));
                return xdata;
            }
            xdata.IP = funs.IpToByte(this.edIP.Text);
            if (!funs.IsIP(this.edMask.Text))
            {
                this.edMask.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg1"));
                return xdata;
            }
            xdata.mask = funs.IpToByte(this.edMask.Text);
            if (!funs.IsIP(this.edGaway.Text))
            {
                this.edGaway.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg2"));
                return xdata;
            }
            xdata.gateway = funs.IpToByte(this.edGaway.Text);
            if (!funs.IsIP(this.eddns.Text))
            {
                this.eddns.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg3"));
                return xdata;
            }
            xdata.dnsServer = funs.IpToByte(this.eddns.Text);
            if (!funs.IsIP(this.eddnsBack.Text))
            {
                this.eddnsBack.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg4"));
                return xdata;
            }
            xdata.dnsServerBack = funs.IpToByte(this.eddnsBack.Text);
            if (!funs.IsPort(this.edPort.Text))
            {
                this.edPort.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg5"));
                return xdata;
            }
            xdata.objport = (UInt16)ntohs(int.Parse(this.edPort.Text));
            if (!funs.IsPort(this.edlocalport.Text))
            {
                this.edlocalport.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg6"));
                return xdata;
            }
            xdata.localport = (UInt16)ntohs(int .Parse(edlocalport.Text));
            xdata.domain_type = (byte)cmObj.SelectedIndex;
            
            //*
            if (cmObj.SelectedIndex == 0)
            {
                if (!funs.IsIP(this.edDomain.Text))
                {
                    this.edDomain.Focus();
                    MessageBox.Show(translate.getInfo("form2", "msg0"));
                    return xdata;
                }
                byte[] x = funs.IpToByte(this.edDomain.Text);
                xdata.desc = new byte[64];
                xdata.desc[0] = x[0];
                xdata.desc[1] = x[1];
                xdata.desc[2] = x[2];
                xdata.desc[3] = x[3];
            }
            else
            {
                xdata.desc = System.Text.Encoding.Default.GetBytes(edDomain.Text.PadRight(64, '\0'));
            }//*/
            xdata.protocol = (byte)this.cmProtocol.SelectedIndex;
            if (xdata.Version < 4)
            {
                xdata.boad = funs.int2byte3(int.Parse(this.edBord.Text));// (UInt16)ntohs(short.Parse(this.edBord.Text));
                xdata.com = (byte)(this.comboBox1.SelectedIndex);
            }
            else if (xdata.Version == 8)      //E830
            {
                xdata.boad = funs.int2byte3(9600);
            }
            xdata.link =(Byte) int.Parse(this.edLink.Text);
            if (!funs.IsEdTimeout(this.edTimeout.Text))
            {
                this.edTimeout.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg9"));
                return xdata;
            }
            
             if (!funs.IsEdheattime(this.edheattime.Text))
            {
                this.edheattime.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg9"));
                return xdata;
            }
            if (!funs.IsBaudrate(this.edBord.Text))
            {
                this.edBord.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg9"));
                return xdata;
            }
            if (!funs.IsEdLink(this.edLink.Text))
            {
                this.edLink.Focus();
                MessageBox.Show(translate.getInfo("form2", "msg9"));
                return xdata;
            }
            xdata.time_root = (UInt16)ntohs(short.Parse(this.edTimeout.Text));
            xdata.reg_send_mode = (byte)this.cmRegMode.SelectedIndex;
            xdata.clear = (byte)comboBox2.SelectedIndex;
            byte[] xarr;
            if (ck_reg.Checked)
            {
                xarr = funs.HexString2Bytes(edregdata.Text);
            }
            else
            {
                xarr = ASCIIEncoding.Default.GetBytes(edregdata.Text);
            }
            xdata.regLen = (byte)xarr.Length;
            if (xarr.Length > 40)
                xdata.regLen = 40;
            xdata.reg_data = new byte[40];
            Array.Copy(xarr, xdata.reg_data, xdata.regLen);
           
            if (ck_heath.Checked)
            {
                xarr = funs.HexString2Bytes(edheatdata.Text);
            }
            else
            {
                xarr = ASCIIEncoding.Default.GetBytes(edheatdata.Text);
            }
            xdata.hbLen = (byte)xarr.Length;
            if (xarr.Length > 40)
                xdata.hbLen = 40;
            xdata.Heartbeat = new byte[40];
            Array.Copy(xarr, xdata.Heartbeat, xdata.hbLen);

            xdata.HeartTime = (UInt16)ntohs(int.Parse(this.edheattime.Text));
            xdata.hbType = (byte)cmhbType.SelectedIndex;
            byte[] vBytes = new byte[2];
            for (int i = 0; i < 4; i += 2)
            {
                    vBytes[i / 2] = 0;
            }
            try
            {
                
            }
            catch (Exception)
            {
            }
            return xdata;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (_Netdata.Version > 3)
            {
                if (submenuFlag == false)
                {
                    if (_Netdata.Version != 7)
                    {

                        ((netForm_Universal)Form).Exit();
                        _Netdata = ((netForm_Universal)Form)._Netdata;
                    }
                    else
                    {

                        ((E70)Form).Exit();
                    }
                }

            }
            netdata xxdata=   GetInfo();
                if (xxdata.hbLen != 255)
            {
                this.DialogResult = DialogResult.OK; 
            } 
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void edGaway_TextChanged(object sender, EventArgs e)
        {

        }

        private void ck_reg_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_reg.Checked)
            {
                edregdata.Text = funs.StrToHex1(edregdata.Text);
            }
            else
            {
                edregdata.Text = funs.HexToStr(edregdata.Text);
            }

        }
        private void ck_heath_CheckedChanged(object sender, EventArgs e)
        {
            if (ck_heath.Checked)
            {
                edheatdata.Text = funs.StrToHex1(edheatdata.Text);
            }
            else
            {
                edheatdata.Text = funs.HexToStr(edheatdata.Text);
            }
        }

        private void edTimeout_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmProtocol.SelectedIndex >= 2)
            {
                label11.Enabled = false;
                edLink.Enabled = false;
            }
            else
            {
                label11.Enabled = true;
                edLink.Enabled = true;
            }
            if (cmProtocol.SelectedIndex == 0 || cmProtocol.SelectedIndex == 2)
                panel_client.Enabled = true;
            else
                panel_client.Enabled = false;

            if (cmProtocol.Text == "TCP Server" || cmProtocol.Text == "UDP Server")
            {
                cmObj.Enabled = false;
                edDomain.Enabled = false;
                edPort.Enabled = false;
            }
            else
            {
                cmObj.Enabled = true;
                edDomain.Enabled = true;
                edPort.Enabled = true;
            }
           
        }

        private void E70Show()
        {
            this.Form = new E70();
            E70 form = (E70)Form;
            form.Mac = _Mac;
            form.netdata = _Netdata;
            form.SetNetMode();
            submenuFlag = false;
            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            form.TopLevel = false; //指示子窗体非顶级窗体
            panel1.Controls.Add(form);//将子窗体载入panel
            form.IPEndPoint = IPEndPoint;
            form.UdpserverStart();
            form.Show();
            funs.Delay1ms(2500);//延时时间最好大于2000ms，在修改端口后会让模块重启，时间比较长，若延时太短可能会导致配置失败
        }
        private void UniversalFormShow()
        {
            Form = new netForm_Universal(_Netdata, _Mac, tForm);  
            var  form = (netForm_Universal)Form;
            Rf.GetConfigFile(_Netdata.Version - 1);
            int i = Rf.GetClass();
            int x = Rf.GetConfigBaudRate();
            netdata netdata = _Netdata;
            netdata.boad = funs.int2byte3(Rf.GetConfigBaudRate());
            netdata.com = 0x00;
            udpclient.send(_Mac,netdata);
            timer1.Enabled = true;
            form.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            form.TopLevel = false; //指示子窗体非顶级窗体
            panel1.Controls.Add(form);//将子窗体载入panel
            form.UDPserverStart();
            form.Show();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex==1&& submenuFlag)
            {
                submenuFlag = false;
                if (_Netdata.Version == 7)
                    E70Show();
                else
                    UniversalFormShow();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            timer1.Enabled = false;
            if (RF._ConfigFile.Length > 30)
            {
                // string [] str = RF._ConfigFile[30].Split(':')[1];
                string str = RF._ConfigFile[30].Split(':')[1];
                char[] data;
                data = str.ToCharArray();
                byte[] dataBack = new byte[data.Length / 2];
                for (int i = 0; i < data.Length / 2; i++)
                {
                    byte tmp = (byte)( ( (data[i * 2] >= 'A')?(data[i*2]-'A'+10):(data[i*2] - 48) )<<4);
                    byte tmp1 = (byte)((data[i * 2+1] >= 'A') ? (data[i * 2+1] - 'A' + 10) : (data[i * 2+1] - 48));

                    dataBack[i] = (byte)(tmp + tmp1);
                }
                

                //for (int i = 0; i < data.Length; i++)
                //{
                //    //data[i]= Convert.ToByte(str[i],16);                 
                //}

                udpclient.send(_Mac, 0x04fe, dataBack);
            }



            //if (tForm.listView1.Items[0].SubItems[7].Text == "E90-DTU (433C30E)")
            //{
            //    byte[] data = { 0x02, 0x03, 0xC3, 0xC3, 0xC3 };
            //    udpclient.send(_Mac, 0x04fe, data);
            //}
            //else
            //{
            //    byte[] data = { 0x03, 0x03, 0xC3, 0xC3, 0xC3 };
            //    udpclient.send(_Mac, 0x04fe, data);
            //}


        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                if (submenuFlag == false)
                {
                    if (_Netdata.Version != 7)
                        ((netForm_Universal)Form).UDPserverStop();
                    else
                    {
                        ((E70)Form).Exit();
                    }
                }
                this.Close();
            }
        }
       
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (submenuFlag == false)
                if (_Netdata.Version != 7)
                    ((netForm_Universal)Form).UDPserverStop();
                else
                {
                    ((E70)Form).Exit();
                }
        }

        private void cmIPmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmIPmode.Text == "动态IP")
            {
                edIP.Enabled = false;
                edMask.Enabled = false;
                edGaway.Enabled = false;
                eddns.Enabled = false;
                eddnsBack.Enabled = false;
            }
            else
            {
                edIP.Enabled = true;
                edMask.Enabled = true;
                edGaway.Enabled = true;
                eddns.Enabled = true;
                eddnsBack.Enabled = true;
            }
            
        }
    }
}
