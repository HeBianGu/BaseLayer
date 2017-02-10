
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HebianGu.Product.WinHelper
{
    /// <summary>
    /// WeatherControl.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherControl : UserControl
    {
        public WeatherControl()
        {
            InitializeComponent();

        }

        public Stream GetStream(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Method = "get";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream result = response.GetResponseStream();
            return result;
        }

        public string GetRequest(string url)
        {

            StreamReader stream = new StreamReader(GetStream(url));
            string res = stream.ReadToEnd();
            return res;
        }


        static BusyDecorator busy = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Start fetching the weather forecast asynchronously.
            NoArgDelegate fetcher = new NoArgDelegate(
                this.FetchWeatherFromServer);

            fetcher.BeginInvoke(null, null);
            if (busy == null)
            {
                busy = new BusyDecorator(canvas);
            }
            busy.StartDecorator();
        }



        private void FetchWeatherFromServer()
        {

            try
            {
                string url = "http://api.map.baidu.com/location/ip?ak=164137132a3351c187c06a7d851d5ad4&coor=bd09ll";
                string ret = GetRequest(url);
                ret = Regex.Unescape(ret);
                dynamic res = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ret);

                url = "http://api.map.baidu.com/telematics/v3/weather?location=" + res.content.point.x + "," + res.content.point.y + "&output=json&ak=164137132a3351c187c06a7d851d5ad4";
                ret = GetRequest(url);
                ret = Regex.Unescape(ret);
                res = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ret);

                grid.Dispatcher.BeginInvoke(
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new TwoArgDelegate(UpdateUserInterface),
                    res, ret);
                //</SnippetThreadingWeatherDispatcherOneArge>
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateUserInterface(dynamic res, string pm)
        {
            if (res.status.ToString() == "success")
            {
                busy.StopDecorator();
                int num = 0;
                string strWeather = res.results[0].weather_data[0].weather.ToString();

                if (strWeather[0] == '晴')
                {
                    if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour < 8)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 1;
                    }
                }
                else if (strWeather[0] == '阴')
                {
                    num = 3;
                }
                else if (strWeather == "多云转晴" || strWeather == "多云" || strWeather == "多云转小雨")
                {
                    num = 5;
                }
                else if (strWeather[0] == '霾')
                {
                    num = 7;
                }
                else if (strWeather == "阵雨" || strWeather == "阵雨转雷阵雨" || strWeather == "小雨" || strWeather == "小雨转多云" || strWeather == "阵雨转多云")
                {
                    num = 8;
                }
                else if (strWeather == "中雨转阵雨" || strWeather == "中雨转阴")
                {
                    num = 9;
                }
                else if (strWeather == "中到大雨")
                {
                    num = 10;
                }
                else if (strWeather == "大到暴雨" || strWeather == "大到暴雨转中雨")
                {
                    num = 11;
                }
                else if (strWeather[0] == '雷')
                {
                    num = 30;
                }
                txt_address.Text = res.results[0].currentCity;
                icon.Source = new BitmapImage(new Uri("../image/weather/" + num + ".png", UriKind.Relative));
                string temp = res.results[0].weather_data[0].date.ToString();
                temp = temp.Substring(temp.LastIndexOf("：") + 1, temp.Length - temp.LastIndexOf("：") - 2);
                txtDate.Text = temp.Substring(0);
                txtWindAndTemperature.Text = res.results[0].weather_data[0].wind.ToString() + " " + res.results[0].weather_data[0].temperature.ToString();

                int pm25 = res.results[0].pm25;
                string pm25Level = "";
                if (pm25 <= 50)
                {
                    pm25Level = "优";
                    txtPM.Background = Brushes.Lime;
                }
                else if (pm25 <= 100)
                {
                    pm25Level = "良";
                    txtPM.Background = Brushes.LimeGreen;
                }
                else if (pm25 <= 150)
                {
                    pm25Level = "轻度污染";
                    txtPM.Background = Brushes.Orange;
                }
                else if (pm25 <= 200)
                {
                    pm25Level = "中度污染";
                    txtPM.Background = Brushes.Red;
                }
                else if (pm25 <= 300)
                {
                    pm25Level = "重度污染";
                    txtPM.Background = Brushes.Purple;
                }
                else
                {
                    pm25Level = "严重污染";
                    txtPM.Background = Brushes.Maroon;
                }
                txtPM.Text = pm25 + pm25Level;
                //pm = pm.Substring(pm.IndexOf("op_pm25_graexp"));
            }
        }

        // Delegates to be used in placking jobs onto the Dispatcher.
        private delegate void NoArgDelegate();
        //<SnippetThreadingWeatherDelegates>
        private delegate void OneArgDelegate(dynamic arg);

        private delegate void TwoArgDelegate(dynamic arg, string pm);
        //</SnippetThreadingWeatherDelegates>
    }
}
