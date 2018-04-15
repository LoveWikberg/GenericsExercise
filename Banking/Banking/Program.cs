using Banking.Helpers;
using System;

namespace Banking
{
    class Program
    {
        static void Main(string[] args)
        {
            Runtime run = new Runtime(new TextFileUtilities());
            run.Start();
        }
    }
}
