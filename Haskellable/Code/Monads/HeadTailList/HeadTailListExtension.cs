using Haskellable.Code.Monads.HeadTailList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class HeadTailListExtension
    {
        public static IHeadTailList<T> ToHeadTailList<T>(this IEnumerable<T> @this)
        {
            var count = @this.Count();
            switch (count)
            {
                case 0:
                    return new EmptyList<T>();
                case 1:
                    return new SingleList<T>(@this.First());
                default:
                    return new HeadTailList<T>(@this);
            }
        }

        public static TReturn Recursion<TValue, TReturn>(
            this IHeadTailList<TValue> @this
            , TReturn emptySelector
            , Func<TValue, TReturn> singleSelector
            , Func<TReturn, TReturn, TReturn> resultSelector)
        {
            return
                @this
                    .ToCaseOf()
                    .Match((EmptyList<TValue> a) => emptySelector)
                    .Match((SingleList<TValue> a) => singleSelector(a.Head))
                    .Match((HeadTailList<TValue> a) =>
                        resultSelector(
                            singleSelector(a.Head)
                            , a.Tail.Recursion(emptySelector, singleSelector, resultSelector)))
                    .Return(default(TReturn));
        }
    }
}
