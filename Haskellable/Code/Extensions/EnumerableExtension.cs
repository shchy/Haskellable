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
    }
}
