using Haskellable.Code.Functor;
using Haskellable.Code.Monads.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET35
using Haskellable.NET35;
#endif

namespace Haskellable.Code.Monads.State
{
    public class State<TState, TValue> : IState<TState, TValue>
    {
        public Func<TState, Tuple<TState, TValue>> Run { get; private set; }
        
        public State(Func<TState, Tuple<TState, TValue>> run)
        {
            this.Run = run;
        }

        public IFunctor<TNew> FMap<TNew>(Func<TValue, TNew> selector)
        {
            return new State<TState, TNew>( 
                s => Tuple.Create(s, selector(this.Run(s).Item2))
            );
        }
    }

    public class Unit
    {
        public static readonly Unit Value = new Unit();
        private Unit()
        {

        }
        
    }
}

