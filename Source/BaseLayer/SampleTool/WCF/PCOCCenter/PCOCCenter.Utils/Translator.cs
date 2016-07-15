using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace OPT.PCOCCenter.Utils
{
	internal class Translator
	{
		Dictionary<string, string> dictionary;

		public Translator()
		{
			dictionary = new Dictionary<string, string>();
		}

		public void LoadDictionary(string _path, bool _clear_previous)
		{
            int DebugLevel = 1;

			if (DebugLevel > 1)
			{
				if (_clear_previous)
				{
					Trace.WriteLine("[PEOffciecCenter] (II) Translator: Clear dictionary.");
				}
				Trace.WriteLine("[PEOffciecCenter] (II) Translator: Load dictionary: \"" + _path + "\"");
			}

			if (_clear_previous)
			{
				dictionary.Clear();
			}
			using (StreamReader reader = new StreamReader(_path, Encoding.UTF8, false))
			{
				string s;
				int line = 0;
				while(null != (s = reader.ReadLine()))
				{
					s = s.Trim();
					line++;
					if (s.Length == 0)
					{
						continue;
					}
					int n = s.IndexOf('=');
                    int m = s.IndexOf("//"); //分类注释
					if (n == -1 )
					{
                        if (m == -1)
                        {
                            if (DebugLevel > 0)
                            {
                                Trace.WriteLine("[PEOffciecCenter] (WW) Translator: missing '=' at line " + line.ToString() + ": ");
                                Trace.WriteLine("[PEOffciecCenter]    \"" + s + "\"");
                            }
                        }
						continue;
					}
					string a = s.Substring(0, n).Trim();
					if (a.Length == 0)
					{
						Trace.WriteLine("[PEOffciecCenter] (WW) Translator: 空词条 '=' at line " + line.ToString() + ": ");
					}
					string b;
					if (dictionary.TryGetValue(a, out b))
					{
						if (DebugLevel > 0)
						{
							Trace.WriteLine("[PEOffciecCenter] (WW) Translator: 重复条 '=' at line " + line.ToString() + ": \"" + a + "\"");
						}
						continue;
					}
					b = s.Substring(n + 1).Trim();
                    string prefix = b.Substring(0, 1);
                    string sufix = b.Substring(b.Length - 1, 1);
                    if (prefix == "\"")
                    {
                        //b.TrimStart('\"');
                        //b.Remove(0);
                        if (b.StartsWith("\""))
                        {
                            b = b.Substring(1, b.Length - 1);//删除第一个字母
                        }
                    }   
   
                    if (sufix == "\"")
                    {
                        b = b.TrimEnd('\"');//删除最后一个字母
                    }
                    //b = b.Trim();
                    if (!string.IsNullOrEmpty(b))
                    {
                        dictionary.Add(a, b);
                    }
				}
			}
		}

		public string Translate(string s)
		{
            int DebugLevel = 1;

			string ret;
			if (dictionary.TryGetValue(s, out ret))
			{
				return ret;
			}
			else
			{
				dictionary.Add(s, s);
				if (DebugLevel > 0)
				{
					Trace.WriteLine("[PEOffciecCenter] (WW) Translator: Missing: \"" + s + "\"");
				}
				return s;
			}
		}
		
	}
}
