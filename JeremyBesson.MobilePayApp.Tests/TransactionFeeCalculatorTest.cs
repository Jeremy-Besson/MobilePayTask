using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Services.TransactionFeeRules;
using System;
using System.Collections.Generic;
using Xunit;

namespace JeremyBesson.MobilePayApp.Tests
{
    public class TransactionFeeCalculatorTest
    {

        private const double BaseFeePercentage = 0.01;

        [Fact]
        public void WhenNoFeeRule_FeeIsZero()
        {
            //Arrange
            Transaction transaction = new Transaction()
            {
                Date = DateTime.Now,
                MerchantName = "",
                Amount = 20
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(new List<IFeeRule>());
            var fee = transactionFeeCalculator.CalculateFee(transaction);

            //Assert
            Assert.Equal(0,fee.Amount);
        }

        [Fact]
        public void WhenOnePercentFeeRule_FeeIsOnePercent()
        {
            //Arrange
            var amount = 100;

            Transaction transaction = new Transaction()
            {
                Date = DateTime.Now,
                MerchantName = "",
                Amount = amount
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new FeeRule((x,y) =>  x.Amount * BaseFeePercentage),
                }
                );
            var fee = transactionFeeCalculator.CalculateFee(transaction);

            //Assert
            Assert.Equal(amount * BaseFeePercentage, fee.Amount);
        }

        [Fact]
        public void WhenDiscountIsAvailableButNotApplicable_DiscountNotApplied()
        {
            //Arrange
            var amount = 100;
            var companyName = "COMPANYWITHDISCOUNT";

            Transaction transaction = new Transaction()
            {
                Date = DateTime.Now,
                MerchantName = "OTHERCOMPANY",
                Amount = amount
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new FeeRuleWithDiscount(new FeeRule((x,y) =>  x.Amount * BaseFeePercentage),
                        new List<IDiscountRule>()
                        {
                            new MerchantDiscountRule(companyName,0.1),
                        }
                    )
                }
            );
            var fee = transactionFeeCalculator.CalculateFee(transaction);

            //Assert
            Assert.Equal(amount * BaseFeePercentage, fee.Amount);
        }

        [Fact]
        public void WhenDiscountIsAvailableAndApplicable_DiscountApplied()
        {
            //Arrange
            var amount = 100;
            var discount = 0.1;
            var companyName = "COMPANYWITHDISCOUNT";

            Transaction transaction = new Transaction()
            {
                Date = DateTime.Now,
                MerchantName = companyName,
                Amount = amount
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new FeeRuleWithDiscount(new FeeRule((x,y) =>  x.Amount * BaseFeePercentage),
                        new List<IDiscountRule>()
                        {
                            new MerchantDiscountRule(companyName,discount),
                        }
                    )
                }
            );
            var fee = transactionFeeCalculator.CalculateFee(transaction);

            //Assert
            Assert.Equal((amount * BaseFeePercentage) * (1- discount), fee.Amount);
        }


        [Fact]
        public void WhenDiscountBiggerThenAmount_FeeIsZero()
        {
            //Arrange
            var amount = 100;

            Transaction transaction = new Transaction()
            {
                Amount = amount
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new FeeRuleWithDiscount(new FeeRule((x,y) =>  x.Amount * BaseFeePercentage),
                        new List<IDiscountRule>()
                        {
                            new DiscountRule( x => 1000),
                        }
                    )
                }
            );
            var fee = transactionFeeCalculator.CalculateFee(transaction);

            //Assert
            Assert.Equal(0, fee.Amount);
        }

        [Fact]
        public void MonthlyFeesAreAppliedCorrectly()
        {
            //Arrange
            Transaction transaction1 = new Transaction()
            {
                MerchantName = "NAME",
                Amount = 100,
                Date = new DateTime(2019,1,10)
            };

            Transaction transaction2 = new Transaction()
            {
                MerchantName = "NAME",
                Amount = 100,
                Date = new DateTime(2019, 1, 20)
            };

            Transaction transaction3 = new Transaction()
            {
                MerchantName = "NAME",
                Amount = 100,
                Date = new DateTime(2019, 2, 20)
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new FeeRule((x,y) =>  x.Amount * BaseFeePercentage),
                    new MonthlyInvoiceFeeRule()
                }
            );

            var baseFee = 100 * BaseFeePercentage;
            var fee1 = transactionFeeCalculator.CalculateFee(transaction1);
            var fee2 = transactionFeeCalculator.CalculateFee(transaction2);
            var fee3 = transactionFeeCalculator.CalculateFee(transaction3);

            //Assert
            //var baseFeePercentage = 100 * 
            Assert.Equal(29 + baseFee,fee1.Amount);
            Assert.Equal(0 + baseFee, fee2.Amount);
            Assert.Equal(29 + baseFee, fee3.Amount);
        }

        [Fact]
        public void WhenFeeIsZero_MonthlyFeesNotApplied()
        {
            //Arrange
            Transaction transaction1 = new Transaction()
            {
                MerchantName = "NAME",
                Amount = 100,
                Date = new DateTime(2019, 1, 10)
            };

            //Act
            var transactionFeeCalculator = new TransactionFeeCalculator(
                new List<IFeeRule>()
                {
                    new MonthlyInvoiceFeeRule()
                }
            );
            var fee1 = transactionFeeCalculator.CalculateFee(transaction1);

            //Assert
            Assert.Equal(0, fee1.Amount);
        }
    }
}
