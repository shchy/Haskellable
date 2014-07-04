using Haskellable.Code.Functor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET35
using Haskellable.NET35;
#endif

namespace Haskellable.Code.Monads.State
{
    public interface IState<TState, TValue> : IFunctor<TValue>
    {
        Func<TState, Tuple<TState, TValue>> Run { get; }
    }
}
