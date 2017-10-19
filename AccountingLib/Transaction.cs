using System;
using System.Collections.Generic;
using System.Text;

namespace AccountingLib
{
    public class Transaction
    {
        public Transaction()
        {
            Entries = new List<Entry>();
        }
        public string Description { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
