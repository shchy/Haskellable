using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.HeadTailList
{
    public class SingleList<T> : IHeadTailList<T>
    {
        private IEnumerable<T> list;
        public SingleList(T head)
        {
            this.Head = head;
            this.list = new[] { this.Head };
        }

        public T Head { get; set; }

        public IHeadTailList<T> Tail
        {
            get { return Enumerable.Empty<T>().ToHeadTailList(); }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("SingleList {0}", this.Head);
        }
    }
}
