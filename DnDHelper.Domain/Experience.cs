using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public class Experience : BaseEntity
    {
        public DateTime? Time { get; set; }

        public int Amount { get; set; }
    }
}
