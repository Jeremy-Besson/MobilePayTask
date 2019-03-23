using System;

namespace JeremyBesson.MobilePayApp.Models
{
    public class TransactionFee
    {
        public DateTime Date { get; set; }
        public string MerchantName { get; set; }
        public double Amount { get; set; }
    }
}
