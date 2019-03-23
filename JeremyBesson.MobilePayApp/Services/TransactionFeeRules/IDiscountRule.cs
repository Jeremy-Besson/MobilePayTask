using JeremyBesson.MobilePayApp.Models;

namespace JeremyBesson.MobilePayApp.Services.FeeRules
{
    public interface IDiscountRule
    {
        double ComputeDiscount(Transaction transaction, double initialFee);
    }
}
