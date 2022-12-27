namespace ATM
{
    public class CentralBank
    {
        public static readonly int[] AvailableBanknotes = { 100000, 50000, 20000, 10000, 5000, 2000, 1000 };

        public static ATM SpawnATM()
        {
            return new();
        }
    }
}
