using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Models
{
    class AccountInfo
    {
        public List<string> Text { get; set; }
        public List<MoreInfo> MoreAccountInfo { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public float Cash { get; set; }
    }
    public class MoreInfo
    {
        public string PropOne { get; set; }
        public string PropTwo { get; set; }
        public string PropThree { get; set; }
        public string[] MoreText { get; set; }
    }
}
