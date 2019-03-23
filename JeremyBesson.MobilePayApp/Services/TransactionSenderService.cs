using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;

namespace JeremyBesson.MobilePayApp.Services
{
    public class TransactionSenderService
    {
        public void Send(Transaction transaction)
        {
            var nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };
            Console.WriteLine($"{transaction.Date:yyyy-MM-dd} {transaction.MerchantName} {transaction.Amount.ToString("##0.00", nfi)}");
        }
    }
}
