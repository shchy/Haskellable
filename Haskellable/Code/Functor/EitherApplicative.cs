using Haskellable.Code.CaseOf;
using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class EitherApplicative
    {
        public static ILeft<T> ToLeft<T>(this T @this)
        {
            return
                new Left<T>(@this);
        }

        public static IRight<T> ToRight<T>(this T @this)
        {
            return
                new Right<T>(@this);
        }

        public static IEither<TLeft, TRight> ToLeft<TLeft, TRight>(this TLeft @this)
        {
            return
                new EitherLeft<TLeft,TRight>(@this);
        }

        public static IEither<TLeft, TRight> ToRight<TLeft, TRight>(this TRight @this)
        {
            return
                new EitherRight<TLeft, TRight>(@this);
        }

        public static IEither<TLeft, TRight> OnLeft<TLeft, TRight>(
            this IEither<TLeft, TRight> @this
            , Action<TLeft> action)
        {
            var left = @this as EitherLeft<TLeft, TRight>;
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
            var right = @this as EitherRight<TLeft, TRight>;
            if (right != null)
            {
                action(right.Value);
            }
            return @this;
        }

        public static IExceptionalEither<TValue> ToExceptional<TValue>(this Func<TValue> @this)
        {
            try
            {
                return new ExceptionalEitherRight<TValue>(@this());
            }
            catch (Exception ex)
            {
                return new ExceptionalEitherLeft<TValue>(ex);
            }
        }

        public static CaseOf<object, TReturn> ToCaseOf<TLeft, TRight, TNewValue, TReturn>(
            this IEither<TLeft, TRight> @this, Func<TNewValue, TReturn> func)
        {
            return 
                @this
                .ToCaseOf()
                .Match((IRight<TRight> right) => right.Value as object)
                .Match((ILeft<TLeft> left) => left.Value as object)
                .Return(_ => new object())
                .ToCaseOf()
                .Match(func);
        }
    }
}
