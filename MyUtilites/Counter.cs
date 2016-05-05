using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtilites
{
    class Counter
    {
        private int count = 0;

        public override string ToString()
        {
            return count.ToString();
        }

        public void Plus()
        {
            count++;
        }

        public void Minus()
        {
            count--;
        }

        public void Reset()
        {
            count = 0;
        }
    }
}
