using System;
using System.Collections.Generic;
using System.Text;

namespace CSWavefront.Util
{
    /// <summary>
    /// Dictionary that automatically generates a value if you access a non existing key with the indexer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    public class AutoDictionary<T,U> :Dictionary<T,U>
    {
        Func<T, U> generator;

        public AutoDictionary(Func<T,U> generator)
        {
            this.generator = generator;
        }

        public new U this[T key]
        {
            get
            {
                if(!TryGetValue(key,out U val))
                {
                    val = generator(key);
                    base[key] = val;
                }
                return val;
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
