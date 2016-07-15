using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Define
{
    /// <summary> 本机缓存 </summary>
    public class LocalCache : IDisposable
    {
        const string SIGN = "::->";

        private static List<string> _names = new List<string>();

        private List<string> _fileLines = new List<string>();

        public LocalCache(string name)
        {
            if (_names.Contains(name))
                throw new Exception("LocalCache name repeat.");

            _names.Add(Name = name);

            FileName =
            Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles)
            + "\\" + Process.GetCurrentProcess().ProcessName;

            if (!System.IO.Directory.Exists(FileName))
            {
                System.IO.Directory.CreateDirectory(FileName);
            }

            FileName += "\\" + Name + ".localcache";

            if (System.IO.File.Exists(FileName))
            {
                var ls =
                System.IO.File.ReadAllLines(FileName, Encoding.UTF8);

                if (ls != null && ls.Length > 0)
                {
                    foreach (var line in ls)
                    {
                        if (!string.IsNullOrEmpty(line))
                            _fileLines.Add(line);
                    }
                }
            }
        }

        ~LocalCache()
        {
            Dispose();
        }

        public void Dispose()
        {
            _names.Remove(Name);
        }

        public string Name
        {
            private set;
            get;
        }

        public string FileName
        {
            private set;
            get;
        }

        public void Set(string key, string value)
        {
            if (_fileLines.Count > 0)
            {
                for (int i = 0; i < _fileLines.Count; i++)
                {
                    if (_fileLines[i].StartsWith(key))
                    {
                        _fileLines[i] = key + SIGN + value;
                        WriteFile(_fileLines.ToArray());
                        return;
                    }
                }
            }

            _fileLines.Add(key + SIGN + value);
            WriteFile(key + SIGN + value);
        }

        public string Get(string key)
        {
            if (_fileLines.Count > 0)
            {
                for (int i = 0; i < _fileLines.Count; i++)
                {
                    if (_fileLines[i].StartsWith(key))
                    {
                        return _fileLines[i].Substring(key.Length + 3);
                    }
                }
            }

            return null;
        }

        public void WriteFile(object lines)
        {
            lock (this)
            {
                if (lines == null)
                    return;

                if (lines is string[])
                {
                    var ls = lines as string[];
                    if (ls == null || ls.Length < 1)
                        return;

                    System.IO.File.WriteAllLines(FileName, ls, Encoding.UTF8);
                }
                else if (lines is string)
                {
                    var st = lines as string;
                    if (string.IsNullOrEmpty(st))
                        return;
                    System.IO.File.AppendAllText(FileName, st, Encoding.UTF8);
                }
            }
        }
    }

}
