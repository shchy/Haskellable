using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class MaybeLinqExtension
    {
        public static IMaybe<TResult> Select<TValue, TResult>(
            this IMaybe<TValue> @this
            , Func<TValue, TResult> selector)
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                return Maybe.New(selector(just.Value));
            }
            return Maybe.Nothing<TResult>();
        }

        public static IMaybe<TValue> Where<TValue>(
            this IMaybe<TValue> @this
            , Func<TValue, bool> predicate)
        {
            var just = @this as Just<TValue>;
            if (just != null && predicate(just.Value))
            {
                return just;
            }
            else
            {
                return Maybe.Nothing<TValue>();
            }
        }

        public static IMaybe<TResult> SelectMany<TValue, TResult>(
            this IMaybe<TValue> @this,
            Func<TValue, IMaybe<TResult>> selector)
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                return selector(just.Value);
            }

            return Maybe.Nothing<TResult>();
        }

        public static IMaybe<TValue> SelectMany<TValue>(
            this IMaybe<IMaybe<TValue>> @this)
        {
            var just = @this as Just<IMaybe<TValue>>;
            if (just == null)
            {
                return Maybe.Nothing<TValue>();
            }

            return just.Value.Select(x=>x);;
        }

        public static IMaybe<TResult> SelectMany<TSource, TSelected, TResult>(
            this IMaybe<TSource> @this,
            Func<TSource, IMaybe<TSelected>> maybeSelector,
            Func<TSource, TSelected, TResult> resultSelector)
        {
            var just = @this as Just<TSource>;

            if (just == null)
            {
                return Maybe.Nothing<TResult>();
            }

            var selectedJust = maybeSelector(just.Value) as Just<TSelected>;
            if (selectedJust == null)
            {
                return Maybe.Nothing<TResult>();
            }

            return resultSelector(just.Value, selectedJust.Value).ToMaybe();
        }


    }
}
