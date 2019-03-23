using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class MonthlyFeeCalculator : IFeeCalculator
    {
        private const double MonthlyFixedCost = 29;

        private readonly Dictionary<string,int> _monthlyPayedFees = new Dictionary<string, int>();

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            var additionalFee = 0.0;

            if (currentFee > 0.0)
            {
                var lastPayedMonth = -1;

                if (_monthlyPayedFees.ContainsKey(transaction.MerchantName))
                {
                    lastPayedMonth = _monthlyPayedFees[transaction.MerchantName];
                }

                if (lastPayedMonth != transaction.Date.Month)
                {
                    additionalFee = MonthlyFixedCost;
                }

                _monthlyPayedFees[transaction.MerchantName] = transaction.Date.Month;
            }

            return additionalFee;
        }
    }
}
