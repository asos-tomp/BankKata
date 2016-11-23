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

    public interface IPrinter<T> where T : IPrintable
    {
        void Print(T printable);
    }

    public class TxRecordPrinter : IPrinter<TxRecord>
    {
        private readonly BankConsole _console;

        public TxRecordPrinter(BankConsole console)
        {
            _console = console;
        }

        public void Print(TxRecord printable)
        {
            if (printable.TxType == TxType.Deposit)
            {
                _console.PrintLine(string.Format("{0:d} | {1:F2} | | {1:F2}", printable.Date, printable.Amount));
            }
            else
            {
                _console.PrintLine(string.Format("{0:d} | | {1:F2} | -{1:F2}", printable.Date, printable.Amount));
            }
        }
    }
}