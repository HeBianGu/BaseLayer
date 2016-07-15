﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HebianGu.ComLibModule.Cache
{
    public interface ICacheProvider:IDisposable 
    {

        void Remove(string key);
        object Get(string key);
        T Get<T>(string key);
        T Get<T>(string key, T defaultValue);
        bool HasKey(string key);
        void Store(string key, object data);
        //void Store(string key, object data, DateTime expiryTime);
        void Flush();
        object this[string key] { get; }
    }

}
