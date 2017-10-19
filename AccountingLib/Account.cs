using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountingLib
{
    public class Account
    {
        public Account()
        {
            Children = new List<Account>();
            Entries = new List<Entry>();
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Amount {
            get
            {
                if (IsSummary)
                    return Children.Sum(child => child.Amount);
                return Entries.Sum(entry => entry.Amount);
            }
        }
        public bool IsSummary { get; set; }
        public List<Account> Children { get; set; }
        public List<Entry> Entries { get; set; }
    }
}
