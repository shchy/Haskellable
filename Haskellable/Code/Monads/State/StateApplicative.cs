using Haskellable.Code.Monads.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if NET35
using Haskellable.NET35;
#endif

namespace System
{
    public static class StateApplicative
    {
        public static IState<TState, TValue> ToState<TState, TValue>(this TValue v)
        {
            return new State<TState, TValue>(s => Tuple.Create(s, v));
        }

        public static IState<TState, TNewValue> SelectMany<TState, TValue, TNewValue>(
            this IState<TState, TValue> @this
            , Func<TValue, IState<TState, TNewValue>> selector)
        {
            return new State<TState, TNewValue>(s =>
            {
                var tpl = @this.Run(s);
                return selector(tpl.Item2).Run(tpl.Item1);
            });
        }

        public static IState<TState, TNewValue> SelectMany<TState, TValue, TSelected, TNewValue>(
            this IState<TState, TValue> @this
            , Func<TValue, IState<TState, TSelected>> selector
            , Func<TValue, TSelected, TNewValue> result)
        {
            return 
                
                @this.SelectMany(
                    x => 
                        selector(x)
                        .SelectMany(
                            y => 
                                result(x, y).ToState<TState, TNewValue>() ));
        }

        public static IState<TState, TState> Get<TState>()
        {
            return new State<TState, TState>(s => Tuple.Create(s, s));
        }

        public static IState<TState, Unit> Put<TState>(TState s)
        {
            return new State<TState, Unit>(_ => Tuple.Create(s, Unit.Value));
        }




    }
}
