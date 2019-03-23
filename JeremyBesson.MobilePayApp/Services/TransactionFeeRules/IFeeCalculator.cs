using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public interface IFeeCalculator
    {
        double ComputeFee(Transaction transaction, double currentFee);
    }
}
