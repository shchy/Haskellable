using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.HeadTailList
{
    public interface IHeadTailList<T> : IEnumerable<T>
    {
        T Head { get; }
        IHeadTailList<T> Tail { get; }
    }

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
    }

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
    }

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
    }
}
