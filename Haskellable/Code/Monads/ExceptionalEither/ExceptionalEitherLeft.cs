using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.ExceptionalEither
{

#if !DEBUG
    [DebuggerStepThrough]
#endif

    public class ExceptionalEitherLeft<TValue>
        : EitherLeft<Exception, TValue>
        , IExceptionalEither<TValue>
    {
        public ExceptionalEitherLeft(Exception value)
            : base(value)
        {
        }
    }
}
