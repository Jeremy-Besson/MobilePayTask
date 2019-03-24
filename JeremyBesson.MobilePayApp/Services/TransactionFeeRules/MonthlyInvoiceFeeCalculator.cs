using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public class MonthlyInvoiceFeeCalculator : IFeeCalculator
    {
        private const double MonthlyFixedCost = 29;

        private readonly Dictionary<string,int> _monthlyPayedInvoiceFees = new Dictionary<string, int>();

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            var additionalFee = 0.0;

            if (currentFee > 0.0)
            {
                var lastPayedMonth = -1;

                if (_monthlyPayedInvoiceFees.ContainsKey(transaction.MerchantName))
                {
                    lastPayedMonth = _monthlyPayedInvoiceFees[transaction.MerchantName];
                }

                if (lastPayedMonth != transaction.Date.Month)
                {
                    additionalFee = MonthlyFixedCost;
                }

                _monthlyPayedInvoiceFees[transaction.MerchantName] = transaction.Date.Month;
            }

            return additionalFee;
        }
    }
}
