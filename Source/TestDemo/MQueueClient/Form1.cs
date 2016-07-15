using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MQueueClient
{
    public partial class Form1 : Form
    {

        MessageQueue _messageQueue = null;

        string path = ".\\Private$\\MSMQDemo";
        public Form1()
        {
            InitializeComponent();


            if(MessageQueue.Exists(path))
            {
                MessageQueue.Delete(path);
                
            }

            _messageQueue = MessageQueue.Create(path);

            this.txt_path.Text = path;
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = DateTime.Now.ToString()+"发来的消息:"+this.textBox1.Text;
            System.Messaging.Message ms = new System.Messaging.Message();
            ms.Label = "HeBianGu";
            ms.Body = this.textBox1.Text;
            _messageQueue.Send(ms);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
