#nullable enable
using System;
using System.Collections.Generic;

namespace HealthcareSystem.Data
{
    // a) Generic Repository<T>
    public class Repository<T> where T : class
    {
        private readonly List<T> items = new();

        public void Add(T item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            items.Add(item);
        }

        public List<T> GetAll() => new(items);

        public T? GetById(Func<T, bool> predicate)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            foreach (var it in items)
                if (predicate(it)) return it;
            return null;
        }

        public bool Remove(Func<T, bool> predicate)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            for (int i = 0; i < items.Count; i++)
            {
                if (predicate(items[i]))
                {
                    items.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
