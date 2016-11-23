using System;
using System.Linq;
using BankKata;
using Moq;
using NUnit.Framework;

namespace BankKataTests
{
    [TestFixture]
    public class StatementPrinterShould
    {
        private TxHistory _txHistory;
        private Mock<BankConsole> _console;
        private StatementPrinter _statementPrinter;
        private Mock<Calendar> _calendar;
        private Mock<IPrinter<TxRecord>> _txRecordPrinter;

        [SetUp]
        public void Setup()
        {
            _calendar = new Mock<Calendar>();
            _txHistory = new TxHistory(_calendar.Object);
            _console = new Mock<BankConsole>();
            _txRecordPrinter = new Mock<IPrinter<TxRecord>>();
            _statementPrinter = new StatementPrinter(_console.Object, _txRecordPrinter.Object);
        }

        [Test]
        public void print_only_header_when_printing_empty_tx_history()
        {
            _statementPrinter.PrintStatement(_txHistory);

            _console.Verify(c => c.PrintLine("date | credit | debit | balance"), Times.Once);
            _txRecordPrinter.Verify(txrp => txrp.Print(It.IsAny<TxRecord>()), Times.Never);
        }

        [Test]
        public void print_header_and_tx_record_given_one_transaction()
        {
            _txHistory.HandleWithdrawal(5);

            _statementPrinter.PrintStatement(_txHistory);

            _console.Verify(c => c.PrintLine("date | credit | debit | balance"), Times.Once);
            _txRecordPrinter.Verify(txrp => txrp.Print(_txHistory.Transactions().First()), Times.Once);
            _txRecordPrinter.Verify(txrp => txrp.Print(It.IsAny<TxRecord>()), Times.Once);
        }

        [Test]
        public void print_header_and_tx_record_given_two_transactions()
        {
            _txHistory.HandleWithdrawal(5);
            _txHistory.HandleWithdrawal(6);

            _statementPrinter.PrintStatement(_txHistory);

            _console.Verify(c => c.PrintLine("date | credit | debit | balance"), Times.Once);
            _txRecordPrinter.Verify(txrp => txrp.Print(_txHistory.Transactions().First()), Times.Once());
            _txRecordPrinter.Verify(txrp => txrp.Print(_txHistory.Transactions().ElementAt(1)), Times.Once());
            _txRecordPrinter.Verify(txrp => txrp.Print(It.IsAny<TxRecord>()), Times.Exactly(2));
        }
    }
}
