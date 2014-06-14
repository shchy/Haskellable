using Haskellable.Code.Monads.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class StateFunctor
    {
        public static IState<TState, TNewValue> Select<TState, TValue, TNewValue>(
            this IState<TState, TValue> @this
            , Func<TValue, TNewValue> selector)
        {
            return @this.FMap(selector) as IState<TState, TNewValue>;
        }
    }
}
