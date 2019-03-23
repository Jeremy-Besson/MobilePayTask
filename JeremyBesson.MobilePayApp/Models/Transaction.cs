using System;

namespace JeremyBesson.MobilePayApp.Models
{
    public class Transaction
    {
        public bool IsEmpty { get; set; } = false;
        public DateTime Date { get; set; }
        public string MerchantName { get; set; }
        public double Amount { get; set; }
    }
}
