using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public class FeeRuleWithDiscount : IFeeRule
    {
        private readonly List<IDiscountRule> _discounts;
        private readonly IFeeRule _rule;

        public FeeRuleWithDiscount(IFeeRule feeRule, List<IDiscountRule> discounts)
        {
            _rule = feeRule;
            _discounts = discounts;
        }

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            var initialFee = _rule.ComputeFee(transaction, currentFee);
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
