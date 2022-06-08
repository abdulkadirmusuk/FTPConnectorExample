using System;
using System.Text;
using System.Windows.Forms;

namespace FTPConnectorExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            comboBox1.SelectedIndex = 0; //Default Proxy Type
            comboBox2.SelectedIndex = 0; //Default Protocol Type
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder logs = new StringBuilder();
            textBox5.Text = string.Empty;
            int portInfo = !string.IsNullOrWhiteSpace(textBox4.Text.Trim()) ? Convert.ToInt32(textBox4.Text.Trim()) : 0;

            var connectionInfo = new ConnectionInfo
            {
                HostName = textBox1.Text.Trim(),
                Port = portInfo,
                UserName = textBox2.Text.Trim(),
                Password = textBox3.Text.Trim(),
                ProxyHostName = textBox6.Text.Trim(),
                ProxyPort = textBox7.Text.Trim(),
                ProxyType = comboBox1.SelectedIndex,
                ProxyUser = textBox8.Text.Trim(),
                ProxyPassword = textBox9.Text.Trim()
            };
            var result = string.Empty;
            logs.AppendLine($"Please wait, connecting to [ {connectionInfo.HostName} ] ...");
            textBox5.Text = logs.ToString();
            if (comboBox2.SelectedIndex == 0)
            {
                result = new FTPConnector(connectionInfo).Connect();
            }
            else
            {
                connectionInfo.SshKey = textBox10.Text.Trim();
                result = new SFTPConnector(connectionInfo).Connect();
            }
            logs.AppendLine(result);
            textBox5.Text = logs.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            if (comboBox2.SelectedIndex == 1)
            {
                textBox1.Text = "192.168.1.78 ";
                textBox2.Text = "user";
                textBox4.Text = "2222";
                textBox3.Text = "password";
                textBox6.Text = "192.168.1.78";
                textBox7.Text = "1080";
                textBox10.Text = "ssh-rsa 2048 VE7VTxKMMVXvFUWlxtA83NEQX888Yx4Y/c+m/5CSgPM=";
            }
            else
            {
                textBox1.Text = "test.rebex.net";
                textBox2.Text = "demo";
                textBox3.Text = "password";
                textBox6.Text = "192.252.215.5";
                textBox7.Text = "16137";
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                textBox10.Visible = true;
                label13.Visible = true;
            }
            if (comboBox2.SelectedIndex == 0)
            {
                textBox10.Visible = false;
                label13.Visible = false;
            }
        }
    }
}
