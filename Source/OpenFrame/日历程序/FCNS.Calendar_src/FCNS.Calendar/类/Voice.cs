using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FCNS.Calendar
{
    /// <summary>
    /// mp3 播放类
    /// </summary>
    class Voice
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("winmm.dll", EntryPoint = "mciGetErrorString", CharSet = CharSet.Auto)]
        public static extern int mciGetErrorString(int errCode, string lpstrReturnString, int uReturnLength);

        [DllImport("winmm.dll", EntryPoint = "waveOutSetVolume", CharSet = CharSet.Auto)]
        public static extern int waveOutSetVolume(uint deviceID, uint Volume);      //参数为uint  

        public enum PlayStatus              // 播放状态
        {
            Playing, Paused, Stopped, NoPlayFile
        }

        private static int bm = 0;
        private string Name = "";           // 存放播放的文件名
        private string buff = "................................................................................................................................";
        private int errCode;                // 执行mciSendString返回的错误码
        private string device;

        public Voice()
        {
            ++bm;
            device = "music" + bm;
        }

        public string Message               // 根据错误码返回错误信息字符串
        {
            get
            {
                mciGetErrorString(errCode, buff, buff.Length);
                int p = buff.IndexOf('\0');
                return buff.Substring(0, p);
            }
        }

        public bool Open(string fileName)   // 打开一个要播放的文件
        {
            bool b = false;
            try
            {
                if (Status != PlayStatus.NoPlayFile) Close();
                errCode = mciSendString("open \"" + fileName + "\" alias " + device, buff, buff.Length, 0);
                b = errCode == 0;
            }
            finally
            {
                if (b) Name = fileName;
                else Name = string.Empty;
            }
            return b;
        }

        public void Play()                  // 播放 - playing
        {
            errCode = mciSendString("play " + device, buff, buff.Length, 0);  // play NetMp3 repeat 循环播放
        }

        public void Stop()                  // 停止 - stopped
        {
            Position = 0;                   // mciSendString("stop NetMp3", buff, buff.Length, 0);
        }

        public void Pause()                 // 状态将变为 - paused
        {
            errCode = mciSendString("pause " + device, buff, buff.Length, 0);
        }

        public void Continue()              // 状态将变为 - playing
        {
            errCode = mciSendString("resume " + device, buff, buff.Length, 0);
        }

        public void Close()                 // 状态将变为 - 空
        {
            errCode = mciSendString("close " + device, buff, buff.Length, 0);
            //errCode = mciSendString("close all", buff, buff.Length, 0);
        }

        public string FileName              // 播放的文件
        {
            get
            {
                return Name;
            }
            set
            {
                Open(value);
            }
        }

        public PlayStatus Status            // 获取当前状态: playing / paused / stopped / 空
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return PlayStatus.NoPlayFile;
                PlayStatus status;
                errCode = mciSendString("status " + device + " mode", buff, buff.Length, 0);
                int p = buff.IndexOf('\0');
                string s = buff.Substring(0, p);
                switch (s)
                {
                    case "playing": status = PlayStatus.Playing; break;
                    case "paused": status = PlayStatus.Paused; break;
                    default: status = PlayStatus.Stopped; break;
                }
                return status;
            }
        }

        public int Length                   // 歌曲总长度（毫秒）
        {
            get
            {
                errCode = mciSendString("status " + device + " length", buff, buff.Length, 0);
                int p = buff.IndexOf('\0');
                string s = buff.Substring(0, p);
                if (string.IsNullOrEmpty(s)) return 0;
                return int.Parse(s);
            }
        }

        public int Position                 // 播放位置（毫秒）
        {
            get
            {
                errCode = mciSendString("status " + device + " position", buff, buff.Length, 0);
                int p = buff.IndexOf('\0');
                string s = buff.Substring(0, p);
                if (string.IsNullOrEmpty(s)) return 0;
                return int.Parse(s);
            }
            set
            {
                errCode = mciSendString("seek " + device + " to " + value, buff, buff.Length, 0);
            }
        }

        public int Volume                   // 音量 0 ～ 1000
        {
            get
            {
                errCode = mciSendString("status " + device + " volume", buff, buff.Length, 0);
                int p = buff.IndexOf('\0');
                string s = buff.Substring(0, p);
                if (string.IsNullOrEmpty(s)) return 0;
                return int.Parse(s);
            }
            set
            {
                errCode = mciSendString("setaudio " + device + " volume to " + value, buff, buff.Length, 0);
            }
        }

        public uint LeftRightVolume         // 分别控制左右声道的音量
        {
            set
            {
                waveOutSetVolume(0, value);  // 0xffffffff，高2字节控制右声道，低2字节控制左声道。
            }
        }
    }
}