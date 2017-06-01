using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodHand.Shared.Models
{
    public class Patch
    {
        public int ByteLength { get; set; }
        public int StartPosition { get; set; }
        public string Value { get; set; }
    }
}
