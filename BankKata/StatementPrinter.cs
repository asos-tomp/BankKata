using System.Linq;

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
        private readonly Calendar _calendarObject;

        public StatementPrinter(BankConsole console, IPrinter<TxRecord> txRecordPrinter, Calendar calendarObject)
        {
            _console = console;
            _txRecordPrinter = txRecordPrinter;
            _calendarObject = calendarObject;
        }

        public void PrintStatement(ITxHistory txHistory)
        {
            _console.PrintLine("date | credit | debit | balance");
            foreach (var txRecord in txHistory.Transactions().OrderByDescending(tx => tx.Date))
            {
                txRecord.Accept<TxRecord>(_txRecordPrinter);
            }
        }
    }
}