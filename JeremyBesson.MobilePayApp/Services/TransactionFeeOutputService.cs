using JeremyBesson.MobilePayApp.Models;
using System;
using System.Globalization;

namespace JeremyBesson.MobilePayApp.Services
{
    public class TransactionFeeOutputService
    {
        public void Send(TransactionFee transactionFee)
        {
            if (transactionFee != null)
            {
                var nfi = new NumberFormatInfo { NumberDecimalSeparator = "." };
                Console.WriteLine(
                    $"{transactionFee.Date:yyyy-MM-dd} {transactionFee.MerchantName} {transactionFee.Amount.ToString("##0.00", nfi)}");
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}
