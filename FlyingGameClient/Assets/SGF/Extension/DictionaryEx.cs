using System;
using System.Collections.Generic;

namespace SGF
{
    public class DictionaryEx<K, V> : Dictionary<K, V>
    {
        public new V this[K indexKey]
        {
            set
            {
                base[indexKey] = value;
            }
            get
            {
                try
                {
                    return base[indexKey];
                }
                catch (Exception)
                {
                    return default(V);
                }
            }
        }
        
    }
}

