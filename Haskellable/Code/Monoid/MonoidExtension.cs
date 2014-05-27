using Haskellable.Code.Monoid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class MonoidExtension
    {
        public static IMonoid<T,TValue> Concat<T, TValue>(this IMonoid<T,TValue> @this, IEnumerable<IMonoid<T,TValue>> list)
        {
            return list.Foldr((x, acc) => x.Append(acc), @this);
        }
    }
}
