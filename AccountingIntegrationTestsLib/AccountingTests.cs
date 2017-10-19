using AccountingLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace AccountingIntegrationTestsLib
{
    public class AccountingTests
    {
        private readonly ITestOutputHelper output;

        public AccountingTests(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public void ImportSIE()
        {

            Accounting sut = new Accounting();
            sut.AddAccount("T", "Tillgångar", true);
            sut.AddAccount("S", "Skulder", true);
            sut.AddAccount("I", "Inkomster", true);
            sut.AddAccount("K", "Kostnader", true);

            var fileName = "MATTIAS0_SIE4 2015-09-01 - 2016-08-31.SE.txt";
            var reader = File.OpenRead(fileName);
            var stringReader = new StreamReader(reader);
            var rowCount = 0;
            List<Tuple<string, decimal>> entries = null;
            while (true)
            {
                string line = stringReader.ReadLine();
                if (line == null)
                    break;
                if (line.StartsWith("#KTYP"))
                {
                    string pattern = @"#KTYP (\d{4}) (.)";
                    var match = Regex.Match(line, pattern);
                    sut.AddAccount(match.Groups[1].Value, "", false);
                    sut.MapParentToChild(match.Groups[2].Value, match.Groups[1].Value);
                }
                if (line.StartsWith("#VER"))
                {
                    entries = new List<Tuple<string, decimal>>();
                }
                if (line.StartsWith("   #TRANS"))
                {
                    string pattern = @"#TRANS (\d{4}) {} (-?\d*.\d*)";
                    var match = Regex.Match(line, pattern);
                    var amount = decimal.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                    entries.Add(new Tuple<string, decimal>(match.Groups[1].Value, amount));
                }
                if (line == "}")
                {
                    sut.AddTransaction(entries);
                }
                rowCount++;
            }
            output.WriteLine(rowCount.ToString());
            output.WriteLine("Tillgångar: " + sut.AccountBalance("T").ToString());
            output.WriteLine("Intäkter: " + sut.AccountBalance("I").ToString());
        }
    }
}
