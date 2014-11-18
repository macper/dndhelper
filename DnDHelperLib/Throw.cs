using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    [Serializable]
    public class Throw
    {
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int EnduranceThrow { get; set; }
    }
}
