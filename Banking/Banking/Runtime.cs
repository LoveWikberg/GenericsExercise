using Banking.Helpers;
using Banking.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Banking
{
    class Runtime
    {
        private readonly TextFileUtilities textFileUtilities;

        public Runtime(TextFileUtilities textFileUtilities)
        {
            this.textFileUtilities = textFileUtilities;
        }

        public void Start()
        {
            List<AccountInfo> accounts = new List<AccountInfo>
            {
                new AccountInfo{Cash = 20030, Id= Guid.NewGuid(), Name = "Alfons", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } }}, Text = new List<string>{"hej", "tja", "elefant", "sen" } },
                new AccountInfo{Cash = 20030, Id= Guid.NewGuid(), Name = "Jennie", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } }, Text = new List<string>{"hej", "tja", "elefant", "sen" } },
                new AccountInfo{Cash = 560030, Id= Guid.NewGuid(), Name = "Erik", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } }, Text = new List<string>{"hej", "tja", "elefant", "sen" } },
                new AccountInfo{Cash = 287030, Id= Guid.NewGuid(), Name = "Jonas", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } }, Text = new List<string>{"hej", "tja", "elefant", "sen" } },
                new AccountInfo{Cash = 20080, Id= Guid.NewGuid(), Name = "Eva", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } } , Text = new List<string>{"hej", "tja", "elefant", "sen" }},
                new AccountInfo{Cash = 2003450, Id= Guid.NewGuid(), Name = "Carl-jan", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo { PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } }, Text = new List<string>{"hej", "tja", "elefant", "sen" }},
                new AccountInfo{Cash = 21240, Id= Guid.NewGuid(), Name = "Eva", MoreAccountInfo = new List<MoreInfo>{ new MoreInfo{PropOne = "test", PropThree = "halle", PropTwo = "bred", MoreText = new string[] { "kille", "tjej", "trans" } } }, Text = new List<string>{"hej", "tja", "elefant", "sen" } }
            };

            //string[] content = textFileUtilities.TextFileSerialize(accounts);
            //string fullPath = $"{Directory.GetCurrentDirectory().Replace("\\bin\\Debug\\netcoreapp2.0", "")}\\TextFiles\\accounts.txt";
            //File.AppendAllLines(fullPath, content);
            //textFileUtilities.WriteToTextFile(Directory.GetCurrentDirectory(), "accounts", content);
            var acounts = textFileUtilities.TextFileDeserialize<AccountInfo>();
        }
    }
}
