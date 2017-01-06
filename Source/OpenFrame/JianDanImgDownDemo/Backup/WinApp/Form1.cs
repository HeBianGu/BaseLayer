using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Drawing.Imaging;

namespace WinApp
{
    public partial class Form1 : Form
    {
        BackgroundWorker _BackgroundWorker = new BackgroundWorker();
        delegate void ShowImgCallBack();
        delegate void DownLoadImgs(string url,string path);
        delegate void AddDownLoadResultCallBack(string url, bool success, long fileSize);
        delegate void EndDownLoadImgCallBack();
        TimeSpan _TS = new TimeSpan();
        bool _StopWorking = false;
        bool _IsStopped = true;
        Thread _ImgDownThread = null;

        string[] _UrlArr = { "http://jandan.net/ooxx/page-{0}#comments", "http://jandan.net/pic/page-{0}#comments" };

        string _SearchingTag = "正在获取第{0}页,第{1}张图片";
        string _ResultTag = "匹配到图片{0}张";
        string _DownLoadingTag = "成功下载{0}张，失败{1}张";
        string _DefaultSavePath = AppDomain.CurrentDomain.BaseDirectory + "JianDanImg\\";
        string _SavePath = string.Empty;

        private int _Timeout = 1000 * 10;
        private int _TotalCount = 0;//总下载数量
        private int _DownCount = 0;//已下载数量
        private int _SuccessCount = 0;//下载成功数量
        private int _FailCount = 0;//下载失败数量
        private List<string> _FailList = null;//下载失败列表
        private int _RetyrCount = 0;//已重试次数
        private int _RetyrAllowCount = 0;//允许重试次数
        private bool _ReDownBegin = false;//是否开始重试
        private List<string> _DownList = null;//下载列表

        Stopwatch _Stopwatch = null;
        StringBuilder logStringBuilder = new StringBuilder();


        public Form1()
        {
            InitializeComponent();
            cbType.Items.Insert(0, "妹子图");
            cbType.Items.Insert(1, "无聊图");
            cbType.SelectedIndex = 0;
            txtFrom.Text = "1";
            txtTo.Text = "5";
            InitLink();
            SetGetImgButtonIsBusy(false);
            txtSavePath.Text = _DefaultSavePath;
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            logStringBuilder = new StringBuilder();
            logStringBuilder.AppendLine("btnBegin_Click");

            _Stopwatch = new Stopwatch();
            _Stopwatch.Start();

            StartInif();

            _ImgDownThread = new Thread(new ThreadStart(delegate() { ShowImg();}));
            _ImgDownThread.IsBackground = true;
            _ImgDownThread.Start();
        }

        /// <summary>
        /// 初始化下载参数
        /// </summary>
        private void StartInif()
        {
            logStringBuilder.AppendLine("StartInif");
            SetGetImgButtonIsBusy(true);
            _TotalCount = _DownCount = _SuccessCount = _FailCount = _RetyrCount = _RetyrAllowCount = 0;
            _IsStopped = false;
            _StopWorking = false;
            _FailList = new List<string>();
            _DownList = new List<string>();
            _ReDownBegin = false;
            int.TryParse(txtTimeOut.Text, out _Timeout);
            if (_Timeout <= 0 || _Timeout > 120)
            {
                _Timeout = 10;
            }
            _Timeout = _Timeout * 1000;
            int.TryParse(txtRetryCount.Text, out _RetyrAllowCount);
            if (_RetyrAllowCount <= 0 || _RetyrAllowCount > 10)
            {
                _RetyrAllowCount = 1;
            }
            labComplete.Text = "";
            labDownResult.Text = "";
        }

        private void ShowImg()
        {
            if (this.InvokeRequired)
            {
                ShowImgCallBack cb = new ShowImgCallBack(ShowImg);
                this.Invoke(cb);
                return;
            }
            logStringBuilder.AppendLine("ShowImg");
            _SavePath = txtSavePath.Text;
            if (string.IsNullOrEmpty(_SavePath))
            {
                _SavePath = _DefaultSavePath;
            }
            if (!Directory.Exists(_SavePath))
            {
                Directory.CreateDirectory(_SavePath);
            }

            txtResultShow.Clear();
            //txtResultShow.Update();
            Thread.Sleep(10);
            LrdComm.Helper.WebCapture wc = new LrdComm.Helper.WebCapture();
            Regex reg = null;
            Match m = null;
            int intFrom = 1, intTo = 5;
            int.TryParse(txtFrom.Text, out intFrom);
            int.TryParse(txtTo.Text, out intTo);
            intFrom = intFrom > 0 ? intFrom : 1;
            intTo = intTo > 0 ? intTo : 5;
            txtFrom.Text = intFrom.ToString();
            txtTo.Text = intTo.ToString();
            string url = _UrlArr[cbType.SelectedIndex];
            bool threadStop = false;
            for (int i = intFrom; i <= intTo; i++)
            {
                if (threadStop) { EndDownLoadImg() ; break; }
                string webSource = string.Empty;
                wc.Get(string.Format(url, i.ToString()), Encoding.UTF8, out webSource);
                reg = new Regex(@"<ol\s*class=""commentlist""[^>]*>(((?<o>)<ol[^>]*>|(?<-o>)</ol>|(?:(?!</?ol)[\s\S]))*)(?(o)(?!))</ol>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                MatchCollection mc = reg.Matches(webSource);
                if (mc != null && mc.Count > 0)
                {
                    string commentlist = mc[0].Value;
                    reg = new Regex(@"<p\s*[^>]*>(((?<o>)<p[^>]*>|(?<-o>)</p>|(?:(?!</?p)[\s\S]))*)(?(o)(?!))</p>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    mc = reg.Matches(commentlist);
                    if (mc != null)
                    {
                        foreach (Match ma in mc)
                        {
                            string p = ma.Value;
                            reg = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>");
                            m = reg.Match(p);
                            string src = m.Groups["url"].Value;
                            if (src != null && src.Length > 0)
                            {
                                src = src.Substring(1, src.Length - 1);
                                src = src.Substring(0, src.IndexOf("\""));
                                if (!IsAllowTypeImg(src) || _DownList.Contains(src)) { continue; }
                                _DownList.Add(src);
                            }
                            else
                            {
                                src = "";
                            }
                            if (!string.IsNullOrEmpty(src))
                            {
                                if (_StopWorking)
                                {
                                    _ImgDownThread.Abort();
                                    threadStop = true;
                                    break;
                                }
                                _TotalCount++;
                                labComplete.Text = string.Format(_SearchingTag, i.ToString(), _TotalCount.ToString());
                                //labComplete.Update();
                                string fileName = src.Substring(src.LastIndexOf("/"), src.Length - src.LastIndexOf("/"));
                                DownLoadImg(src, _SavePath + fileName);
                            }                
                        }
                    }
                }
            }
            labComplete.Text = string.Format(_ResultTag, _TotalCount.ToString());
        }

        /// <summary>
        /// 使用HttpWebRequest下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeOut"></param>
        private void HttpWebRequestDelegate(string url, int timeOut)
        {
            logStringBuilder.AppendLine("HttpWebRequestDelegate:" + url);
            if (_StopWorking) { EndDownLoadImg(); return; }
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.AllowWriteStreamBuffering = false;
            IAsyncResult res = hwr.BeginGetResponse(new AsyncCallback(AsyncDownLoadImg), hwr);
            ThreadPool.RegisterWaitForSingleObject(res.AsyncWaitHandle, new WaitOrTimerCallback(TimeoutCallback), hwr, timeOut, true);
        }

        private void DownLoadImg(string url,string path)
        {
            if (this.InvokeRequired)
            {
                DownLoadImgs dl = new DownLoadImgs(DownLoadImg);
                this.Invoke(dl, url, path);
                return;
            }
            logStringBuilder.AppendLine("DownLoadImg:" + url);
            if (_StopWorking) { EndDownLoadImg(); return; }
            //txtResultShow.AppendText(string.Format("正在下载：{0}", url));
            txtResultShow.Text += string.Format("正在下载：{0}", url);
            try
            {
                #region  使用WebClient类下载 
                //using (WebClient wcDown = new WebClient())
                //{
                //    //wcDown.DownloadFile(url, path);//同步下载还是会假死
                //    wcDown.DownloadFileAsync(new Uri(url), path);
                //}
                #endregion
                HttpWebRequestDelegate(url, _Timeout);
            }
            catch (System.Exception ex)
            {

            }
            //txtResultShow.AppendText("\r\n");
            txtResultShow.Text += string.Format("\r\n");
            Application.DoEvents();
            Thread.Sleep(50);
        }
        private static void TimeoutCallback(object obj, bool timedOut)
        {
            if (timedOut)
            {
                HttpWebRequest req = (HttpWebRequest)obj;
                if (req != null)
                    req.Abort();//超时就挂起线程
            }
        }
        private void AsyncDownLoadImg(IAsyncResult asyncResult)
        {
            
            if (_StopWorking) { EndDownLoadImg(); return; }
            logStringBuilder.AppendLine("AsyncDownLoadImg");
            WebRequest request = (WebRequest)asyncResult.AsyncState;
            string url = request.RequestUri.ToString();
            try
            {
                WebResponse response = request.EndGetResponse(asyncResult);
                long cLength = response.ContentLength;
                using (Stream s = response.GetResponseStream())
                {
                   
                    string saveFileName = _SavePath + "/" + Guid.NewGuid() + url.Substring(url.LastIndexOf("/") + 1, url.Length - url.LastIndexOf("/") - 1);

                    //FileStream的Write方法适合全部图片
                    //Image的Save不适合gif图片(Save(saveFileName, ImageFormat.Gif)同样不适合)
                    //WebClient的DownloadFileAsync可以，但是扩展不好
                    System.IO.FileStream so = new System.IO.FileStream(saveFileName, System.IO.FileMode.Create);
                    byte[] by = new byte[1024];
                    int size = 0;
                    while ((size = s.Read(by, 0, by.Length)) > 0)
                    {
                        so.Write(by, 0, size);
                    }
                    so.Close();


                    //Image img = Image.FromStream(s);
                    //img.Save(saveFileName);
                    //img.Dispose();

                    //WebClient wc = new WebClient();
                    //wc.DownloadFileAsync(new Uri(url), saveFileName);

                    s.Close();
                }

                AddDownLoadResult(url, true, cLength);
            }
            catch (Exception ex)
            {
                AddDownLoadResult(url, false, 0);
            }
        }
        private void AddDownLoadResult(string url,bool success,long fileSize)
        {
            if (this.InvokeRequired)
            {
                AddDownLoadResultCallBack cb = new AddDownLoadResultCallBack(AddDownLoadResult);
                this.Invoke(cb, url, success, fileSize);
                return;
            }
            if (_StopWorking) { return; }
            logStringBuilder.AppendLine("AddDownLoadResult:" + url);
            if (string.IsNullOrEmpty(url)) return;
            
            string fileSizeStr = null;
            if (fileSize > 1024)
            {
                double s = fileSize / (1024);
                fileSizeStr = s < 1024 ? s + "k" : s / 1024 + "m";
            }
            else
            {
                fileSizeStr = fileSize.ToString() + "b";
            }
            string insertString = success ? "(√)" + string.Format("size:{0}", fileSizeStr) : "(×)";

            if (success)
            {
                _SuccessCount++;
                if (_ReDownBegin) { _FailCount--; _FailCount = _FailCount > 0 ? _FailCount : 0; }
            }
            else
            {
                if (!_ReDownBegin)
                {
                    _FailCount++;
                }
                _FailList.Add(url);
            }
            int insertIndex = 0;
            if (_DownCount < _TotalCount)
            {
                _DownCount++;
                if (!_ReDownBegin)
                {
                    insertIndex = txtResultShow.Text.IndexOf(url);
                }
                else
                {
                    insertIndex = txtResultShow.Text.IndexOf(string.Format("第{0}次重试：",_RetyrCount) + url);
                }
                
            }

            if (insertIndex > -1)
            {
                txtResultShow.Text = txtResultShow.Text.Insert(insertIndex + url.Length + (!_ReDownBegin ? 0 : 6), insertString);
            }
            labDownResult.Text = string.Format(_DownLoadingTag, _SuccessCount.ToString(), _FailCount.ToString());


            if (_DownCount == _TotalCount)
            {
                if (rbGiveUp.Checked || (_RetyrCount >= _RetyrAllowCount) || (_FailList == null || _FailList.Count == 0))
                {
                    EndDownLoadImg();
                }
                else
                {
                    if (_FailList != null && _FailList.Count > 0)
                    {
                        _ReDownBegin = true;
                        _RetyrCount++;
                        if (_RetyrCount <= _RetyrAllowCount)
                        {
                            for (int i = _FailList.Count - 1; i >= 0; i--)
                            {
                                _DownCount--;
                                txtResultShow.Text += string.Format("第{1}次重试：{0}", _FailList[i], _RetyrCount);
                                HttpWebRequestDelegate(_FailList[i], _Timeout);
                                _FailList.RemoveAt(i);
                                txtResultShow.Text += string.Format("\r\n");
                            }
                            
                        }
                       
                    }
                }
            }
        }


        private void EndDownLoadImg()
        {
            if (this.InvokeRequired)
            {
                EndDownLoadImgCallBack cb = new EndDownLoadImgCallBack(EndDownLoadImg);
                this.Invoke(cb);
                return;
            }
            SetGetImgButtonIsBusy(false);
            if (!_IsStopped)
            {
                _IsStopped = true;
                _Stopwatch.Stop();
                _TS = _Stopwatch.Elapsed;
                
                MessageBox.Show(string.Format("耗时{0}秒,成功下载{3}张，失败{4}张{2}图片保存在 {1}", _TS.ToString(), _SavePath, "\r\n\r\n", _SuccessCount.ToString(), _FailCount.ToString()));
            }
            //MessageBox.Show(logStringBuilder.ToString());
        }
        
        private void SetGetImgButtonIsBusy(bool busy)
        {
            _StopWorking = !busy;
            if (busy)
            {
                btnBegin.Text = "操作中.....";   
            }
            else
            {
                btnBegin.Text = "开始";
            }
            btnBegin.Enabled = !busy;
            btnStop.Enabled = busy;
            btnSelectSavePath.Enabled = !busy;
            cbType.Enabled = !busy;
            txtFrom.Enabled = !busy;
            txtTo.Enabled = !busy;

            cbJpg.Enabled = cbGif.Enabled = cbBmp.Enabled = cbPng.Enabled = !busy;
            
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitLink();
        }

        private void InitLink()
        {
            string url = _UrlArr[cbType.SelectedIndex];
            url = url.Substring(0, url.LastIndexOf("/"));
            labUrl.Text = url;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            logStringBuilder.AppendLine("btnStop_Click");
            _StopWorking = true;
            EndDownLoadImg();
        }

        private void btnSelectSavePath_Click(object sender, EventArgs e)
        {
            SelectSavePath();
            
        }


        private void SelectSavePath()
        {
            FolderBrowserDialog path = new FolderBrowserDialog();
            path.ShowDialog();
            if (!string.IsNullOrEmpty(path.SelectedPath))
            {
                txtSavePath.Text = path.SelectedPath;
            }
            else
            {
                txtSavePath.Text = _DefaultSavePath;
            }
        }

        /// <summary>
        /// 判断是否属于允许下载的类型
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private bool IsAllowTypeImg(string img)
        {
            if (string.IsNullOrEmpty(img) || !img.Contains(".")) return false;
            int beginSubIndex = img.LastIndexOf(".") + 1;
            if(beginSubIndex>img.Length)return false;
            int subLength = img.Length - beginSubIndex;
            string imgType = img.Substring(beginSubIndex, subLength);
            if (cbJpg.Checked && (imgType.ToUpper() == "JPG" || imgType.ToUpper() == "JPEG")) return true;
            if (cbGif.Checked && imgType.ToUpper() == "GIF") return true;
            if (cbPng.Checked && imgType.ToUpper() == "PNG") return true;
            if (cbBmp.Checked && imgType.ToUpper() == "BMP") return true;
            return false;
        }

        private void btnOpenSavePath_Click(object sender, EventArgs e)
        {
            string path = txtSavePath.Text;
            if(!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                try
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        #region
        private void rbRetry_CheckedChanged(object sender, EventArgs e)
        {
            txtRetryCount.Enabled = rbRetry.Checked;
        }

        private void rbGiveUp_CheckedChanged(object sender, EventArgs e)
        {
            txtRetryCount.Enabled = rbRetry.Checked;
        }
        #endregion
    }
}
