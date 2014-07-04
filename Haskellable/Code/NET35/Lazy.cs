using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haskellable.NET35
{
    public class Lazy<T>
    {
        private Func<T> getValue;
        private bool hasValue;
        private T cache;

        public Lazy(Func<T> getValue)
        {
            this.getValue = getValue;
        }

        public T Value
        {
            get
            {
                if (this.hasValue == false)
                {
                    this.cache = this.getValue();
                    this.hasValue = true;
                }
                return this.cache;
            }
        }
    }
}
