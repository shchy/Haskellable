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
    public class ExceptionalEitherRight<TValue> : EitherRight<Exception, TValue>, IExceptionalEither<TValue>
    {
        public ExceptionalEitherRight(TValue value)
            : base(value)
        {

        }
    }
}
