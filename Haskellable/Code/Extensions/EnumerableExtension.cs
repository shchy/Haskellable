using Haskellable.Code.Monads.HeadTailList;
using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
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
