using System;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;



namespace netcfg
{    
    public partial class netForm : Form
    { 
        [DllImport("ws2_32.dll")]
        static extern ushort ntohs(int netshort);

        public TUdpServer xServer;
        RF Rf = new RF();
       // int Language = 0;
        string[] protocol_info;
        public string _curMac;
        public netdata _curData;
        public SynchronizationContext _syncContext = null;
       // public UdpClient xClient;
        public int flag = 0;
        public void ChangeLanguage(int index) {
            translate.cur_language = index;
            this.Text = translate.getInfo("form1", "caption");
            this.toolStripButton1.Text = translate.getInfo("form1", "toolStripButton1");
            this.toolStripButton2.Text = translate.getInfo("form1", "toolStripButton2");
            this.toolStripButton3.Text = translate.getInfo("form1", "toolStripButton3");
           // this.toolStripButton4.Text = translate.getInfo("form1", "toolStripButton4");
            this.toolStripButton5.Text = translate.getInfo("form1", "toolStripButton5");
            this.toolStripButton6.Text = translate.getInfo("form1", "toolStripButton6");
            this.toolStripButton6.Text = translate.getInfo("form1", "toolStripButton6");
           // this.toolStripDropDownButton1.Text = translate.getInfo("form1", "toolStripDropDownButton1");
            for (int i = 0; i < 9; i++)
            {
                this.listView1.Columns[i].Text = translate.getInfo("form1", String.Format("Columns{0}", i));
                this.listView1.GridLines = true;
            }
        }
        private void  GetIP()   //获取本地IP
        {
            string name = Dns.GetHostName();
            
            toolStripComboBox1.Items.Clear();
            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
           // for(int i =0; )
               
            for (int i = 0; i < ipadrlist.Length; i++)
            {
                if (ipadrlist[i].GetAddressBytes().Length == 4)
                    toolStripComboBox1.Items.Add(ipadrlist[i].ToString());
            }
            try
            {
                toolStripComboBox1.SelectedIndex = 0;
            }
            catch (Exception)
            {

              //  throw;
            }
           // return ipadr;
        }
        public netForm()
        {
            InitializeComponent();
            GetIP();
            
            translate.doInit();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 1901);
           
            //udpclient.Bind(iPEndPoint, endPoint);
            
            udpclient.RemoteIPEndPoint=endPoint;
            protocol_info = new string[4];
            protocol_info[0] = "TCP Client";
            protocol_info[1] = "TCP Server";
            protocol_info[2] = "UDP Client";
            protocol_info[3] = "UDP Server";
            this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
            this.listView1.Columns.Add("Mac地址", 120, HorizontalAlignment.Center);
            this.listView1.Columns.Add("网络协议", 90, HorizontalAlignment.Center);
            this.listView1.Columns.Add("目标IP", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("目标端口", 80, HorizontalAlignment.Center);
            this.listView1.Columns.Add("本地IP", 100, HorizontalAlignment.Center);
            this.listView1.Columns.Add("本地端口", 80, HorizontalAlignment.Center);
            this.listView1.Columns.Add("型号", 140, HorizontalAlignment.Center);
            this.listView1.Columns.Add("版本", 80, HorizontalAlignment.Center);

            _syncContext = SynchronizationContext.Current;
            xServer = new TUdpServer(1901);
            xServer.UIForm = this;
            xServer.Start();
            ChangeLanguage(translate.cur_language);
        }
        public void UdpserverStop() { xServer.Stop(); }
        private void doAddItem(object itemData)
        {
            netdata_ex_new _ex_data = (netdata_ex_new)itemData;
            netdata_new _data = _ex_data.xdata;
            string _Mac = funs.Byte2Mac(_ex_data.xCmd.mac);
            _curMac = _Mac;
            foreach (ListViewItem item in this.listView1.Items)
            {
                if (item.SubItems[1].Text == _curMac)
                {
                    item.SubItems[4].Text = ntohs(_data.xdata.objport).ToString();
                    return;
                }
            }
            ListViewItem lvi = new ListViewItem();
            lvi.Tag = _data;
            lvi.Text = (listView1.Items.Count + 1).ToString();
            lvi.SubItems.Add(_curMac);
            lvi.SubItems.Add(protocol_info[_data.xdata.protocol]);
            if (_data.xdata.domain_type == 0)
            {
                byte[] x = new byte[4];
                x[0] = _data.xdata.desc[0];
                x[1] = _data.xdata.desc[1];
                x[2] = _data.xdata.desc[2];
                x[3] = _data.xdata.desc[3];
                lvi.SubItems.Add(funs.ByteArrayToIPAddress(x));
            }
            else
            {
                lvi.SubItems.Add(Encoding.Default.GetString(_data.xdata.desc));
            }
            lvi.SubItems.Add(ntohs(_data.xdata.objport).ToString());
            lvi.SubItems.Add(funs.ByteArrayToIPAddress(_data.IP));
            lvi.SubItems.Add(ntohs(_data.xdata.localport).ToString());
            if (_data.xdata.Version == 0)
            {
                lvi.SubItems.Add("- -");
            }
            else
            {
                if (Rf.Models.Length >= _data.xdata.Version)
                    lvi.SubItems.Add(Rf.Models[_data.xdata.Version - 1]);
                else
                    lvi.SubItems.Add("unknown");
            }

            if (netcfg.E830_ETH2A.E830ETH_Flag == false && _data.xdata.Version == 8)
            {
                MessageBox.Show("型号选择错误，请重新选择");
            }
            else
            {
                this.listView1.Items.Add(lvi);
            }
            float ver = (float)Convert.ToDouble(Convert.ToString(_data.xdata.E_Version, 16)) / 10;
            lvi.SubItems.Add("V" + string.Format("{0:F1}", ver));
        }

        public void _doAddItem(netdata_ex_new itemData)
        {
            _syncContext.Post(doAddItem, itemData);
        }



        public void _doPatchUI(int timer)
        {
            _syncContext.Post(PatchUI, timer);
        }
        public void _doClearList(int timer)
        {
            _syncContext.Post(doClearList, timer);
        }
        public void doClearList(object t)
        {
            this.listView1.Items.Clear();
        }
        public void PatchUI(object timer)
        {
            //this.listView1.Items.Clear();

            int t = (int)timer;
            timer_PatchUI.Enabled = true;
            timer_PatchUI.Interval = t;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {//搜索
            listView1.Items.Clear();
            foreach (ListViewItem item in this.listView1.Items)
            {
                //item.SubItems[8].Text = "disconnect";
            }
            timer_PatchUI.Enabled = false;
            udpclient.send(Encoding.Default.GetBytes("www.cdebyte.comwww.cdebyte.com"));
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {//配置
            ListView listView = listView1;
            if (this.listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show(translate.getInfo("form1", "btn2_show"));
                return;
            }
            ListView.SelectedIndexCollection c = this.listView1.SelectedIndices;
            netdata_new netData_new;
            string netMac = listView.Items[c[0]].SubItems[1].Text;

            if (listView.Items[c[0]].SubItems[7].Text == "unknown")
            {
                MessageBox.Show(translate.getInfo("form1", "cmd_error"));
                return;
            }
            netData_new = (netdata_new)(listView.Items[c[0]].Tag);
           
            netForm_Eth dataForm = new netForm_Eth();//this

            xServer.Stop();
           // dataForm.Language = Language;
            dataForm._Mac = netMac; 
            dataForm.IPEndPoint = netData_new.ipEndPoint;
            dataForm.SetInfo(netData_new.xdata);
            dataForm.E830_BaudVsibleSet(netcfg.E830_ETH2A.E830ETH_Flag);               //E830
            dataForm.ShowDialog();
            
            //funs.Delay1ms(200);
            xServer.Start();
            //funs.Delay1ms(200);
            netdata xdata = netData_new.xdata;
            if (dataForm.DialogResult == DialogResult.OK)
            {
                xdata = dataForm.GetInfo();
                flag = 1;
                
            }
            else
            {
                xdata = dataForm._Netdata;
                flag = 0;
                
            }

            udpclient.send(netMac, xdata);
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {//清空
            this.listView1.Items.Clear();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {//退出
            this.xServer.Stop();
            Environment.Exit(0);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.toolStripButton2_Click(sender, e);
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show(translate.getInfo("form1", "btn6_show"));
                return;
            }
            flag = 2;
            ListView.SelectedIndexCollection c = this.listView1.SelectedIndices;
            udpclient.send(listView1.Items[c[0]].SubItems[1].Text,0x03fe);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show(translate.getInfo("form1", "btn5_show"));
                return;
            }

            ListView.SelectedIndexCollection c = this.listView1.SelectedIndices;

            netForm_Mac form3 = new netForm_Mac();
            form3.setMac(listView1.Items[c[0]].SubItems[1].Text);
            form3.ShowDialog();
            if (form3.DialogResult == DialogResult.OK)
            {

                TCmdHead xCmd = new TCmdHead();
                xCmd.cmd = 0x02FE;
                xCmd.mac = funs.Mac2Byte(listView1.Items[c[0]].SubItems[1].Text);
                byte[] NewMac = funs.Mac2Byte(form3.GetMac());

                int CmdSize = Marshal.SizeOf(xCmd);
                byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), NewMac);
                netdata_new netData = (netdata_new)listView1.Items[c[0]].Tag;
                udpclient.send(xBuf);

                listView1.Items[c[0]].SubItems[1].Text = form3.GetMac();

                toolStripButton1_Click(sender, e);
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem x = (ToolStripMenuItem)sender;
           // Language = ;
            ChangeLanguage(int.Parse((string)x.Tag));
        }

        private void netForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer_PatchUI_Tick(object sender, EventArgs e)
        {
            this.listView1.Items.Clear();
            udpclient.send(Encoding.Default.GetBytes("www.cdebyte.comwww.cdebyte.com"));
            timer_PatchUI.Enabled = false;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GetIP();
            Random random = new Random();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(toolStripComboBox1.Text), random.Next(5000,60000));
            udpclient.LocalIPEndPiont = iPEndPoint;

        }

    }

    public static class E830_ETH2A
    {
        public static bool E830ETH_Flag;
    }

    public class TUdpServer : udpserver
    {

        public netForm UIForm;
        public static bool flag = false;
        public TUdpServer(int port)
            : base(port)
        {
            Console.WriteLine("Server Start...@" + port.ToString());
        }

        protected override void PacketReceived(UDPPacketBuffer buffer)
        {
            TCmdHead xCmd = new TCmdHead();
            IWin32Window win32Window = UIForm;
            xCmd = (TCmdHead)funs.ByteToStruct(buffer.Data, xCmd.GetType());
            int len = Marshal.SizeOf(xCmd);
            switch (xCmd.cmd)
            {
                case 0x00FD://查询返回指令
                    {
                        netdata_ex_new _data;
                        netdata_new xcfg = new netdata_new();
                        byte[] newdata = funs.SubByte(buffer.Data, len, buffer.DataLength - len);
                        xcfg.xdata = (netdata)funs.ByteToStruct(newdata, xcfg.xdata.GetType());
                        UIForm._curMac = funs.Byte2Mac(xCmd.mac);
                        IPEndPoint Ip = (IPEndPoint)buffer.RemoteEndPoint;
                        xcfg.IP = Ip.Address.GetAddressBytes();
                        xcfg.ipEndPoint = Ip;
                        _data.xCmd = xCmd;
                        _data.xdata = xcfg;
                        UIForm._doAddItem(_data);
                    }
                    break;
                case 0x01FD:
                    {
                        udpclient.send(funs.Byte2Mac(xCmd.mac), 0x03fe);
                    }
                    break;
                case 0x02FD:
                    {
                        string sMac = funs.Byte2Mac(xCmd.mac);
                        MessageBox.Show(win32Window, translate.getInfo("form1", "cmd_result_mac"));
                    }
                    break;
                case 0x03FD:
                    {
                      
                        if (UIForm.flag==1)
                        {
                            
                            UIForm._doClearList(1);
                            udpclient.send(Encoding.Default.GetBytes("www.cdebyte.comwww.cdebyte.com"));
                            flag = false;
                            MessageBox.Show(win32Window,translate.getInfo("form1", "cmd_result_cfg"));
                            UIForm._doPatchUI(2000);
                        }
                        if (UIForm.flag == 2)
                            MessageBox.Show(win32Window, translate.getInfo("form1", "cmd_result_reboot"));
                        UIForm.flag = 0;

                    }
                    break;
                default:
                    break;

            }
        }
        protected override void PacketSent(UDPPacketBuffer buffer, int bytesSent)
        {
        }

    }
    public static class udpclient
    {
        static  UdpClient  client=new UdpClient();
        public static IPEndPoint LocalIPEndPiont;
        public static EndPoint RemoteIPEndPoint;
        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
        public static void send(byte[] data)
        {
            send(data, LocalIPEndPiont, RemoteIPEndPoint);
        }
        public static void send(byte[] data, EndPoint remote)
        {
            send(data, LocalIPEndPiont, remote);
        }
        public static void send(byte[] data, IPEndPoint local)
        {
            client.Close();
            client = new UdpClient(local);
            client.Send(data, data.Length, (IPEndPoint)RemoteIPEndPoint);
        }
        public static void send(byte[] data,IPEndPoint local,EndPoint remote)
        {
            client.Close();
            client = new UdpClient(local);
            client.Send(data, data.Length, (IPEndPoint)remote);
        }
        public static void send(string mac, netdata xdata)
        {
            TCmdHead xCmd = new TCmdHead()
            {
                cmd = 0x01FE,
                mac = funs.Mac2Byte(mac)
            };
            int CmdSize = Marshal.SizeOf(xCmd);
            int dataSize = Marshal.SizeOf(xdata);
            byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), funs.StructToBytes(xdata, dataSize));
            send(xBuf,LocalIPEndPiont,RemoteIPEndPoint);
        }
        public static void send(string mac,ushort cmd)
        {
            TCmdHead xCmd = new TCmdHead();
            xCmd.mac =funs.Mac2Byte(mac);
            xCmd.cmd = cmd;
            int CmdSize = Marshal.SizeOf(xCmd);
            byte[] xBuf = funs.StructToBytes(xCmd, CmdSize);
            //netdata_new netData = ((netdata_new)listView1.Items[c[0]].Tag);
            send(xBuf, LocalIPEndPiont, RemoteIPEndPoint);
        }


        public static void send(string mac, ushort cmd,byte[] data)
        {
            TCmdHead xCmd = new TCmdHead();
            xCmd.mac = funs.Mac2Byte(mac);
            xCmd.cmd = cmd;
            int CmdSize = Marshal.SizeOf(xCmd);
            byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), data);
            //netdata_new netData = ((netdata_new)listView1.Items[c[0]].Tag);
            send(xBuf, LocalIPEndPiont, RemoteIPEndPoint);
        }
    }
}
