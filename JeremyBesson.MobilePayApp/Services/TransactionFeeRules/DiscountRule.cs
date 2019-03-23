using System;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class DiscountRule : IDiscountRule
    {
        private readonly Func<Transaction, double> _discount;

        public DiscountRule(Func<Transaction,double> discount)
        {
            _discount = discount;
        } 

        public double ComputeDiscount(Transaction transaction, double initialFee)
        {
            return _discount(transaction);
        }
    }
}
