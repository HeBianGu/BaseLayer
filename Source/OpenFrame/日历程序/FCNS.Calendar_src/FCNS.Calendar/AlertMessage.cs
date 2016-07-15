using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FCNS.Calendar
{
    public partial class AlertMessage : Form
    {
        Voice v;
        public AlertMessage(string title,string timeSpan,string summary,bool voice)
        {
            InitializeComponent();
            
            this.Text = title;
            textBoxSummary.AppendText(timeSpan);
            textBoxSummary.AppendText("\r\n\r\n");
            textBoxSummary.AppendText(summary);

            v = new Voice();
            if (voice)
            {
                v.Open((string)Main.appConfig.Get("AlertFile", (string)AppDomain.CurrentDomain.BaseDirectory + "alert.mid"));
                v.Play();
            }
        }

        private void AlertMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            v.Stop();
        }
    }
}