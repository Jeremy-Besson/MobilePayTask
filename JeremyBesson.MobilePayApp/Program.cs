using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Services;
using System;
using System.IO;

namespace JeremyBesson.MobilePayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var enumeratorService = new TransactionEnumeratorService();
                var feeCalculator = new TransactionFeeCalculatorService();
                var feeSender = new TransactionFeeOutputService();

                using (var transactions = enumeratorService.GetEnumerator())
                {
                    while (transactions.MoveNext())
                    {
                        var current = transactions.Current;
                        TransactionFee fee = null;
                        if (!current.IsEmpty)
                        {
                            fee = feeCalculator.CalculateFee(current);
                        }
                        feeSender.Send(fee);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The 'transactions.txt' file is missing");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oups: Something went wrong: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
