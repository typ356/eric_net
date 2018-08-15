using System;
using System.IO;
using System.Windows.Forms;

namespace netcfg
{
    public partial class HomePage : Form
    {
        netForm netform;
        E70 e70;
        string Form_Num = "NULL";
        public HomePage()
        {
            InitializeComponent();
            netform = new netForm();
            netform.UdpserverStop();
            e70 = new E70();
            try
            {
                Form_Num = File.ReadAllText(@"C:\Users\Public\Documents\Ebyte_Config1.txt");
                if (Form_Num == "3")
                {
                    netcfg.E830_ETH2A.E830ETH_Flag = true;
                }
                else
                {
                    netcfg.E830_ETH2A.E830ETH_Flag = false;
                }
            }
            catch (Exception)
            {

            }
           
            object sender = new object();
            EventArgs e = new EventArgs();
            switch (Form_Num)
            {
                case "1":
                    eToolStripMenuItem_Click(sender, e);
                    break;
                case "2":
                    e70ToolStripMenuItem_Click(sender, e);
                    break;
                case "3":
                    E830toolStripMenuItem_Click(sender, e);
                    break;
                default:
                    eToolStripMenuItem_Click(sender, e);
                    break;
            }
        
        }
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem x = (ToolStripMenuItem)sender;
            int Language = int.Parse((string)x.Tag);
            ChangeLanguage(Language);
            switch (Form_Num)
            {
                case "1":
                    netform.ChangeLanguage(Language);
                    break;
                case "2":
                   e70.ChangeLanguage(Language);
                    break;
                default:
                    netform.ChangeLanguage(Language);
                    break;
            }
        }
        public void ChangeLanguage(int index)
        {
            translate.cur_language = index;
            this.Text = translate.getInfo("HomePage", "HomePage");
            toolStripButton1.Text = translate.getInfo("HomePage", "toolStripButton1");
            toolStripDropDownButton2.Text= translate.getInfo("HomePage", "toolStripDropDownButton2");
        }

        private void eToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo form_num = new FileInfo(@"C:\Users\Public\Documents\Ebyte_Config1.txt");
            StreamWriter sw = form_num.CreateText();
            netcfg.E830_ETH2A.E830ETH_Flag = false;
            Form_Num = "1";
            sw.Write("1");
            sw.Close();
            netform.Close();
            netform.UdpserverStop();
            netform = new netForm();
            Width = netform.Width;
            Height = netform.Height + 30;
            netform.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            netform.TopLevel = false; //指示子窗体非顶级窗体
            panel1.Controls.Clear();
            panel1.Controls.Add(netform);
            netform.Show();
        }

        private void e70ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo form_num = new FileInfo(@"C:\Users\Public\Documents\Ebyte_Config1.txt");
            StreamWriter sw = form_num.CreateText();
            netcfg.E830_ETH2A.E830ETH_Flag = false;
            Form_Num = "2";
            sw.Write('2');
            sw.Close();
            e70.Exit();
            e70.Close();
            e70 = new E70();
            Width = e70.Width;
            Height = e70.Height + 30;
            e70.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            e70.TopLevel = false; //指示子窗体非顶级窗体
            panel1.Controls.Clear();
            panel1.Controls.Add(e70);
            e70.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void E830toolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo form_num = new FileInfo(@"C:\Users\Public\Documents\Ebyte_Config1.txt");
            StreamWriter sw = form_num.CreateText();
            netcfg.E830_ETH2A.E830ETH_Flag = true;
            Form_Num = "3";
            sw.Write("3");
            sw.Close();
            netform.Close();
            netform.UdpserverStop();
            netform = new netForm();
            Width = netform.Width;
            Height = netform.Height + 30;
            netform.FormBorderStyle = FormBorderStyle.None; //隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            netform.TopLevel = false; //指示子窗体非顶级窗体
            panel1.Controls.Clear();
            panel1.Controls.Add(netform);
            netform.Show();
        }
    }
}
