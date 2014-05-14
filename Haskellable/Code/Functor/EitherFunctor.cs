using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class EitherFunctor
    {
        public static IEither<TLeft, TNewRight> Select<TLeft,TRight,TNewRight>(
            this IEither<TLeft, TRight> @this
            , Func<TRight, TNewRight> selector)
        {
            return @this.FMap(selector) as IEither<TLeft, TNewRight>;
        }
    }
}
