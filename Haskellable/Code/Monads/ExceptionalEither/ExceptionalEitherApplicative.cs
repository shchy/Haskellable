using Haskellable.Code.Monads.Either;
using Haskellable.Code.Monads.ExceptionalEither;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class ExceptionalEitherApplicative
    {
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

        public static IExceptionalEither<TReturn> ToExceptional<TReturn>(this Func<TReturn> @this, string message)
        {
            var ei = @this.ToExceptional();
            return
                ei.IsLeft
                ? new ExceptionalEitherLeft<TReturn>(new Exception(message, (ei as ILeft<Exception>).Value)) as IExceptionalEither<TReturn>
                : new ExceptionalEitherRight<TReturn>(ei.Return(() => default(TReturn))) as IExceptionalEither<TReturn>;
        }

    }
}
