using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN17001
{
    public class SN17001Exception : Exception
    {
        public SN17001Exception(string message) : base("SN17001Exception: " + message)
        {


        }
    }
}


