using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public interface IFeeRule
    {
        double ComputeFee(Transaction transaction, double currentFee);
    }
}
