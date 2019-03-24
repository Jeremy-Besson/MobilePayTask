using JeremyBesson.MobilePayApp.Helpers;
using JeremyBesson.MobilePayApp.Models;
using System;

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
