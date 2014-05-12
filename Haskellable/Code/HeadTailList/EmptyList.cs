using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.HeadTailList
{
    public class EmptyList<T> : IHeadTailList<T>
    {
        public T Head
        {
            get { throw new NotImplementedException(); }
        }

        public IHeadTailList<T> Tail
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("EmptyList {0}", typeof(T));
        }
    }
}
