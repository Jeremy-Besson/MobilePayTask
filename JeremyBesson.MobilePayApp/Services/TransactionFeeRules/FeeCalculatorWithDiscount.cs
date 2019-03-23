using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class FeeCalculatorWithDiscount : IFeeCalculator
    {
        private readonly List<IDiscountRule> _discounts;
        private readonly IFeeCalculator _calculator;

        public FeeCalculatorWithDiscount(IFeeCalculator feeCalculator, List<IDiscountRule> discounts)
        {
            _calculator = feeCalculator;
            _discounts = discounts;
        }

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            var initialFee = _calculator.ComputeFee(transaction, currentFee);
            var discount = 0.0;

            _discounts?.ForEach(
                discountRule => { discount += discountRule.ComputeDiscount(transaction, initialFee); });

            var fee = initialFee - discount;
            if (fee < 0)
            {
                fee = 0.0;
            }

            return fee;
        }
    }
}
