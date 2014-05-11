using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Guards
{
    public class GuardsSeed<TValue>
	{
        private  TValue value;
		public GuardsSeed (TValue value)
	    {
            this.value = value;
	    }

        public Guards<TValue, TReturn> When<TReturn>(
            Func<TValue,bool> when
            , Func<TValue,TReturn> selector)
        {
            return
                new Guards<TValue,TReturn>(this.value)
                .When(when,selector);
        }

        public GuardsSeed<TNew> Where<TNew>(Func<TValue, TNew> selector)
        {
            return
                new GuardsSeed<TNew>(selector(this.value));
        }
	}

    public class Guards<TValue, TReturn>
    {
        private TValue tValue;
        private IMaybe<TReturn> returnValue;

        public Guards(TValue tValue)
        {
            this.tValue = tValue;
            this.returnValue = Maybe.Nothing<TReturn>();
        }

        public Guards<TValue, TReturn> When(
            Func<TValue, bool> when
            , Func<TValue, TReturn> selector)
        {
            if (this.returnValue.IsSomething || when(this.tValue) == false)
            {
                return this;
            }

            this.returnValue = selector(this.tValue).ToMaybe();
            return this;
        }
        
        public TReturn Return(Func<TValue,TReturn> otherwise)
        {
            return
                this.returnValue.Return(() => otherwise(this.tValue));
        }
    }
    
}

