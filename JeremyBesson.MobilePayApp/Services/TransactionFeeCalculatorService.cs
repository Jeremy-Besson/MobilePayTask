using JeremyBesson.MobilePayApp.Models;
using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Services.TransactionFeeRules;

namespace JeremyBesson.MobilePayApp.Services
{
    class TransactionFeeCalculatorService
    {
        private const double TransactionFeePercentage = 0.01;

        private readonly List<IFeeRule> _feeCalculators = new List<IFeeRule>()
        {
            new FeeRuleWithDiscount(new FeeRule((x,y) =>  x.Amount * TransactionFeePercentage), 
                new List<IDiscountRule>()
                {
                    new MerchantDiscountRule("TELIA",0.1),
                    new MerchantDiscountRule("CIRCLE_K",0.2),
                }
                ),

            new MonthlyInvoiceFeeRule(),
        };

        private readonly TransactionFeeCalculator _feeCalculator;

        public TransactionFeeCalculatorService()
        {
            _feeCalculator = new TransactionFeeCalculator(_feeCalculators);
        }

        public TransactionFee CalculateFee(Transaction transaction)
        {
            return _feeCalculator.CalculateFee(transaction);
        }
    }
}
