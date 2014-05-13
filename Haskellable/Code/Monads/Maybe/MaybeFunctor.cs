using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class MaybeFunctor
    {
        public static IMaybe<TResult> Select<TValue, TResult>(
            this IMaybe<TValue> @this
            , Func<TValue, TResult> selector)
        {
            return 
                @this.ToCaseOf()
                .Match((Just<TValue> just) => new Just<TResult>(selector(just.Value)) as IMaybe<TResult>)
                .Return(_ => new Nothing<TResult>());
        }

        public static IMaybe<TValue> Where<TValue>(
            this IMaybe<TValue> @this
            , Func<TValue, bool> predicate)
        {
            return
                @this.ToGuards()
                .When(
                    m => m.ToCaseOf()
                        .Match((Just<TValue> just)=>predicate( just.Value))
                        .Return(false)
                    , m => m)
                .Return(_=>new Nothing<TValue>());
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

            return new Nothing<TResult>();
        }

        public static IMaybe<TValue> SelectMany<TValue>(
            this IMaybe<IMaybe<TValue>> @this)
        {
            var just = @this as Just<IMaybe<TValue>>;
            if (just == null)
            {
                return new Nothing<TValue>();
            }
            return just.Value;
        }

        public static IMaybe<TResult> SelectMany<TSource, TSelected, TResult>(
            this IMaybe<TSource> @this,
            Func<TSource, IMaybe<TSelected>> maybeSelector,
            Func<TSource, TSelected, TResult> resultSelector)
        {
            var just = @this as Just<TSource>;

            if (just == null)
            {
                return new Nothing<TResult>();
            }

            var selectedJust = maybeSelector(just.Value) as Just<TSelected>;
            if (selectedJust == null)
            {
                return new Nothing<TResult>();
            }

            return resultSelector(just.Value, selectedJust.Value).ToMaybe();
        }


    }
}
