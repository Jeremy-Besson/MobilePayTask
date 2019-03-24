using System;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public class FeeRule : IFeeRule
    {
        private readonly Func<Transaction, double,double> _feeCalculator;

        public FeeRule(Func<Transaction,double,double> feeCalculator)
        {
            _feeCalculator = feeCalculator;
        }

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            return _feeCalculator(transaction, currentFee);
        }
    }
}
