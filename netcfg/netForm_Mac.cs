using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace netcfg
{
    public partial class netForm_Mac : Form
    {
        public netForm_Mac()
        {
            InitializeComponent();

            this.Text = translate.getInfo("form3", "caption");
            button1.Text = translate.getInfo("form3", "btn_ok");
            button2.Text = translate.getInfo("form3", "btn_cancel");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] x = funs.Mac2Byte(textBox1.Text);
                this.DialogResult = DialogResult.OK; 
            }
            catch
            {
                MessageBox.Show(translate.getInfo("form3", "msg"));
            }
        }

        public void setMac(string _mac)
        {
            textBox1.Text = _mac;
        }

        public string GetMac()
        {
            return textBox1.Text;
        }

        private void Form3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
    }
}
