using System.Text;
using ATM.Exceptions;
using ATM.Exceptions.Base;

namespace ATM
{
    public class Program
    {
        public static void Main()
        {
            var atm = InitializeATM();

            while (atm.HasMoney)
            {
                Console.Write("Input sum: ");

                if (int.TryParse(Console.ReadLine(), out int inputSum))
                {
                    try
                    {
                        var returnedBanknotes = atm.Withdraw(inputSum);
                        var sb = new StringBuilder("Your request was successfully processed. Returned: ");

                        if (!returnedBanknotes.Any()) throw new NoNecessaryBanknotesException();

                        var groupedBanknotes = returnedBanknotes.GroupBy(x => x.Amount).Select((x) => new { Banknote = x.Key, Count = x.Count() });
                        foreach (var item in groupedBanknotes)
                        {
                            sb.Append($"{item.Banknote}: {item.Count}; ");
                        }

                        Console.WriteLine(sb.ToString());
                    }
                    catch (ATMException ex)
                    {
                        Console.WriteLine("We can't process your request, because " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Invalid error occured while processing withdraw request " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid sum. You must input a number");
                }
            }

            Console.WriteLine("No such money in ATM :(");
        }

        public static ATM InitializeATM()
        {
            var atm = CentralBank.SpawnATM();

            int amount;
            for (int i = 0; i < CentralBank.AvailableBanknotes.Length; i++)
            {
                amount = CentralBank.AvailableBanknotes[i];
                Console.Write($"Input count for banknote {amount}: ");

                if (int.TryParse(Console.ReadLine(), out int count))
                {
                    atm.PutMoney(Enumerable.Repeat(new Banknote(amount), count));
                }
                else
                {
                    Console.WriteLine("Invalid count. You must input a number");
                    --i;
                }
            }

            return atm;
        }
    }
}