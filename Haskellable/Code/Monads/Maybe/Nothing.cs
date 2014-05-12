﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haskellable.Code.Monads.Maybe
{
    public class Nothing<T> : IMaybe<T>
    {
        public override string ToString()
        {
            return string.Format("Nothing {0}", typeof(T));
        }

        public bool IsNothing
        {
            get { return true; }
        }

        public bool IsSomething
        {
            get { return false; }
        }
    }
}