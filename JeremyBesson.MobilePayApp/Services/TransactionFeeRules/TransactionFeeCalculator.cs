using JeremyBesson.MobilePayApp.Models;
using System.Collections.Generic;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class TransactionFeeCalculator
    {
        private readonly List<IFeeCalculator> _feeCalculators;

        public TransactionFeeCalculator(List<IFeeCalculator> feeCalculators)
        {
            _feeCalculators = feeCalculators;
        }

        public TransactionFee CalculateFee(Transaction transaction)
        {
            var fee = 0.0;
            var lastFee = 0.0;
            _feeCalculators.ForEach(
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
