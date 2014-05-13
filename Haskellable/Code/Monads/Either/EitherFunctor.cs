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
            return
               @this.ToCaseOf()
               .Match((Right<TRight> right) => selector(right.Value).ToRight().ToEither<TLeft>())
               .Return(left => (left as Left<TLeft>).ToEither<TNewRight>());
        }

        //public static IEither<TLeft,TRight> Where<TLeft,TRight>(
        //    this IEither<TLeft, TRight> @this
        //    , Func<TRight,bool> predicate)
        //{
        //    var predicated =
        //        @this.ToCaseOf()
        //        .Match((Right<TRight> right) => predicate(right.Value))
        //        .Return(false);
        //    return
        //        @this.ToGuards()
        //        .When(_=>predicate, @this)
        //        .Return(_=>)

        //}
    }
}
