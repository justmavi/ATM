using ATM.Exceptions.Base;

namespace ATM.Exceptions
{
    public class MustBeDividedBy1000Exception : ATMException
    {
        public MustBeDividedBy1000Exception() : base("Input sum must be divided by 1000") { }
    }
}
