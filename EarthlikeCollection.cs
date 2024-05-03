using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSystem
{
    public class EarthlikeCollection<T> : IEnumerable<T> where T : IEarthLike
    {
        private List<T> _earthlike = new List<T>();

        public EarthlikeCollection() { }

        public IEnumerator<T> GetEnumerator()
        {
            return _earthlike.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _earthlike.Add(item);
        }

        public void Clear()
        {
            _earthlike.Clear();
        }

        public IEnumerable<T> SortCollection(string sortPar)
        {
            return _earthlike.OrderBy(item=>item.GetEarthLikeCoefficient());
        }
    }
}
