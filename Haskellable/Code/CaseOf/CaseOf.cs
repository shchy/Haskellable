using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.CaseOf
{
    public class CaseOfSeed
	{
        private object value;
		public CaseOfSeed (object value)
	    {
            this.value = value;
	    }

        public CaseOf<TReturn> Match<TValue, TReturn>(Func<TValue, TReturn> matchCase)
        {
            return
                new CaseOf<TReturn>(this.value)
                .Match(matchCase);
        }
	}

    public class CaseOf<TReturn>
    {
        private object value;
        private IMaybe<TReturn> returnValue;

        public CaseOf(object value)
        {
            this.value = value;
            this.returnValue = Maybe.Nothing<TReturn>();
        }

        public CaseOf<TReturn> Match<TValue>(Func<TValue, TReturn> selector)
        {
            this.returnValue
                .Or(() =>
                {
                    var query =
                        from v in this.value.ToMaybeAs<TValue>()
                        select selector(v);
                    this.returnValue = query;
                });

            return this;
        }

        public TReturn Return(Func<TReturn> otherwise)
        {
            return
                this.returnValue
                .Return(otherwise);
        }

        public TReturn Return(TReturn otherwise)
        {
            return
                this.returnValue
                .Return(otherwise);
        }
    }
}
