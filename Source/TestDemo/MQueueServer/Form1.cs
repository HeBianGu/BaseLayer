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

namespace MQueueServer
{
    public partial class Form1 : Form
    {

        MessageQueue _messageQueue = null;

        TimeSpan outTime = new TimeSpan(1000);

        Timer _timer = new Timer();
        public Form1()
        {
            InitializeComponent();


            MessageQueue[] mqs = MessageQueue.GetPrivateQueuesByMachine("HeBianGu");

            this.cmb_paths.DataSource = mqs;
            this.cmb_paths.DisplayMember = "Path";

            this.cmb_paths.SelectedIndex = 0;

            _messageQueue = this.cmb_paths.SelectedItem as MessageQueue;

            _timer.Interval = 1000;

            _timer.Enabled = false;

            _timer.Tick+=_timer_Tick;

        }

        private void _timer_Tick(object sender,EventArgs e)
        {
            try
            {
                System.Messaging.Message ms = _messageQueue.Receive(outTime);
                ms.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                this.richTextBox1.AppendText(ms.Body.ToString());
                this.richTextBox1.AppendText(Environment.NewLine);
            }
            catch (MessageQueueException ex)
            {
              //    没有消息了

            }
            catch(Exception ex)
            {
                _timer.Stop();
                MessageBox.Show(ex.Message, "接收失败！");
            }
            finally
            {
                //  记录运行日志
            }
        }
        private void btn_recive_Click(object sender, EventArgs e)
        {
            _timer.Enabled = !_timer.Enabled;

            if(_timer.Enabled)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
            btn_recive.Text =_timer.Enabled? "停止监控":"开始监控";
          
            
        }
    }
}
