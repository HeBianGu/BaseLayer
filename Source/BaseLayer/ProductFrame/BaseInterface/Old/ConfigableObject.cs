using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace OPT.Product.BaseInterface
{
    public interface IBLUIConfigable
    {
        IBLUIConfig UIConfig { get; }
    }


    public class BLKeyValue<TKey, TValue>
    {
        TKey _key;
        TValue _value;

        public TKey Key { set { _key = value; } get { return _key; } }
        public TValue Value { set { _value = value; } get { return _value; } }

        public BLKeyValue() { _key = default(TKey); _value = default(TValue); }
        public BLKeyValue(TKey key, TValue val) { _key = key; _value = val; }
    }

    public class BLConfigItem : BLKeyValue<string, Object>
    {
        public BLConfigItem() { }
        public BLConfigItem(string key, Object val) : base(key, val) { }
    }

    public interface IBLConfig
    {
        IEnumerable<BLConfigItem> KeyValues { get; }
        IEnumerable<string> Keys { get; }
        IEnumerable Values { get; }
        Object this[string key] { get; set; }
        void Add(string key, Object value);
        bool ContainsKey(string key);
        bool Remove(string key);
        bool TryGetValue(string key, out Object value);
    }

    public interface IBSConfigable
    {
        IBLConfig Config { get; }
    }

    public interface IBSStaticConfigInit
    {
        void InitStaticConfig(IBLConfig staticConfig);
    }

}
