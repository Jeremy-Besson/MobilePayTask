using JeremyBesson.MobilePayApp.Models;
using JeremyBesson.MobilePayApp.Services.TransactionProvider;
using System.Collections;
using System.Collections.Generic;

namespace JeremyBesson.MobilePayApp.Services
{

    public class TransactionEnumeratorService : IEnumerable<Transaction>
    {
        private readonly string fileName = "transactions.txt";

        public IEnumerator<Transaction> GetEnumerator()
        {
            return new TransactionFileEnumerator(fileName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
