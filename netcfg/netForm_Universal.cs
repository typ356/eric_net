using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace netcfg
{
    public partial class netForm_Universal : Form
    {
        [DllImport("ws2_32.dll")]
        static extern int ntohs(int netshort);
        RF Rf = new RF();
        Label[] label = new Label[10];
        TextBox[] textBox = new TextBox[4];
        ComboBox[] comboBox = new ComboBox[9];
        //UdpClient xClient = new UdpClient("255.255.255.255",1901);
        public  string _Mac="";
        public  netdata _Netdata=new netdata();
        public  SynchronizationContext _syncContext = null;
        public  int Language=translate.cur_language;
        submenuTUdpServer1 xServer;
        public bool flag = true;
        public netForm tForm;
        public netForm_Universal(netdata netdata,string mac,netForm parent)
        {
            tForm = parent;
            InitializeComponent();
            button3.Text = translate.getInfo("form2", "button3");
            button4.Text = translate.getInfo("form2", "button4");
            for (int i = 0; i < label.Length; i++)
            {
                label[i] = (Label)Controls.Find("RF_label" + i.ToString(), true)[0];             
            }
            for (int i = 1; i < comboBox.Length; i++)
            {
                comboBox[i] = (ComboBox)Controls.Find("RF_C" + (i-1).ToString(), true)[0];
            }
            for (int i = 0; i < textBox.Length; i++)
            {
                textBox[i] = (TextBox)Controls.Find("RF_T" + i.ToString(), true)[0];
            }
            Rf.ControlEnable(label, textBox, comboBox, false);
            xServer = new submenuTUdpServer1(1901);
            _syncContext = SynchronizationContext.Current;
            xServer.UIForm = this;
            _Netdata = netdata;
            _Mac = mac;
            Rf.GetConfigFile(netdata.Version-1);
            

        }
        public void UDPserverStart() {xServer.Start();}
        public void UDPserverStop() { xServer.Stop(); }
        public void Exit()
        {
            if (RF_T0.Enabled == true)
            {
                TCmdHead xCmd = new TCmdHead();
                xCmd.cmd = 0x04FE;
                xCmd.mac = funs.Mac2Byte(_Mac);
                int CmdSize = Marshal.SizeOf(xCmd);
                byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), Rf.RfConfigData(textBox, comboBox));
                flag = false;
                udpclient.send(xBuf);
                int mode = RF_C0.SelectedIndex;
                if (Rf.GetClass() == 2)
                {
                    if (mode != 0)
                        mode += 1;
                }
               
                _Netdata.MTU = (ushort)(ntohs(Rf.GetMTU()) | ntohs(mode << 12));
                _Netdata.boad = funs.int2byte3(int.Parse(RF_C2.Text));
                _Netdata.com=(byte)(RF_C1.SelectedIndex);
                while (flag == false)
                    funs.Delay1ms(5);
            }
            xServer.Stop();
        }
        public void _doAddRfData(UDPPacketBuffer RfData)
        {
            _syncContext.Post(setRfData, RfData);
        }
        public void setRfData(object Data)
        {

            UDPPacketBuffer RfData = (UDPPacketBuffer)Data;
            switch (RfData.Data[10])
            {
                case (0xC0):
                    {
                        Rf.ControlDisView(label, textBox, comboBox);
                        Rf.ParseConfigFile(label, textBox, comboBox, Language);
                        Rf.RfConfigDataParse(textBox, comboBox, RfData.Data);
                        Rf.Show(textBox, comboBox, Language, _Netdata.Version);
                        try
                        {
                            int mode = (ntohs(_Netdata.MTU) >> 12) & 0x07;
                            if (Rf.GetClass() == 2)
                            {
                                if (mode > 1)
                                    mode -= 1;
                            }
                            RF_C0.SelectedIndex = mode;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;

                case (0xC3):
                    {
                        if (Rf.GetVersion(RfData.Data) != -1)
                        {
                            button3.Enabled = true;
                            button4.Enabled = true;
                        }
                    }
                    break;
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            TCmdHead xCmd = new TCmdHead();
            xCmd.cmd = 0x04FE;
            xCmd.mac = funs.Mac2Byte(_Mac);
            int CmdSize = Marshal.SizeOf(xCmd);
            byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), Rf.GetFactorySettings());
            udpclient.send(xBuf);

        }
        private void button3_Click(object sender, EventArgs e)
        {
            TCmdHead xCmd = new TCmdHead();
            xCmd.cmd = 0x04FE;
            xCmd.mac = funs.Mac2Byte(_Mac);
            byte[] data = { 0x03, 0x03, 0xC1, 0xC1, 0xC1 };
            data[0] = (byte)Rf.GetConfigMode();
            int CmdSize = Marshal.SizeOf(xCmd);
            byte[] xBuf = funs.copybyte(funs.StructToBytes(xCmd, CmdSize), data);
            udpclient.send(xBuf);
        }

        private void RF_label8_Click(object sender, EventArgs e)
        {

        }

        private void RF_C6_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (RF_C6.SelectedIndex.ToString() == "0")
            //{
            //    RF_C3.Enabled = true;
            //}
            //else
            //{
            //    RF_C3.Enabled = false;
            //}
            //if (RF_C6.Text == "不启用")
            //{
            //    RF_C3.Enabled = true;
            //    RF_C2.Enabled = true;
            //}
            //else
            //{
            //    RF_C3.Enabled = false;
            //    RF_C2.Enabled = false;
            //}
       
        }

        private void RF_C5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RF_C5.Text == "定长传输方式")
            {
                RF_C3.Enabled = true;
 
            }
            else if(RF_C5.Text == "连续传输方式")
            {
                RF_C3.Enabled = false;
            
            }
            
        }
   
        //private void RF_C0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int len = tForm.listView1.Items.Count;
        //    string str = tForm.listView1.Items[0].SubItems[7].Text;
        //    if (tForm.listView1.Items[0].SubItems[7].Text == "E90-DTU (433C30E)")
        //    {
        //        RF_C0.Enabled = false;
        //        RF_C0.Visible = false;
        //    }
        //    }




        //private void RF_T3_TextChanged(object sender, EventArgs e)
        //{
        //    if (Convert.ToUInt32(RF_T3.Text) > 31)
        //    {
        //        RF_T3.Text = "31";
        //    }
        //}
    }
    public class submenuTUdpServer1 : udpserver
    {

        public netForm_Universal UIForm;
        public submenuTUdpServer1(int port)
            : base(port)
        {
            Console.WriteLine("Server Start...@" + port.ToString());
        }

        protected override void PacketReceived(UDPPacketBuffer buffer)
        {
            TCmdHead xCmd = new TCmdHead();
            xCmd = (TCmdHead)funs.ByteToStruct(buffer.Data, xCmd.GetType());
            switch (xCmd.cmd)
            {
                case 0x04FD:
                    {
                        UIForm._doAddRfData(buffer);
                        if (UIForm.flag == false)
                            UIForm.flag = true;

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
}
