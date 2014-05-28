using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.CaseOf
{
#if !DEBUG
        [DebuggerStepThrough]
#endif
    public class CaseOfSeed<TValue> 
	{
        private TValue value;


        public CaseOfSeed(TValue value)
	    {
            this.value = value;
	    }

        public CaseOf<TValue, TReturn> Match<TNewValue, TReturn>(Func<TNewValue, TReturn> matchCase)
        {
            return
                new CaseOf<TValue, TReturn>(this.value)
                .Match(matchCase);
        }
	}


#if !DEBUG
        [DebuggerStepThrough]
#endif
    public class CaseOf<TValue, TReturn> 
    {
        private TValue value;
        private IMaybe<TReturn> returnValue;


        public CaseOf(TValue value)
        {
            this.value = value;
            this.returnValue = new Nothing<TReturn>();
        }

        public CaseOf<TValue, TReturn> Match<TNewValue>(Func<TNewValue, TReturn> selector)
        {
            this.returnValue
                .Or(
                () =>
                {
                    this.value.ToMaybeAs<TNewValue>()
                        .On(
                        v =>
                        {
                            this.returnValue = selector(v).ToMaybe();
                        });
                });

            return this;
        }
        

        public TReturn Return(Func<TValue, TReturn> otherwise)
        {
            return
                this.returnValue
                .Return(()=> otherwise(this.value));
        }

        public TReturn Return(TReturn otherwise)
        {
            return
                this.returnValue
                .Return(otherwise);
        }
    }
}
