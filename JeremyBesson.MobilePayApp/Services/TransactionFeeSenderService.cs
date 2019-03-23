using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;

namespace JeremyBesson.MobilePayApp.Services
{
    public class TransactionFeeSenderService
    {
        public void Send(TransactionFee transactionFee)
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };
            Console.WriteLine($"{transactionFee.Date:yyyy-MM-dd} {transactionFee.MerchantName} {transactionFee.Amount.ToString("##0.00", nfi)}");
        }
    }
}
