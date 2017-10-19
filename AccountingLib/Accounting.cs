using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AccountingLib
{
    public class Accounting
    {
        public decimal Surround {
            get
            {
                return _accounts.Sum(account => account.Amount);
            }
        }
        private List<Account> _accounts = new List<Account>();

        public void AddAccount(string code, string name, bool isSummary)
        {
            _accounts.Add(new Account { Code = code, Name = name, IsSummary=isSummary });
        }

        public void MapParentToChild(string parentCode, string accountCode)
        {
            var parent = GetAccount(parentCode);
            var child = GetAccount(accountCode);
            parent.Children.Add(child);
        }

        private Account GetAccount(string accountId)
        {
            return _accounts.SingleOrDefault(account => account.Code == accountId);
        }

        public decimal AccountBalance(string accountId)
        {
            return GetAccount(accountId).Amount;
        }

        public void AddTransactions(List<Tuple<string, string, decimal>> entries)
        {
            foreach (var tuple in entries)
            {
                AddTransaction(tuple.Item1, tuple.Item2, tuple.Item3);
            }
        }

        public void AddTransaction(string fromAccount, string toAccount, decimal amount)
        {
            GetAccount(fromAccount).Entries.Add(new Entry { Amount = amount });
            GetAccount(toAccount).Entries.Add(new Entry { Amount = -amount });
        }

        public void AddTransaction(List<Tuple<string, decimal>> entries)
        {
            foreach (var tuple in entries)
            {
                GetAccount(tuple.Item1).Entries.Add(new Entry { Amount = tuple.Item2 });
            }
        }
    }
}
