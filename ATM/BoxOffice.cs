namespace ATM
{
    public class BoxOffice
    {
        private readonly Dictionary<int, Stack<Banknote>> save;
        private int totalMoney;

        public int Total => totalMoney;
        public bool HasMoney => totalMoney > 0;

        public BoxOffice()
        {
            save = new();
            totalMoney = default;
        }

        public BoxOffice(BoxOffice? boxOffice) : this()
        {
            if (boxOffice is not null)
            {
                PutMoney(boxOffice.Withdraw(boxOffice.Total));
            }
        }

        public void PutMoney(IEnumerable<Banknote> banknotes)
        {
            var amounts = banknotes.Select(x => x.Amount).Distinct();

            foreach (var amount in amounts)
            {
                save.TryAdd(amount, new());
            }

            foreach (var banknote in banknotes)
            {
                save[banknote.Amount].Push(banknote);
                totalMoney += banknote.Amount;
            }
        }

        public IEnumerable<Banknote> Withdraw(int sum)
        {
            Stack<Banknote>? section;
            var banknotesToReturn = new List<Banknote>();

            foreach (var banknoteAmount in CentralBank.AvailableBanknotes)
            {
                section = save.GetValueOrDefault(banknoteAmount);
                if (section is null) continue;

                if (sum >= banknoteAmount && section.Count > 0)
                {
                    int banknoteCount = Math.Min(section.Count, sum / banknoteAmount);
                    
                    for (int i = 0; i < banknoteCount; i++)
                    {
                        var banknote = section.Pop();
                        banknotesToReturn.Add(banknote);

                        totalMoney -= banknote.Amount;
                        sum -= banknote.Amount;
                    }

                    if (sum == 0) break;
                }
            }

            if (sum != 0)
            {
                PutMoney(banknotesToReturn);
                banknotesToReturn.Clear();
            }

            return banknotesToReturn;
        }

        public void Empty()
        {
            save.Clear();
        }
    }
}
