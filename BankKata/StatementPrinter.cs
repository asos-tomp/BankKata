namespace BankKata
{
    public interface IStatementPrinter
    {
        void PrintStatement(ITxHistory txHistory);
    }

    public class StatementPrinter : IStatementPrinter
    {
        private readonly BankConsole _console;
        private readonly IPrinter<TxRecord> _txRecordPrinter;

        public StatementPrinter(BankConsole console, IPrinter<TxRecord> txRecordPrinter)
        {
            _console = console;
            _txRecordPrinter = txRecordPrinter;
        }

        public void PrintStatement(ITxHistory txHistory)
        {
            _console.PrintLine("date | credit | debit | balance");
            foreach (var txRecord in txHistory.Transactions())
            {
                txRecord.Accept<TxRecord>(_txRecordPrinter);
            }
        }
    }
}