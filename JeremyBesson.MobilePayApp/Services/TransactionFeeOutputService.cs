using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;
using JeremyBesson.MobilePayApp.Helpers;

namespace JeremyBesson.MobilePayApp.Services
{
    public class TransactionFeeOutputService
    {
        public void Send(TransactionFee transactionFee)
        {
            if (transactionFee != null)
            {
                var output = TransactionFeeConvertor.Convert(transactionFee);
                Console.WriteLine(output);
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}
