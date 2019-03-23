﻿using System;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public class FeeCalculator : IFeeCalculator
    {
        private readonly Func<Transaction, double,double> _feeCalculator;

        public FeeCalculator(Func<Transaction,double,double> feeCalculator)
        {
            _feeCalculator = feeCalculator;
        }

        public double ComputeFee(Transaction transaction, double currentFee)
        {
            return _feeCalculator(transaction, currentFee);
        }
    }
}
