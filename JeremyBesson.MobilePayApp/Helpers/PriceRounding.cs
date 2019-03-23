using System;

namespace JeremyBesson.MobilePayApp.Helpers
{
    class PriceRounding
    {
        //TODO: Clarify and use...
        public double Round(double price)
        {
            return Math.Round(price, 1, MidpointRounding.AwayFromZero);
        }
    }
}
