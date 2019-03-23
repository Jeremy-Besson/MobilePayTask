using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;

namespace JeremyBesson.MobilePayApp.Helpers
{
    public static class TransactionConvertor
    {
        private static readonly NumberFormatInfo Nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public static Transaction Convert(string fileLine)
        {
            fileLine = fileLine.Trim();
            while (fileLine.Contains("/t")) fileLine = fileLine.Replace("/t", " ");
            while (fileLine.Contains("  ")) fileLine = fileLine.Replace("  ", " ");

            Transaction transaction;
            if (string.IsNullOrEmpty(fileLine.Trim()))
            {
                 transaction= new Transaction()
                 {
                     IsEmpty = true
                 };
            }
            else
            {
                var c = fileLine.Split(' ');
                transaction = new Transaction()
                {
                    Date = DateTime.Parse(c[0]),
                    MerchantName = c[1],
                    Amount = double.Parse(c[2], Nfi),
                };
            }

            return transaction;
        }

        public static string Convert(Transaction transaction)
        {
            return
                $"{transaction.Date:yyyy-MM-dd} {transaction.MerchantName} {transaction.Amount.ToString("0.00", Nfi)}";
        }
    }
}
