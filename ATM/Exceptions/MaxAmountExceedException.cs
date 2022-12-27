using ATM.Exceptions.Base;

namespace ATM.Exceptions
{
    internal class MaxAmountExceedException : ATMException
    {
        public int MaxAmount { get; }
        public MaxAmountExceedException(int maxAmount) : base("Max amount exceeded. Max amount: " + maxAmount)
        {
            MaxAmount = maxAmount;
        }
    }
}
