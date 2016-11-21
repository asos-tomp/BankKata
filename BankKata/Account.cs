using System;

namespace BankKata
{
    public class Account
    {
        private readonly ITxHistory _txHistory;
        private readonly IStatementPrinter _statementPrinter;

        public Account(ITxHistory txHistory, IStatementPrinter statementPrinter)
        {
            _txHistory = txHistory;
            _statementPrinter = statementPrinter;
        }

        public void PrintStatement()
        {
            _statementPrinter.PrintStatement(_txHistory);
        }

        public void MakeDeposit(decimal deposit)
        {
            _txHistory.HandleDeposit(deposit);
        }

        public void MakeWithdrawal(decimal withdrawal)
        {
            _txHistory.HandleWithdrawal(withdrawal);
        }
    }
}