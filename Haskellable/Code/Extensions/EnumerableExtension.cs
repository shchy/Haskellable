using Haskellable.Code.Monads.HeadTailList;
using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class EnumerableExtension
    {
        public static IEnumerable<TResult> Scan<TAccumulate, TValue, TResult>(
            this IEnumerable<TValue> @this
            , TAccumulate acc
            , Func<TAccumulate, TValue, TAccumulate> func
            , Func<TAccumulate, TResult> resultSelector
            )
        {
            var context = acc;
            return
                @this.ToHeadTailList()
                .Recursion(
                    Enumerable.Empty<TAccumulate>()
                    , x =>
                    {
                        context = func(context, x);
                        return new[] { context };
                    }
                    , (head, tailResult) => head.Concat(tailResult))
                .Select(resultSelector);
        }

        
        public static TResult Foldl<T, TResult>(this IEnumerable<T> @this, TResult acc, Func<TResult, T, TResult> func)
        {
            return @this.Aggregate( acc, func);
        }

        public static TResult Foldr<T, TResult>(this IEnumerable<T> @this, Func<T, TResult, TResult> func, TResult acc)
        {
            return @this.Reverse().Foldl(acc, (t, a) => func(a, t));
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> act)
        {
            foreach (var item in @this)
            {
                act(item);
            }
        }

        public static void ForEach<T>(this IEnumerable<IMaybe<T>> @this, Action<T> act)
        {
            foreach (var item in @this)
            {
                item.On(act);
            }
        }

        public static void ForEach<T1,T2>(this IEnumerable<Tuple<T1,T2>> @this, Action<T1,T2> act)
        {
            foreach (var item in @this)
            {   
                act(item.Item1, item.Item2);
            }
        }


        public static IMaybe<T> FirstOrMaybe<T>(
            this IEnumerable<T> @this)
        {
            return
                @this.FirstOrDefault().ToMaybe();
        }

        public static IMaybe<T> FirstOrMaybe<T>
            (this IEnumerable<T> @this
            , Func<T,bool> predicate)
        {
            return
                @this.FirstOrDefault(predicate).ToMaybe();
        }
    }
}
