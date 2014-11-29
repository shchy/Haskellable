﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.Code.Monads.Either
{
    public interface ILeft<T>
    {
        T Value { get; }

        IEither<T, TRight> ToEither<TRight>();
    }
}
