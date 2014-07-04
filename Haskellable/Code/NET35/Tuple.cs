using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.NET35
{
    public class Tuple<T1,T2>
    {
        public Tuple(T1 t1, T2 t2)
        {
            this.Item1 = t1;
            this.Item2 = t2;
        }

        public T1 Item1 { get; private set; }

        public T2 Item2 { get; private set; }
    }

    public static class Tuple
    {
        public static Tuple<T1,T2> Create<T1,T2>(T1 t1, T2 t2)
        {
            return new Tuple<T1, T2>(t1, t2);
        }
        
    }
}
