using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace GameFrameX.Config
{
    [Preserve]
    public abstract class BaseDataTable<T> : IDataTable<T>
    {
        protected readonly SortedDictionary<long, T> LongDataMaps = new SortedDictionary<long, T>();
        protected readonly SortedDictionary<string, T> StringDataMaps = new SortedDictionary<string, T>();

        protected readonly List<T> DataList = new List<T>();
        public abstract Task LoadAsync();

        public T Get(int id)
        {
            LongDataMaps.TryGetValue(id, out T value);
            return value;
        }

        public T Get(string id)
        {
            StringDataMaps.TryGetValue(id, out T value);
            return value;
        }

        public T this[int id] => Get(id);

        public T this[string id] => Get(id);

        public int Count => Math.Max(LongDataMaps.Count, StringDataMaps.Count);

        public T FirstOrDefault => DataList.FirstOrDefault();

        public T LastOrDefault => DataList.LastOrDefault();

        public T[] All => DataList.ToArray();

        public T[] ToArray()
        {
            return DataList.ToArray();
        }

        public T Find(Func<T, bool> func)
        {
            return DataList.FirstOrDefault(func);
        }

        public T[] FindList(Func<T, bool> func)
        {
            return DataList.Where(func).ToArray();
        }
    }
}