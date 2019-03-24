using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.TransactionFeeRules
{
    public interface IDiscountRule
    {
        double ComputeDiscount(Transaction transaction, double initialFee);
    }
}
