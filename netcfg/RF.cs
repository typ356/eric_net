using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace netcfg
{
   
    class RF
    {
        public string[]Models;
        string[,] tbtxt0 ={

        {"产品型号：" ,"Product Model:" },
        {"\r\n\r\n射频固件版本：" ,"\r\n\r\nRF Version:" },
        {"\r\n\r\n射频发射功率：" ,"\r\n\r\nRF Power" },
        {"\r\n\r\n射频工作频率：" ,"\r\n\r\nRF Work Freq:" },
        {"\r\n\r\n射频功能特点：" ,"\r\n\r\nFeatures:" },

        };
        double Fre = 0;
        double Fre_Step = 0;
        byte Fre_Min = 0;
        byte Fre_Max = 0;
        string[,] tbtxt1 ={

        {"当前波特率：" ,"Present Baud:" },
        {"\r\n\r\n当前射频空中速率：" ,"\r\n\r\nPresent Air Speed:" },
        {"\r\n\r\n当前射频发射功率：" ,"\r\n\r\nPresent Tx Power:" },
        {"\r\n\r\n当前射频工作频率：" ,"\r\n\r\nPresent Frequence:" },
        {"\r\n\r\n当前射频地址设定：0x" ,"\r\n\r\nPresent Adress Set: 0x" },

        };
        StreamReader _textStreamReader;
        Assembly _assembly= Assembly.GetExecutingAssembly();
        private string ConfigFile ;
        public static string[] _ConfigFile;// = ConfigFile.Split('*');
        public int ConfigFileIndex = 0;
        static int version = 0;
        public  UInt16 MTU = 0;
        public RF()
        {
            _textStreamReader =new StreamReader( _assembly.GetManifestResourceStream("netcfg.Resources.配置.txt"));
            ConfigFile = _textStreamReader.ReadToEnd();
            string[] str = ConfigFile.Split('*');
            Models = new string[str.Length / 2];
            for (int i = 1; i < str.Length; i += 2)
                Models[i/2] = str[i];
        }
        public void ControlDisView(Label[] label, TextBox[] textBox, ComboBox[] comboBox)//将所有控件隐藏
        {
            for (int i = 0; i < label.Length; i++)
            {
                label[i].Visible = false;
            }
            for (int i = 0; i < textBox.Length; i++)
            {
                textBox[i].Visible = false;
            }
            for (int i = 1; i < comboBox.Length; i++)
            {
                comboBox[i].Visible = false;
            }
        }
        public void ControlEnable(Label[] label, TextBox[] textBox, ComboBox[] comboBox,bool enable)//将所有控件隐藏
        {
            for (int i = 0; i < label.Length; i++)
            {
                label[i].Enabled = enable;
            }
            for (int i = 0; i < textBox.Length; i++)
            {
                textBox[i].Enabled = enable;
            }
            for (int i = 1; i < comboBox.Length; i++)
            {
                comboBox[i].Enabled = enable;
            }
        }
       
        public byte[] RfConfigData(TextBox[] textBox, ComboBox[] comboBox)
        {
            int rfClass = GetClass();
            byte[] data = new byte[8];
            if (rfClass == 1)
            {
                data[0] = (byte)GetConfigMode();
                data[1] = 0x06;
                data[2] = 0xC0;
                data[3] = (byte)(Convert.ToInt16(textBox[2].Text, 16) >> 8);
                data[4] = (byte)(Convert.ToInt16(textBox[2].Text, 16));
                data[5] = (byte)(comboBox[2].SelectedIndex << 6 | comboBox[3].SelectedIndex << 3 | comboBox[4].SelectedIndex);
                data[6] = Convert.ToByte(textBox[3].Text);
                if (data[6] < Fre_Min)
                    data[6] = Fre_Min;
                if (data[6] > Fre_Max)
                    data[6] = Fre_Max;
                data[7] = (byte)(comboBox[5].SelectedIndex << 7 | comboBox[6].SelectedIndex << 3/* | comboBox[7].SelectedIndex << 2 */| comboBox[8].SelectedIndex);
                return data;
            }
            if (rfClass == 2)
            {
                data[0] = (byte)GetConfigMode();
                data[1] = 0x06;
                data[2] = 0xC0;
                data[3] = (byte)(Convert.ToInt16(textBox[2].Text, 16) >> 8);
                data[4] = (byte)(Convert.ToInt16(textBox[2].Text, 16));
                data[5] = (byte)(comboBox[2].SelectedIndex << 6 | comboBox[3].SelectedIndex << 3 | comboBox[4].SelectedIndex);
                data[6] = Convert.ToByte(textBox[3].Text);
                if (data[6] < Fre_Min)
                    data[6] = Fre_Min;
                if (data[6] > Fre_Max)
                    data[6] = Fre_Max;
                int transmit = comboBox[6].SelectedIndex;
                if (transmit == 1)
                    transmit = 2;
                data[7] = (byte)(comboBox[5].SelectedIndex << 7 | transmit << 3 | comboBox[7].SelectedIndex << 2 | comboBox[8].SelectedIndex);
                return data;
            }
            return data;
        }
        public void  RfConfigDataParse(TextBox[] textBox, ComboBox[] comboBox, byte[] data)
        {
            int rfClass = GetClass();
            if (rfClass == 1)
            {
                textBox[2].Text = Convert.ToString(data[11] << 8 | data[12], 16);
                comboBox[2].SelectedIndex = data[13] >> 6;
                comboBox[3].SelectedIndex = (data[13] & 0x38) >> 3;
                comboBox[4].SelectedIndex = data[13] & 0x7;
                textBox[3].Text = Convert.ToString(data[14]);
                comboBox[5].SelectedIndex = data[15] >> 7;
                comboBox[6].SelectedIndex = (data[15] & 0x38) >> 3;
                comboBox[8].SelectedIndex = data[15] & 0x3;
            }
            if (rfClass == 2)
            {
                textBox[2].Text = Convert.ToString(data[11] << 8 | data[12], 16);
                comboBox[2].SelectedIndex = data[13] >> 6;
                comboBox[3].SelectedIndex = (data[13] & 0x38) >> 3;
                comboBox[4].SelectedIndex = data[13] & 0x7;
                textBox[3].Text = Convert.ToString(data[14]);
                comboBox[5].SelectedIndex = data[15] >> 7;
                if (((data[15] & 0x38) >> 3) != 2)
                    comboBox[6].SelectedIndex = 0;
                else
                    comboBox[6].SelectedIndex = 1;
                comboBox[7].SelectedIndex = (data[15] & 0x4) >> 2;
                comboBox[8].SelectedIndex = data[15] & 0x3;
            }
        }
        public void Show(TextBox[] textBox, ComboBox[] comboBox, int language,int model)
        {
            string []str= _ConfigFile[6].Split(':')[1].Split(',')[language].Split('/');
            string tbStr1, tbStr2;
            tbStr1 = tbtxt0[0, language];
            tbStr1 += ConfigFile.Split('*')[2*model-1];
            tbStr1 += tbtxt0[1, language];
            tbStr1 += string.Format("{0:F1}",(float.Parse( Convert.ToString(version, 16)))/10);
            tbStr1 += tbtxt0[2, language];
            tbStr1 += str[0];
            tbStr1 += tbtxt0[3, language];
            tbStr1 += str[1];
            tbStr1 += tbtxt0[4, language];
            tbStr1 += str[2];

            tbStr2 = tbtxt1[0, language];
            tbStr2 += comboBox[3].Text;
            tbStr2 += tbtxt1[1, language];
            tbStr2 += comboBox[4].Text;
            tbStr2 += tbtxt1[2, language];
            tbStr2 += comboBox[8].Text;
            tbStr2 += tbtxt1[3, language];
            tbStr2 += Convert.ToString(Fre+Fre_Step*int.Parse(textBox[3].Text))+"MHz";
            tbStr2 += tbtxt1[4, language];
            tbStr2 += textBox[2].Text;

            textBox[0].Text = tbStr1;
            textBox[1].Text = tbStr2;

        }
        public void GetConfigFile(int index)
        {
             string[] configFile = ConfigFile.Split('*');
            _ConfigFile = configFile[(index) *2 + 2].Split('#');
        }
        public void ParseConfigFile(Label[] label, TextBox[] textBox, ComboBox[] comboBox,int language)//language为语言选择项
        {      
            for (int i = 7; i < _ConfigFile.Length - 1; i++)
            {
                string[] configFile = _ConfigFile[i].Split(':');
                char[] s = configFile[0].Split('@')[0].ToCharArray();
                int _index = int.Parse(configFile[0].Split('@')[1]);
                switch (s[s.Length-1])
                {
                    case ('L'):
                        label[_index].Visible = true;
                        label[_index].Enabled = true;
                        label[_index].Text = configFile[1].Split(',')[language];
                        break;
                    case ('T'):
                        if (_index == 3)
                        {
                            string[] fre = configFile[1].Split(',');
                            Fre = Convert.ToDouble(fre[0]);
                            Fre_Step= Convert.ToDouble(fre[1]);
                            Fre_Min = Convert.ToByte(fre[2]);
                            Fre_Max = Convert.ToByte(fre[3]);
                        }
                        textBox[_index].Visible = true;
                        textBox[_index].Enabled = true;
                        break;
                    case ('C'):
                        if ((_index + 1) == 1)
                        {
                            comboBox[_index + 1].DropDownStyle = ComboBoxStyle.DropDownList;
                            comboBox[_index + 1].Visible = true;
                            comboBox[_index + 1].Enabled = true;
                            comboBox[_index + 1].Items.Clear();
                            string[] bb= { "" };
                            string[] tmp;
                            tmp = configFile[1].Split(',')[language].Split('/');
                            if (tmp.Length == 1)
                            {

                                bb[0] = configFile[1].Split(',')[language].ToString();
                                comboBox[_index + 1].DropDownStyle = ComboBoxStyle.Simple;
                      
                                comboBox[_index + 1].Items.Add(bb[0]);
                                comboBox[_index + 1].Items.Add(bb[0]);
                            }
                            else
                            {
                                comboBox[_index + 1].Items.AddRange(tmp);
                            }

                            //comboBox[_index + 1].Items.Add("");//[language].Split('/'));
                            //comboBox[_index + 1].Items.Add(bb);//[language].Split('/'));


                        }
                        else
                        {
                            comboBox[_index + 1].DropDownStyle = ComboBoxStyle.DropDownList;
                            comboBox[_index + 1].Visible = true;
                            comboBox[_index + 1].Enabled = true;
                            comboBox[_index + 1].Items.Clear();
                            comboBox[_index + 1].Items.AddRange(configFile[1].Split(',')[language].Split('/'));
                        }


                        break;
                    default:
                        break;
                }
            }
        }
        public int GetClass()//
        {
            return int.Parse(_ConfigFile[0].Split(':')[1].Split(',')[0]);
        }
        public int GetVersion(byte[]data)
        {
            byte Class = byte.Parse(_ConfigFile[3].Split(':')[1].Split(',')[0]);
            long model = Convert.ToInt64(_ConfigFile[3].Split(':')[1].Split(',')[1], 16);
            
            switch (Class)
            {
                case 3:
                    if (model != (data[11] << 8 | data[13]))
                        return -1;
                    version = data[12];
                    break;
                case 2:
                    if (model != (data[11] << 8 | data[12]))
                        return -1;
                    version = data[13];
                    break;
                case 1:
                    long ver = ((long)(data[11]) << 32 | (long)(data[12]) << 24 | (long)(data[14]) << 16 | (long)(data[15]) << 8 | (long)(data[16]));
                    if (model != ver)
                        return -1;
                    version = data[13];
                    break;
                default:
                    version = -1;
                    break;
            }
            return version;
        }
        public int GetConfigMode()
        {
            return int.Parse(_ConfigFile[1].Split(':')[1].Split(',')[0]);
        }
        public int GetConfigBaudRate()
        {
            return int.Parse(_ConfigFile[2].Split(':')[1]);
        }
       
       public byte[] GetFactorySettings()
       {
            string[] factorySettings = _ConfigFile[5].Split(':')[1].Trim(' ').Split(' ');
            byte[] data = new byte[factorySettings.Length+2];
            data[0] = (byte)GetConfigMode();
            data[1] = (byte)factorySettings.Length;
            for (int i = 0; i < factorySettings.Length; i++)
            {
                data[i+2] = Convert.ToByte(factorySettings[i],16);
            }
            return data;
       }
        public UInt16 GetMTU()
        {
                int mtu = int.Parse(_ConfigFile[4].Split(':')[1]) << 1;
                if (mtu > 0)
                    return (ushort)(1| mtu);
                else
                    return (200<<1);
          
        }
    }
}
