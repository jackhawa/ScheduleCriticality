using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchedulePath.Helper
{
    public interface ICloneable<T>
    {
        T Clone();
    }

}
