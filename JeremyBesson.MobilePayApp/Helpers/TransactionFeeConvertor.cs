﻿using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Helpers
{
    public static class TransactionFeeConvertor
    {
        public static string Convert(TransactionFee transactionFee)
        {
            return
                $"{transactionFee.Date:yyyy-MM-dd} {transactionFee.MerchantName} {transactionFee.Amount.ToString("##0.00", NumberFormat.Nfi)}";
        }
    }
}
