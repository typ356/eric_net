using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using netcfg;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net;
using System.IO;

namespace netcfg
{
    public partial class E70 : Form
    {
        SerialPort serialPort = new SerialPort();
        private bool NetFlag { get ; set; }

        long RecvConter = 0;
        long SendConter = 0;
        public IPEndPoint IPEndPoint { get; set; }
        public string Mac { get; set; }
        
        TextBox[] textBox_ZD = new TextBox[40];
        TextBox[] textBox_BZ = new TextBox[40];
        Button[] button_ZD = new Button[40];
        Label[] label_BZ = new Label[40];
        int Page = 0;

        public netdata netdata { get; set; }
        EndPoint EndPoint;
        public SynchronizationContext _syncContext = null;
        SubmenuTUdpServer2 xServer;
        int Port = 0;
        [DllImport("ws2_32.dll")]
        static extern int ntohs(int netshort);
        public void ChangeLanguage(int index)
        {
            translate.cur_language = index;
            string[] buttonName = { "button_SerialOpen", "button_SerialClose", "button_WindowsSave", "button_RecvClear", "button_Send",
                "button_SendClear", "button_AT0", "button_AT1", "button_AT2", "button_AT3", "button_AT4", "button_AT5", "button_AT6", "button_AT7", "button_AT8", "button_AT9",
                "button1", "button2", "button3", "button_PageSub", "button_PageFist", "button_PageAdd" , "button_PageEnd"};
            string[] labelName = { "label_Port", "label_Baud", "label_StopBit", "label_DataBit", "label_Parity", "label6", "label2", "label3" };
            string[] checkBoxName = { "checkBox2", "checkBox1" };
            for (int i = 0; i < buttonName.Length; i++)
            {
                try
                {
                    Button button = (Button)Controls.Find(buttonName[i], true)[0];
                    button.Text = translate.getInfo("e70", buttonName[i]);
                }
                catch (Exception)
                {
                }
            }
            for (int i = 0; i < checkBoxName.Length; i++)
            {

                CheckBox checkBox = (CheckBox)Controls.Find(checkBoxName[i], true)[0];
                checkBox.Text = translate.getInfo("e70", checkBoxName[i]);
            }
            for (int i = 0; i < labelName.Length; i++)
            {

                Label label = (Label)Controls.Find(labelName[i], true)[0];
                label.Text = translate.getInfo("e70", labelName[i]);
            }
            for (int i = 0; i < 11; i++)
            {
                BindCbox((ComboBox)Controls.Find("comboBox_AT" + i.ToString(), true)[0]);
            }
            for (int i=0;i< label_BZ.Length;i++)
            {
                label_BZ[i].Text = translate.getInfo("e70", "label_bz");
            }
            tabPage1.Text = translate.getInfo("e70", "tabPage1");
            tabPage2.Text = translate.getInfo("e70", "tabPage2");
            tabPage3.Text = translate.getInfo("e70", "tabPage3");
        }
        public void ChangePort(int Port, bool mtu)//
        {
            netdata _netdata = netdata;
            _netdata.protocol = 2;//UDP client
            _netdata.localport = (UInt16)ntohs(Port);
            _netdata.objport= (UInt16)ntohs(Port);
            byte []bytes = funs.IpToByte(udpclient.LocalIPEndPiont.Address.ToString());
            for (int i = 0; i < bytes.Length; i++)
                _netdata.desc[i] = bytes[i];
            if (mtu)
                _netdata.MTU = (UInt16)(ntohs(0x7000) | _netdata.MTU);
            else
                _netdata.MTU = (UInt16)(ntohs(0x8fff)& _netdata.MTU);
            udpclient.send(Mac,_netdata);
            funs.Delay1ms(50);
            udpclient.send(Mac,0x03fe);//重启生效

        }
        public void SetNetMode()
        {
            ChangePort(Port, false);
            NetFlag = true;
            panel1.Enabled = false;
            comboBox_AT5.Enabled = false;
            comboBox_AT6.Enabled = false;
        }

        public void  Exit()
        {
           
            if (NetFlag)
            {
                xServer.Stop();
            }
            else
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
            }
        }
        public E70()
        {
            InitializeComponent();
            panel_AT1.Parent = tabPage2;
            panel_AT2.Parent = tabPage2;

            for (int i = 0; i < label_BZ.Length; i++)
            {
                button_ZD[i] = new Button() ;
                textBox_ZD[i] = new TextBox();
                textBox_BZ[i] = new TextBox();
                label_BZ[i] = new Label();

                Button button= (Button)Controls.Find("button_zd" + (i % 4 + 1).ToString(), true)[0];
                TextBox textBox1 =(TextBox)Controls.Find("textBox_zd" + (i % 4 + 1).ToString(), true)[0];
                TextBox textBox2 = (TextBox)Controls.Find("textBox_bz" + (i % 4 + 1).ToString(), true)[0];
                Label label= (Label)Controls.Find("label_bz" + (i % 4 + 1).ToString(), true)[0];

                button_ZD[i].Name = "button_ZD" + i.ToString();
                button_ZD[i].Text = (i + 1).ToString();
                button_ZD[i].Size = button.Size;
                button_ZD[i].Parent = button.Parent;
                button_ZD[i].Location = button.Location;
                button_ZD[i].Click += button_zd_Click;
               
               
                textBox_ZD[i].Name = "textBox_ZD" + i.ToString();
                textBox_ZD[i].Size = textBox1.Size;
                textBox_ZD[i].Parent = textBox1.Parent;
                textBox_ZD[i].Location = textBox1.Location;


                textBox_BZ[i].Name = "textBox_BZ" + i.ToString();
                textBox_BZ[i].Size = textBox2.Size;
                textBox_BZ[i].Parent = textBox2.Parent;
                textBox_BZ[i].Location = textBox2.Location;

                label_BZ[i].Size = label.Size;
                label_BZ[i].Parent = label.Parent;
                label_BZ[i].Text = translate.getInfo("e70", "label_bz");
                label_BZ[i].Location = label.Location;

            }
            Panel3Add(Page);
            string[] baud = { "1200","2400","4800","9600","19200","38400","57600","115200"};
            string[] stopbits = { "1","2", "1.5" };
            string[] parity = { "None","Odd", "Even" };
            string[] databits = { "5", "6", "7", "8" };
            string[] portName = SerialPort.GetPortNames();
            comboBox_Port.Items.AddRange(portName);
            comboBox_Baud.Items.AddRange(baud);
            comboBox_Baud.Text = "115200";
            comboBox_StopBit.Items.AddRange(stopbits);
            comboBox_StopBit.Text = "1";
            comboBox_Parity.Items.AddRange(parity);
            comboBox_Parity.SelectedIndex = 0;
            comboBox_DataBits.Items.AddRange(databits);
            comboBox_DataBits.SelectedIndex = databits.Length - 1;
           
            Random random = new Random();
            do
            {
                Port = random.Next(5000,65535);
            } while (udpclient.PortInUse(Port));
           
            
            xServer = new SubmenuTUdpServer2(Port);
            xServer.UIForm = this;
            _syncContext = SynchronizationContext.Current;
            NetFlag = false;
            
            button_SerialSwitch.Click += Button_Click;
            button_SerialSwitch.Name = "button_SerialOpen";
            
            try
            {
                string[] file = File.ReadAllLines(@"C:\Users\Public\Documents\Ebyte_Config2.txt");
                if (file.Length >= textBox_ZD.Length + textBox_BZ.Length)
                {
                    for (int i = 0; i < textBox_ZD.Length; i++)
                    {
                        textBox_ZD[i].Text = file[2 * i];
                        textBox_BZ[i].Text = file[2 * i+1];
                    }
                }
                

            }
            catch (Exception)
            {

                // throw;
            }
            for (int i = 0; i < textBox_ZD.Length; i++)
            {
                textBox_ZD[i].TextChanged += FiletextBox_TextChange;
                textBox_BZ[i].TextChanged += FiletextBox_TextChange;
            }
            ChangeLanguage(translate.cur_language);
            try
            {
                string[] file = File.ReadAllLines(@"C:\Users\Public\Documents\Ebyte_Config3.txt");
                if (file.Length > 4)
                {
                    comboBox_Port.Text = file[0];
                    comboBox_Baud.Text = file[1];
                    comboBox_StopBit.Text = file[2];
                    comboBox_DataBits.Text = file[3];
                    comboBox_Parity.Text = file[4];
                   
                }
            }
            catch (Exception)
            {

                //  throw;
            }
            comboBox_Port.SelectedIndexChanged += ComboBox_Serial_SelectedIndexChanged;
            comboBox_StopBit.SelectedIndexChanged += ComboBox_Serial_SelectedIndexChanged;
            comboBox_Parity.SelectedIndexChanged += ComboBox_Serial_SelectedIndexChanged;
            comboBox_DataBits.SelectedIndexChanged += ComboBox_Serial_SelectedIndexChanged;
            comboBox_DataBits.SelectedIndexChanged += ComboBox_Serial_SelectedIndexChanged;
        }

        private void ComboBox_Serial_SelectedIndexChanged(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            FileInfo serial = new FileInfo(@"C:\Users\Public\Documents\Ebyte_Config3.txt");
            StreamWriter sw = serial.CreateText();
            sw.Write(comboBox_Port.Text+"\r\n");
            sw.Write(comboBox_Baud.Text + "\r\n");
            sw.Write(comboBox_StopBit.Text + "\r\n");
            sw.Write(comboBox_DataBits.Text + "\r\n");
            sw.Write(comboBox_Parity.Text + "\r\n");
            sw.Close();
            try
            {
                serialPort.Close();   
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
              //  throw;
            }
            SerialOpen();
        }

        private void FiletextBox_TextChange(object sender, EventArgs e)
        {
            FileInfo form_num = new FileInfo(@"C:\Users\Public\Documents\Ebyte_Config2.txt");
            StreamWriter sw = form_num.CreateText();
            string[] ZD = new string[textBox_ZD.Length];
            string[] BZ = new string[textBox_BZ.Length];
            for (int i = 0; i < textBox_ZD.Length; i++)
            {
                ZD[i] = textBox_ZD[i].Text;
                sw.Write(ZD[i]+"\r\n");
                BZ[i] = textBox_BZ[i].Text;
                sw.Write(BZ[i] + "\r\n");
            }
            sw.Close();
        }
        public void UdpserverStart()
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPEndPoint.Address,Port);
            EndPoint = iPEndPoint;
            xServer.Start();
        }
        
        public void UdpserverStop() { xServer.Stop(); }
        private void DataSend(string str,bool acsii)
        {
            if (NetFlag)
                NetSendData(str, acsii);
            else
                SerialPortSendData(str,acsii);
        }
        private void NetSendData(string str, bool acsii)
        {
            UDPPacketBuffer buffer=null;
            if (acsii)
            {
                buffer = new UDPPacketBuffer(Encoding.ASCII.GetBytes(str), EndPoint);
            }
            else
            {
                byte[] Hex = funs.HexString2Bytes(str);
                buffer = new UDPPacketBuffer(Hex, EndPoint);
            }

            SendConter += buffer.DataLength;
            label_SendCouter.Text = "S:" + SendConter.ToString();
            
            xServer.AsyncBeginSend(buffer);
        }
        private void SerialPortSendData(string str,bool ascii)
        {

            if (ascii)
            {
                serialPort.Write(str);
                SendConter += str.Length;
                // label_SendCouter.Text = "S:" + SendConter.ToString();
            }
            else
            {
                byte[] Hex = funs.HexString2Bytes(str);
                SendConter += Hex.Length;
                serialPort.Write(Hex, 0, Hex.Length);
            }
            label_SendCouter.Text = "S:" + SendConter.ToString();

        }
        private void SerialOpen()
        {
            try
            {
                if (comboBox_Port.Text != "")
                serialPort.PortName = comboBox_Port.Text;
            serialPort.BaudRate = int.Parse(comboBox_Baud.Text);
            serialPort.DataBits = int.Parse(comboBox_DataBits.Text);
            serialPort.Parity = (Parity)(comboBox_Parity.SelectedIndex);
            serialPort.StopBits = (StopBits)(comboBox_StopBit.SelectedIndex + 1);
          
                serialPort.Open();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                return;
                //throw;
            }

            button_SerialSwitch.Name = "button_SerialClose";
            serialPort.DataReceived += SerialPort_DataReceived;
            button_SerialSwitch.Text = translate.getInfo("e70", button_SerialSwitch.Name);
            button_SerialSwitch.ForeColor = Color.Red;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
            Button button = (Button)sender;
            switch (button.Name)
            {
                case"button_SerialOpen":
                    SerialOpen();
                    break;
                case "button_SerialClose":
                    button_SerialSwitch.Name = "button_SerialOpen";
                    serialPort.DataReceived -= SerialPort_DataReceived;
                    button_SerialSwitch.Text = translate.getInfo("e70", button_SerialSwitch.Name);
                    button_SerialSwitch.ForeColor = Color.Black;
                    serialPort.Close();
                    break;
                default:
                    break;
            }
        }

        public static void Delay1ms(int milliSecond)
        {
            int start = Environment.TickCount;
            int time = Math.Abs(Environment.TickCount - start);
            while (time < milliSecond)
            {

                if (time > 20)
                    Thread.Sleep(20);
                else
                   Thread.Sleep(time);
                Application.DoEvents();
                time = Math.Abs(Environment.TickCount - start);
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            serialPort.DataReceived -= SerialPort_DataReceived;
            int bytes = serialPort.BytesToRead;
            Delay1ms(5);//延时5ms
            while ((bytes < serialPort.BytesToRead) && (bytes < serialPort.ReadBufferSize))
            {
                bytes = serialPort.BytesToRead;
                Delay1ms(5);//延时5ms
            }
            byte[] buf = new byte[bytes];
            serialPort.Read(buf, 0, bytes);
            _syncContext.Post(SerialRecvData, buf);
            serialPort.DataReceived += SerialPort_DataReceived;

        }
        private void SerialRecvData(object data)
        {
            byte[] Data = (byte[])data;
            TextBox_Rev(Data, Data.Length);
        }
        public void _NetRecvData(UDPPacketBuffer Data)
        {
            _syncContext.Post(NetRecvData, Data);
        }
        private void NetRecvData(object data)
        {
            UDPPacketBuffer buff = (UDPPacketBuffer)data;
            TextBox_Rev(buff.Data, buff.DataLength);
        }
        private void TextBox_Rev(byte[] data,int length)
        {
            RecvConter += length;
            label_RecvCounter.Text = "R:" + RecvConter.ToString();
            string recv=null;
            if (radioButton1.Checked)
                recv = Encoding.ASCII.GetString(data,0,length);
            else
            {
                string[] hex = new string[length];
                for (int i=0;i<length;i++)
                {
                    int value = Convert.ToInt32(data[i]);
                    hex[i] = String.Format("{0:X2}", value);
                }
                
                recv = string.Join(" ",hex);
                if (textBox_Rev.Text.Length > 0)
                    textBox_Rev.AppendText(" ");
            }
            
            textBox_Rev.AppendText(recv);
        }
        private void BindCbox(ComboBox comboBox)
        {
           // comboBox.Items.Clear();
            IList<Info> infoList = new List<Info>();
            string Dis= translate.getInfo("e70", comboBox.Name+"_Dis");
            string[] name = System.Text.RegularExpressions.Regex.Split(translate.getInfo("e70", comboBox.Name + "_Dis"), "\r\n");
            string []id= System.Text.RegularExpressions.Regex.Split(translate.getInfo("e70", comboBox.Name + "_Val"), "\r\n"); 
            if (id.Length != name.Length)
                MessageBox.Show(comboBox.Name+"指令录入错误");
            for (int i = 0; i < id.Length; i++)
            {
                Info info = new Info() { Id = id[i]+"\r\n", Name = name[i] };
                infoList.Add(info);
            }
            
            comboBox.DataSource = infoList;
            comboBox.ValueMember = "Id";
            comboBox.DisplayMember = "Name";
        }
        private void comboBox_Port_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox_Port.Items.Clear();
            string[] portName = SerialPort.GetPortNames();
            comboBox_Port.Items.AddRange(portName);
        }

        private void button_RecvClear_Click(object sender, EventArgs e)
        {
            textBox_Rev.Text = null;
            RecvConter = 0;
            label_RecvCounter.Text = "R:0";
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel_AT1.Visible = true;
            panel_AT2.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel_AT2.Visible = true;
            panel_AT1.Visible = false;
        }

        private void button_AT_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string data=(string)button.Tag;
            if (data != "+++")
                data += "\r\n";
            ///textBox_Rev.Text += data;
            ///
            try
            {
                DataSend(data, true);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
             //   throw;
            }
          
        }
        private void button_Data_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int num = int.Parse(button.Text);
            string data;
            if (num < 4)
            {
                TextBox textBox= (TextBox)Controls.Find("textBox_AT" + (num-1).ToString(), true)[0];
                data = textBox.Tag+textBox.Text + "\r\n";
            }
            else
            {
                ComboBox comboBox= (ComboBox)Controls.Find("comboBox_AT" + (num - 4).ToString(), true)[0];
                data = (string)(comboBox.SelectedValue);
            }
            //textBox_Rev.Text += data;
            try
            {
                DataSend(data, true);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                //throw;
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DataSend("+++", true);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                return;
                //   throw;
            }
           
            Delay1ms(100);
            for (int i = 0; i < 14; i++)
            {
                CheckBox checkBox= (CheckBox)Controls.Find("checkBox_AT" + i.ToString(), true)[0];
                if (checkBox.Checked)
                {
                    button_Data_Click((Button)Controls.Find("button_Data" + i.ToString(), true)[0],  e);
                    Delay1ms(1000);
                }
            }
            DataSend("AT+RSTART\r\n", true);
        }

        private string newline()
        {
            if (checkBox1.Checked)
            {
                if (radioButton4.Checked)
                    return "\r\n";
                else
                    return "0D0A";

            }
            else
                return null;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void textBox_Rev_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void button_zd_Click(object sender, EventArgs e)
        {
            
            TextBox textBox = (TextBox)Controls.Find("textBox_ZD" + (int.Parse(((Button )sender).Text)-1).ToString(), true)[0];
            try
            {
                DataSend(textBox.Text + newline(), radioButton4.Checked);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
               // throw;
            }
           
        }
        private void button_zd1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        void Panel3Add(int page)
        {
            panel3.Controls.Clear();
           
            for (int i = page * 4; i < page * 4 + 4; i++)
            {
                panel3.Controls.Add(button_ZD[i]);
                panel3.Controls.Add(textBox_BZ[i]);
                panel3.Controls.Add(textBox_ZD[i]);
                panel3.Controls.Add(label_BZ[i]);
            }
            label_page.Text = "Page:" + (page+1).ToString();
        }
        private void buttonPage_Click(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            switch (button.Name)
            {
                case "button_PageFist":
                    Page=0;
                    break;
                case "button_PageEnd":
                    Page = 9;
                    break;
                case "button_PageAdd":
                    Page ++;
                    break;
                case "button_PageSub":
                    Page--;
                    break;
                default:
                    break;
            }
            if (Page > 9) Page = 0;
            if (Page < 0) Page = 9;
            Panel3Add(Page);
           
        }
        

        private void button_zd4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                textBox_Rev.Text = funs.HexToStr(textBox_Rev.Text);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                byte[] data = Encoding.ASCII.GetBytes(textBox_Rev.Text);
                string[] hex = new string[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    //  int value = Convert.ToInt32(data[i]);
                    hex[i] = String.Format("{0:X2}", data[i]);
                }
                string recv = string.Join(" ", hex);
                textBox_Rev.Text = recv;
            }
        }

        private void button_Send_Click(object sender, EventArgs e)
        {
            try
            {
                DataSend(textBox_Send.Text + newline(), radioButton4.Checked);
            }
            catch (Exception r)
            {
                MessageBox.Show(r.Message);
                //throw;
            }
           
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                timer1.Interval = int.Parse(textBox1.Text);
                timer1.Enabled = true;
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void button_SendClear_Click(object sender, EventArgs e)
        {
            textBox_Send.Text = null;
            SendConter = 0;
            label_SendCouter.Text = "S:0";
        }

        private void button_AT3_Click(object sender, EventArgs e)
        {
            if (NetFlag)
            {
                button_AT3.Click -= button_AT3_Click;
                byte[] data = { 0x03, 0x00 };
                udpclient.send(Mac,0x04fe, data);
                Delay1ms(1600);
                DataSend( "AT", true);
                Delay1ms(103);
                DataSend("+++", true);
                Delay1ms(20);
                DataSend(button_AT3.Tag + "\r\n", true);
                Delay1ms(20);
                data[0] = 0;
                udpclient.send(Mac, 0x04fe, data);
                button_AT3.Click += button_AT3_Click;
            }
            else
            {
                try
                {
                    DataSend(button_AT3.Tag + "\r\n", true);
                }
                catch (Exception r)
                {
                    MessageBox.Show(r.Message);
                   // throw;
                }
               
            }
        }
       
        private void keyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                return;
            }
           
            
        }

        private void textBox_TextChange(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            int num;
            if (textBox.Text == "")
                textBox.Text = "0";
            num = int.Parse(textBox.Text);
            if (textBox.Name == "textBox1")
            {
                if (num == 0)
                    textBox.Text = "1";
                return;
            }
            if (num > 65535)
                textBox.Text = "65535";

        }

        private void textBox_Send_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool flag = true;
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F'))
                flag = false;       
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8&&e.KeyChar!=' '&&flag)
            {
                e.Handled = true;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox_Send.KeyPress += textBox_Send_KeyPress;
                byte[] data = Encoding.ASCII.GetBytes(textBox_Send.Text);
                string[] hex = new string[data.Length];
                for (int i = 0; i < data.Length; i++)
                {
                    hex[i] = String.Format("{0:X2}", data[i]);
                }
                string recv = string.Join(" ", hex);
                textBox_Send.Text = recv;      
            }
           
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox_Send.KeyPress -= textBox_Send_KeyPress;
                textBox_Send.Text = funs.HexToStr(textBox_Send.Text);
            }
        }

        private void button_WindowsSave_Click(object sender, EventArgs e)
        {
            string fileName = "";
            saveFileDialog1.FileName =
                saveFileDialog1.InitialDirectory + "\\" + fileName;
            saveFileDialog1.Title = "Save As";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, textBox_Rev.Lines);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(textBox1.Text);
            try
            {
                DataSend(textBox_Send.Text + newline(), radioButton4.Checked);
            }
            catch (Exception r)
            {
                timer1.Enabled = false;
                MessageBox.Show(r.Message);
               
            }
        }
    }

    internal class Info
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
public class SubmenuTUdpServer2 : udpserver//子菜单的UDPServer
{
    public E70 UIForm ;
    public SubmenuTUdpServer2(int port)
        : base(port)
    {
        Console.WriteLine("Server Start...@" + port.ToString());
    }

    

    protected override void PacketReceived(UDPPacketBuffer buffer)
    {
        UIForm._NetRecvData(buffer);
    }

    protected override void PacketSent(UDPPacketBuffer buffer, int bytesSent)
    {
    }
   
}
