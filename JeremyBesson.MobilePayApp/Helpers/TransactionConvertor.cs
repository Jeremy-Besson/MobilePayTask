using JeremyBesson.MobilePayApp.Models;
using System;

namespace JeremyBesson.MobilePayApp.Helpers
{
    public static class TransactionConvertor
    {
        public static Transaction Convert(string transactionString)
        {
            transactionString = transactionString.Trim();
            while (transactionString.Contains("/t")) transactionString = transactionString.Replace("/t", " ");
            while (transactionString.Contains("  ")) transactionString = transactionString.Replace("  ", " ");

            Transaction transaction;
            if (string.IsNullOrEmpty(transactionString.Trim()))
            {
                 transaction= new Transaction()
                 {
                     IsEmpty = true
                 };
            }
            else
            {
                var c = transactionString.Split(' ');
                transaction = new Transaction()
                {
                    Date = DateTime.Parse(c[0]),
                    MerchantName = c[1],
                    Amount = double.Parse(c[2], NumberFormat.Nfi),
                };
            }

            return transaction;
        }

        public static string Convert(Transaction transaction)
        {
            return
                $"{transaction.Date:yyyy-MM-dd} {transaction.MerchantName} {transaction.Amount.ToString("0.00", NumberFormat.Nfi)}";
        }
    }
}
