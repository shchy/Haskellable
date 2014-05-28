using Haskellable.Code.Functor;
using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class MaybeApplicative
    {
        public static IMaybe<TValue> ToMaybe<TValue>(
            this IMaybe<TValue> @this)
        {
            return @this;
        }

        public static IMaybe<TValue> ToMaybe<TValue>(
            this TValue @this)
        {
            if (@this == null)
            {
                return new Nothing<TValue>();
            }
            else
            {
                return new Just<TValue>(()=>@this);
            }
        }

        public static IMaybe<TValue> ToMaybeAs<TValue>(
            this object @this)
        {
            if (@this is TValue)
            {
                return ((TValue)@this).ToMaybe();
            }
            else
            {
                return new Nothing<TValue>();
            }
        }

        public static IMaybe<TValue> SelectMany<TValue>(
            this IMaybe<IMaybe<TValue>> @this)
        {
            return @this.Return(()=>new Nothing<TValue>());
        }

        public static IMaybe<TNew> SelectMany<TValue, TNew>(
           this IMaybe<IMaybe<TValue>> @this
            , Func<TValue,TNew> selector)
        {
            return @this.Select( x => x.Select(selector)).SelectMany();
        }

        public static IMaybe<TResult> SelectMany<TValue, TResult>(
            this IMaybe<TValue> @this,
            Func<TValue, IMaybe<TResult>> selector)
        {
            return @this.Select(selector).SelectMany();
        }

        public static IMaybe<TReturn> ApplyMaybe<T1, TReturn>(
          this Func<T1, TReturn> func
           , IMaybe<T1> f1)
        {
            return
                f1.Select(func);
        }

        public static Func<IMaybe<T2>, IMaybe<TReturn>> ApplyMaybe<T1, T2, TReturn>(
           this Func<T1, T2, TReturn> func
            , IMaybe<T1> f1)
        {
            return
                (IMaybe<T2> f2) =>
                    f1.Select(t1 =>
                    f2.Select(t2 =>
                    func(t1, t2)))
                    .SelectMany();
        }

        public static IMaybe<TReturn> ApplyMaybe<T2, TReturn>(
           this Func<IMaybe<T2>, IMaybe<TReturn>> func
            , IMaybe<T2> f2)
        {
            return
                func(f2);
        }

        public static Func<IMaybe<T2>, IMaybe<TNew>> Apply<T1, T2, TNew>(
            this IMaybe<T1> @this
            , Func<T1, T2, TNew> selector)
        {
            return
                (IMaybe<T2> f2) =>
                    @this.Select(t1 =>
                        f2.Select(t2 =>
                            selector(t1, t2)))
                                .SelectMany();
        }

        public static IMaybe<TResult> SelectMany<TSource, TSelected, TResult>(
            this IMaybe<TSource> @this,
            Func<TSource, IMaybe<TSelected>> maybeSelector,
            Func<TSource, TSelected, TResult> resultSelector)
        {
            var apply = @this.Apply(resultSelector);
            var second = @this.Select(maybeSelector).SelectMany();
            return apply(second);
        }
        






        public static IMaybe<TValue> Where<TValue>(
            this IMaybe<TValue> @this
            , Func<TValue, bool> predicate)
        {
            var predicated = 
                    @this.ToCaseOf()
                    .Match((Just<TValue> just) => predicate(just.Value))
                    .Return(false);
            return
                @this.ToGuards()
                .When(_ => predicated, m => m)
                .Return(_ => new Nothing<TValue>());
        }

        public static IEnumerable<T> Concat<T>(this IMaybe<T> @this, IMaybe<T> second)
        {
            if (@this.IsSomething)
            {
                yield return @this.Return();
            }

            if (second.IsSomething)
            {
                yield return second.Return();
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> @this, IMaybe<T> second)
        {
            foreach (var item in @this)
            {
                yield return item;
            }

            if (second.IsSomething)
            {
                yield return second.Return();
            }
        }


        public static TValue Return<TValue>(
            this IMaybe<TValue> @this
            , TValue defaultValue = default(TValue))
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                return just.Value;
            }
            else
            {
                return defaultValue;
            }
        }


        public static TValue Return<TValue>(
            this IMaybe<TValue> @this
            , Func<TValue> getDefaultValue)
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                return just.Value;
            }
            else
            {
                return getDefaultValue();
            }
        }

        public static IMaybe<TValue> On<TValue>(
            this IMaybe<TValue> @this
            , Action<TValue> action)
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                action(just.Value);
            }
            return @this;
        }

        public static IMaybe<TValue> Or<TValue>(
            this IMaybe<TValue> @this
            , Action action)
        {
            if (@this.IsNothing)
            {
                action();
            }

            return @this;
        }
    }
}
