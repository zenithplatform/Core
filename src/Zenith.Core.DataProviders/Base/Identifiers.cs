using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.DataProviders.Base
{
    public abstract class IdentifierBase
    {
        public abstract string Name { get; set; }
        public abstract object Value { get; set; }
    }

    public class IndentifierCollection<T> : ICollection<T> where T : IdentifierBase
    {
        ICollection<T> _innerCollection = null;

        public IndentifierCollection()
        {
            _innerCollection = new Collection<T>();
        }

        public int Count
        {
            get
            {
                return _innerCollection.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _innerCollection.IsReadOnly;
            }
        }

        public void Add(T item)
        {
            _innerCollection.Add(item);
        }

        public void Clear()
        {
            _innerCollection.Clear();
        }

        public bool Contains(T item)
        {
            return _innerCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _innerCollection.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return _innerCollection.Remove(item);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _innerCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IEnumerator enumerator = ((IEnumerable)_innerCollection).GetEnumerator();

            return enumerator;
        }
    }
}
