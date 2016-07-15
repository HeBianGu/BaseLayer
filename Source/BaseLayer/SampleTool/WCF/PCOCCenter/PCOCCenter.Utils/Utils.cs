using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
using System.Security.Permissions;
using System.Xml.Serialization;

using OPT.Product.Base;
using OPT.Product.BaseInterface;
using System.Reflection;

namespace OPT.PCOCCenter.Utils
{
	/// <summary>
	/// 一些常用的静态函数。
	/// </summary>
	public static class Utils
	{
        private static Translator translator;

		/// <summary>
		/// PEOfficeCenter资源目录, 此目录下的文件是只读的
		/// </summary>
		public static string PEOfficeCenterResourceFolder
		{
			get
			{
                if (BxSystemInfo.Instance.SystemPath == null)
                {
                    string exeFileFullPath = Assembly.GetEntryAssembly().Location;
                    string binPath = Path.GetDirectoryName(exeFileFullPath);
                    BxSystemInfo.Instance.Init(binPath);
                }

                return BxSystemInfo.Instance.SystemPath.CultureConfigPath + "\\GeneralLayer\\PEOfficeCenter";
			}
		}
        
		/// <summary>
		/// 翻译
		/// </summary>
		public static string Translate(string s)
		{
			if (translator == null)
			{
				translator = new Translator();
				try
				{
                    translator.LoadDictionary(PEOfficeCenterResourceFolder + "\\Languages\\PEOfficeCenter.lng", true);
				}
				catch (Exception exp)
				{
					Trace.WriteLine("[PEOffciecCenter] (EXP) " + exp.Message);
				}
			}
			return translator.Translate(s);
		}

    }
}
