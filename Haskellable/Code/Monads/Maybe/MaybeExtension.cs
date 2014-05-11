using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class MaybeExtension
    {
        public static IMaybe<TValue> ToMaybe<TValue>(this TValue @this)
        {
            if (@this == null)
            {
                return Maybe.Nothing<TValue>();
            }
            else
            {
                return Maybe.New(@this);
            }
        }
        
        public static IMaybe<TValue> ToMaybeAs<TValue>(this object @this)
        {
            if (@this is TValue)
            {
                return ((TValue)@this).ToMaybe();
            }
            else
            {
                return Maybe.Nothing<TValue>();
            }
        }

        public static TValue Return<TValue>(this IMaybe<TValue> @this, TValue defaultValue = default(TValue))
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


        public static TValue Return<TValue>(this IMaybe<TValue> @this, Func<TValue> getDefaultValue)
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

        public static IMaybe<TValue> On<TValue>(this IMaybe<TValue> @this, Action<TValue> action)
        {
            var just = @this as Just<TValue>;
            if (just != null)
            {
                action(just.Value);
            }
            return @this;
        }

        public static IMaybe<TValue> Or<TValue>(this IMaybe<TValue> @this, Action action)
        {
            if (@this.IsNothing)
            {
                action();
            }

            return @this;
        }
    }
}
