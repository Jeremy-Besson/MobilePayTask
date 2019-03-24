using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public interface IFeeCalculator
    {
        double ComputeFee(Transaction transaction, double currentFee);
    }
}
