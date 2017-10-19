using AccountingLib;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountingTestsLib
{
    public class AccountingTests
    {
        [Fact]
        public void SurroundIsZero()
        {
            Accounting sut = new Accounting();
            sut.AddAccount("T", "Tillgångar", true);
            sut.AddAccount("S", "Skulder", true);
            sut.AddAccount("I", "Inkomster", true);
            sut.AddAccount("K", "Kostnader", true);
            sut.AddAccount("1010", "Kassa", false);
            sut.AddAccount("1040", "Handelsbanken", false);
            sut.MapParentToChild("T", "1010");
            sut.MapParentToChild("S", "1040");
            sut.AddTransaction("1010", "1040", 500);
            var entries = new List<Tuple<string, string, decimal>>();
            entries.Add(new Tuple<string, string, decimal>("1010", "1040", 500));
            entries.Add(new Tuple<string, string, decimal>("1040", "1010", 200));
            sut.AddTransactions(entries);
            Assert.Equal(sut.Surround, 0M);
            Assert.Equal(sut.AccountBalance("1010"), 500);
            Assert.Equal(sut.AccountBalance("1040"), -500);
        }
    }
}
