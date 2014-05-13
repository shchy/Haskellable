using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
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
                return new Just<TValue>(@this);
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

        public static Func<IMaybe<TValue>, IMaybe<TReturn>> Apply<TValue, TReturn>(
            this IMaybe<Func<TValue, TReturn>> @this)
        {
            var selector = @this.Return(_=>default(TReturn));
            return
                Fn.New<IMaybe<TValue>, IMaybe<TReturn>>(m => m.Select(selector));
        }

        public static Func<IMaybe<T1>, IMaybe<T2>, IMaybe<TReturn>> Apply<T1, T2, TReturn>(
            this IMaybe<Func<T1, T2, TReturn>> @this)
        {
            var selector = @this.Return((_,__)=>default(TReturn));
            return
                    Fn.New<IMaybe<T1>, IMaybe<T2>, IMaybe<TReturn>>(
                        (m1, m2) =>
                            from n1 in m1
                            from n2 in m2
                            select selector(n1, n2));
        }


        //public static Func<IMaybe<TValue>, IMaybe<TReturn>> Apply<TValue, TReturn>(
        //    this Func<TValue, TReturn> @this)
        //{
        //    var selector = @this.Return(Fn.New<TValue, TReturn>(_ => default(TReturn)));
        //    return
        //            Fn.New<IMaybe<TValue>, IMaybe<TReturn>>(m => m.Select(selector));
        //}


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
    
    }
}
