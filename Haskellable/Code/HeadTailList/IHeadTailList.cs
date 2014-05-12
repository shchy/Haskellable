using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.HeadTailList
{
    public interface IHeadTailList<T> : IEnumerable<T>
    {
        T Head { get; }
        IHeadTailList<T> Tail { get; }
    }
}
