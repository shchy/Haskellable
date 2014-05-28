using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{

#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class Fn
    {
        public static Func<T1> New<T1>(Func<T1> x) { return x; }
        public static Func<T1, T2> New<T1, T2>(Func<T1, T2> x) { return x; }
        public static Func<T1, T2, T3> New<T1, T2, T3>(Func<T1, T2, T3> x) { return x; }
        public static Func<T1, T2, T3, T4> New<T1, T2, T3, T4>(Func<T1, T2, T3, T4> x) { return x; }
        public static Func<T1, T2, T3, T4, T5> New<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> x) { return x; }
    }


#if !DEBUG
        [DebuggerStepThrough]
#endif
    public static class Act
    {
        public static Action New(Action x) { return x; }
        public static Action<T1> New<T1>(Action<T1> x) { return x; }
        public static Action<T1, T2> New<T1, T2>(Action<T1, T2> x) { return x; }
        public static Action<T1, T2, T3> New<T1, T2, T3>(Action<T1, T2, T3> x) { return x; }
        public static Action<T1, T2, T3, T4> New<T1, T2, T3, T4>(Action<T1, T2, T3, T4> x) { return x; }
        public static Action<T1, T2, T3, T4, T5> New<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> x) { return x; }
    }
}
