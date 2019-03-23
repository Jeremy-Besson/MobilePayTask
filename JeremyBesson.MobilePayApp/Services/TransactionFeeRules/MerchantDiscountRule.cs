using System;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class MerchantDiscountRule : IDiscountRule
    {
        private readonly string _merchantName;
        private readonly double _percentageDiscount;

        public MerchantDiscountRule(string merchantName, double percentageDiscount)
        {
            if (string.IsNullOrEmpty(merchantName))
            {
                throw new ArgumentException("Merchant Name cannot be empty");
            }
            if (_percentageDiscount < 0 || _percentageDiscount > 1)
            {
                throw new ArgumentException("Discount % should be in [0-1]");
            }

            _merchantName = merchantName;
            _percentageDiscount = percentageDiscount;
        }

        public double ComputeDiscount(Transaction transaction, double initialFee)
        {
          return  transaction.MerchantName == _merchantName ? initialFee * _percentageDiscount : 0.0;
        }
    }
}
