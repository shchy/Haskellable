using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EitherExtension
    {

        public static IEither<TLeft, TRight> ToLeft<TLeft, TRight>(this TLeft @this)
        {
            return
                new Left<TLeft, TRight>(@this);
        }


        public static IEither<TLeft, TRight> ToRight<TLeft, TRight>(this TRight @this)
        {
            return
                new Right<TLeft, TRight>(@this);
        }

        public static IEither<TLeft, TRight> OnLeft<TLeft, TRight>(
            this IEither<TLeft, TRight> @this
            , Action<TLeft> action)
        {
            var left = @this as Left<TLeft, TRight>;
            if (left != null)
            {
                action(left.Value);
            }
            return @this;
        }

        public static IEither<TLeft, TRight> OnRight<TLeft, TRight>(
            this IEither<TLeft, TRight> @this
            , Action<TRight> action)
        {
            var right = @this as Right<TLeft, TRight>;
            if (right != null)
            {
                action(right.Value);
            }
            return @this;
        }
    }
}
