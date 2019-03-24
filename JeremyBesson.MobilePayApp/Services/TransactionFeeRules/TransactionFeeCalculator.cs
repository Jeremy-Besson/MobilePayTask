using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public class TransactionFeeCalculator
    {
        private readonly List<IFeeRule> _feeRules;

        public TransactionFeeCalculator(List<IFeeRule> feeRules)
        {
            _feeRules = feeRules;
        }

        public TransactionFee CalculateFee(Transaction transaction)
        {
            var fee = 0.0;
            var lastFee = 0.0;
            _feeRules.ForEach(
                feeCalculator =>
                {
                    lastFee = feeCalculator.ComputeFee(transaction, lastFee);
                    fee += lastFee;
                }
            );

            return new TransactionFee()
            {
                Date = transaction.Date,
                MerchantName = transaction.MerchantName,
                Amount = fee,
            };
        }
    }
}
