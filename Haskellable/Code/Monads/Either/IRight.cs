using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Either
{
    public interface IRight<T>
    {
        T Value { get; }

        IEither<TLeft, T> ToEither<TLeft>();
    }
}
