﻿using Haskellable.Code.Monads.Maybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.CaseOf
{
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

    public class CaseOf<TValue, TReturn> 
    {
        private TValue value;
        private IMaybe<TReturn> returnValue;

        public CaseOf(TValue value)
        {
            this.value = value;
            this.returnValue = Maybe.Nothing<TReturn>();
        }

        public CaseOf<TValue, TReturn> Match<TNewValue>(Func<TNewValue, TReturn> selector)
        {
            this.returnValue
                .Or(() =>
                {
                    var query =
                        from v in this.value.ToMaybeAs<TNewValue>()
                        select selector(v);
                    this.returnValue = query;
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
