using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public class CharacterEventArgs : EventArgs
    {
        public Character Character { get; set; }
    }
}
