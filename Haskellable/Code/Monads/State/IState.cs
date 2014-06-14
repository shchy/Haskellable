using Haskellable.Code.Functor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.State
{
    public interface IState<TState, TValue> : IFunctor<TValue>
    {
        Func<TState, Tuple<TState, TValue>> Run { get; }
    }
}
