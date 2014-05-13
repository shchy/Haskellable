using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.HeadTailList
{
    public class HeadTailList<T> : IHeadTailList<T>
    {
        private Lazy<T> lazyHead;
        private Lazy<IEnumerable<T>> lazyTail;
        private Lazy<IEnumerable<T>> lazyList;
        public HeadTailList(IEnumerable<T> list)
        {
            System.Diagnostics.Contracts.Contract.Requires(list.Count() > 1);
            
            this.lazyHead = new Lazy<T>(() => list.First());
            this.lazyTail = new Lazy<IEnumerable<T>>(() => list.Skip(1));
            this.lazyList = new Lazy<IEnumerable<T>>(() => list);
        }

        public HeadTailList(T head, IEnumerable<T> tail)
        {
            System.Diagnostics.Contracts.Contract.Requires(tail.Count() > 0);

            this.lazyHead = new Lazy<T>(() => head);
            this.lazyTail = new Lazy<IEnumerable<T>>(() => tail);
            this.lazyList = new Lazy<IEnumerable<T>>(() => new[] { head }.Concat(tail));
        }

        public T Head { get { return this.lazyHead.Value; } }
        public IHeadTailList<T> Tail { get { return this.lazyTail.Value.ToHeadTailList(); } }


        public IEnumerator<T> GetEnumerator()
        {
            return
                this.lazyList.Value.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return
                this.lazyList.Value.GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("List head:{0}", this.Head);
        }
    }
}
