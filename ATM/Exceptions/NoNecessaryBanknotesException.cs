using ATM.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Exceptions
{
    public class NoNecessaryBanknotesException : ATMException
    {
        public NoNecessaryBanknotesException() : base("There are no necessary banknotes")
        {

        }
    }
}
