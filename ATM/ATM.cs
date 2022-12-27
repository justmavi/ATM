using ATM.Exceptions;

namespace ATM
{
    public class ATM
    {
        public static readonly int DefaultMaxWithdrawAmountPerTransaction = 200000;

        private readonly BoxOffice boxOffice;
        public int Id { get; }
        public int MaxWithdrawAmountPerTransaction { get; }
        internal bool HasMoney => boxOffice.HasMoney;

        public ATM() : this(DefaultMaxWithdrawAmountPerTransaction, null) { }
        public ATM(int maxWithdrawAmountPerTransaction) : this(maxWithdrawAmountPerTransaction, null) { }
        public ATM(BoxOffice boxOffice) : this(DefaultMaxWithdrawAmountPerTransaction, boxOffice) { }

        public ATM(int maxWithdrawAmountPerTransaction, BoxOffice? boxOffice)
        {
            Id = new Random().Next(0, 1000);
            MaxWithdrawAmountPerTransaction = maxWithdrawAmountPerTransaction;
            this.boxOffice = new BoxOffice(boxOffice);
        }

        public IEnumerable<Banknote> Withdraw(int sum)
        {
            if (sum > MaxWithdrawAmountPerTransaction) throw new MaxAmountExceedException(MaxWithdrawAmountPerTransaction);
            else if (sum % 1000 != 0) throw new MustBeDividedBy1000Exception();

            return boxOffice.Withdraw(sum);
        }

        internal void PutMoney(IEnumerable<Banknote> banknotes)
        {
            boxOffice.PutMoney(banknotes);
        }
    }
}
