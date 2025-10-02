using System.Collections.Generic;
using System.Linq;

namespace Amib.Threading.Internal
{
    internal class SynchronizedDictionary<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;
        private readonly object _lock;

        public SynchronizedDictionary()
        {
            _lock = new object();
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public int Count
        {
            get { return _dictionary.Count; }
        }

        public bool Contains(TKey key)
        {
            lock (_lock)
            {
                return _dictionary.ContainsKey(key);
            }
        }

        public void Remove(TKey key)
        {
            lock (_lock)
            {
                _dictionary.Remove(key);
            }
        }

        public object SyncRoot
        {
            get { return _lock; }
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (_lock)
                {
                    if (_dictionary.TryGetValue(key, out TValue value))
                    {
                        return value;
                    }
                    return default;
                }
            }
            set
            {
                lock (_lock)
                {
                    _dictionary[key] = value;
                }
            }
        }

        public List<TKey> Keys
        {
            get
            {
                lock (_lock)
                {
                    return _dictionary.Keys.ToList();
                }
            }
        }

        public List<TValue> Values
        {
            get
            {
                lock (_lock)
                {
                    return _dictionary.Values.ToList();
                }
            }
        }
        public void Clear()
        {
            lock (_lock)
            {
                _dictionary.Clear();
            }
        }
    }
}
