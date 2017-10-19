using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingLib
{
    public class Entry
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
