using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;

namespace SendEmail
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ImeMode = ImeMode.Hangul;
        }

        //�����Load�¼�
        private void Form1_Load(object sender, EventArgs e)
        {
            //�������smpt������������
            cmbBoxSMTP.Items.Add("smtp.163.com");
            cmbBoxSMTP.Items.Add("smtp.gmail.com");
            //����Ϊ�����б�
            cmbBoxSMTP.DropDownStyle = ComboBoxStyle.DropDownList;
            //Ĭ��ѡ�е�һ��ѡ��
            cmbBoxSMTP.SelectedIndex = 0;
            //�������������Ҫ��ʼ�������ݣ�������ʾ�������û�����

        }

        //��Ӱ�ť�ĵ����¼�
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //���岢��ʼ��һ��OpenFileDialog��Ķ���
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Application.StartupPath;
            openFile.FileName = "";
            openFile.RestoreDirectory = true;
            openFile.Multiselect = false;

            //��ʾ���ļ��Ի��򣬲��ж��Ƿ񵥻���ȷ����ť
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                //�õ�ѡ����ļ���
                string fileName = openFile.FileName;
                //���ļ�����ӵ�TreeView��
                treeViewFileList.Nodes.Add(fileName);
            }
        }

        //ɾ����ť�ĵ����¼�
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //�ж��Ƿ�ѡ���˽ڵ�
            if (treeViewFileList.SelectedNode != null)
            {
                //�õ�ѡ��Ľڵ�
                TreeNode tempNode = treeViewFileList.SelectedNode;
                //ɾ��ѡ�еĽڵ�
                treeViewFileList.Nodes.Remove(tempNode);
            }
            else
            {
                MessageBox.Show("��ѡ��Ҫɾ���ĸ�����");
            }
        }

        //���Ͱ�ť�ĵ����¼�
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //ȷ��smtp��������ַ��ʵ����һ��Smtp�ͻ���
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(cmbBoxSMTP.Text);
                //����һ�����͵�ַ
                string strFrom = string.Empty;
                //if (cmbBoxSMTP.SelectedText == "smtp.163.com")
                    strFrom = txtUserName.Text + "@163.com";
                //else
                //    strFrom = txtUserName.Text + "@gmail.com";

                //����һ�������˵�ַ����
                MailAddress from = new MailAddress(strFrom, txtDisplayName.Text, Encoding.UTF8);
                //����һ���ռ��˵�ַ����
                MailAddress to = new MailAddress(txtEmail.Text, txtToName.Text, Encoding.UTF8);

                //����һ��Email��Message����
                MailMessage message = new MailMessage(from, to);

                //Ϊ message ��Ӹ���
                foreach (TreeNode treeNode in treeViewFileList.Nodes)
                {
                    //�õ��ļ���
                    string fileName = treeNode.Text;
                    //�ж��ļ��Ƿ����
                    if (File.Exists(fileName))
                    {
                        //����һ����������
                        Attachment attach = new Attachment(fileName);
                        //�õ��ļ�����Ϣ
                        ContentDisposition disposition = attach.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(fileName);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileName);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(fileName);
                        //���ʼ���Ӹ���
                        message.Attachments.Add(attach);
                    }
                    else
                    {
                        MessageBox.Show("�ļ�" + fileName + "δ�ҵ���");
                    }
                }

                //����ʼ����������
                message.Subject = txtSubject.Text;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = rtxtBody.Text;
                message.BodyEncoding = Encoding.UTF8;

                //�����ʼ�����Ϣ
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = false;

                //���������֧�ְ�ȫ���ӣ��򽫰�ȫ������Ϊtrue��
                //gmail֧�֣�163��֧�֣������gmail��һ��Ҫ������Ϊtrue
                if (cmbBoxSMTP.SelectedText == "smpt.163.com")
                    client.EnableSsl = false;
                else
                    client.EnableSsl = true;

                //�����û��������롣
                //string userState = message.Subject;
                client.UseDefaultCredentials = false;
                string username = txtUserName.Text;
                string passwd = txtPassword.Text;
                //�û���½��Ϣ
                NetworkCredential myCredentials = new NetworkCredential(username, passwd);
                client.Credentials = myCredentials;
                //�����ʼ�
                client.Send(message);
                //��ʾ���ͳɹ�
                MessageBox.Show("���ͳɹ�!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}