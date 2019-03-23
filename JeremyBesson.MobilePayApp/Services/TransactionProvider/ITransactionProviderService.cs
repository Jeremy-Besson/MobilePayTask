using System.Collections.Generic;
using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionProvider
{
    public interface ITransactionProvider :IEnumerable<Transaction>
    {
    }
}
