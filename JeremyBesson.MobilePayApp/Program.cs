using System;
using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Services;

namespace JeremyBesson.MobilePayApp
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            Console.WriteLine("Transactions:");
            var transactionSender = new TransactionSenderService();
            using (var transactions = new TransactionEnumeratorService().GetEnumerator())
            {
                while (transactions.MoveNext())
                {
                    transactionSender.Send(transactions.Current);
                }
            }
            Console.WriteLine("");
            Console.WriteLine("Transaction fees:");
#endif

            var feeCalculator = new TransactionFeeCalculatorService();
            var feeSender = new TransactionFeeSenderService();
            using (var transactions = new TransactionEnumeratorService().GetEnumerator())
            {
                while (transactions.MoveNext())
                {
                    var fee = feeCalculator.CalculateFee(transactions.Current);
                    feeSender.Send(fee);
                }
            }

            Console.ReadLine();
        }
    }
}
