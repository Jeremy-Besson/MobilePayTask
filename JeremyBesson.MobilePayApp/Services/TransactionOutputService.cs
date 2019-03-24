using JeremyBesson.MobilePayApp.Helpers;
using JeremyBesson.MobilePayApp.Models;
using System;

namespace JeremyBesson.MobilePayApp.Services
{
    //TODO: No need now
    public class TransactionOutputService
    {
        public void Send(Transaction transaction)
        {
            if (transaction != null && !transaction.IsEmpty)
            {
                Console.WriteLine(TransactionConvertor.Convert(transaction));
            }
            else
            {
                Console.WriteLine("");
            }
        }
    }
}
