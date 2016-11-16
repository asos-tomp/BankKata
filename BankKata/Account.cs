using System;

namespace BankKata
{
    public class Account
    {
        private readonly ITxHistory _txHistory;

        public Account(ITxHistory txHistory)
        {
            _txHistory = txHistory;
        }

        public void PrintStatement()
        {
            throw new System.NotImplementedException();
        }

        public void MakeDeposit(decimal deposit)
        {
            _txHistory.HandleDeposit(deposit);
        }

        public void MakeWithdrawal(decimal withdrawal)
        {
            throw new NotImplementedException();
        }
    }
}