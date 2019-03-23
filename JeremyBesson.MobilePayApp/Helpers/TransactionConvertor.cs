using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;

namespace JeremyBesson.MobilePayApp.Helpers
{
    public static class TransactionConvertor
    {
        private static NumberFormatInfo nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public static bool IsEmptyLine(string fileLine)
        {
            while (fileLine.Contains("/t")) fileLine = fileLine.Replace("/t", "");
            while (fileLine.Contains("  ")) fileLine = fileLine.Replace(" ", "");

            return string.IsNullOrEmpty(fileLine);
        }

        public static Transaction Convert(string fileLine)
        {
            while (fileLine.Contains("/t")) fileLine = fileLine.Replace("/t", " ");
            while (fileLine.Contains("  ")) fileLine = fileLine.Replace("  ", " ");
            var c = fileLine.Split(' ');
            
            Transaction transaction = new Transaction()
            {
                Date = DateTime.Parse(c[0]),
                MerchantName = c[1],
                Amount = double.Parse(c[2],nfi),
            };

            return transaction;
        }

        public static string Convert(Transaction transaction)
        {
            return
                $"{transaction.Date:yyyy-MM-dd} {transaction.MerchantName} {transaction.Amount.ToString("0.00", nfi)}";
        }
    }
}
