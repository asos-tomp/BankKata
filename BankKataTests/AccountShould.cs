using BankKata;
using Moq;
using NUnit.Framework;

namespace BankKataTests
{
    [TestFixture]
    public class AccountShould
    {
        private Mock<ITxHistory> _txHistory;
        private Mock<IStatementPrinter> _statementPrinter;
        private Account _account;

        [SetUp]
        public void setup()
        {
            _txHistory = new Mock<ITxHistory>();
            _statementPrinter = new Mock<IStatementPrinter>();
            _account = new Account(_txHistory.Object, _statementPrinter.Object);
        }

        [Test]
        public void add_deposit_to_tx_history_given_deposit_of_amount_10()
        {
            _account.MakeDeposit(10);

            _txHistory.Verify(tx => tx.HandleDeposit(10.00M), Times.Once);
        }

        [Test]
        public void add_withdrawal_to_tx_history_given_withdrawal_of_amount_10()
        {
            _account.MakeWithdrawal(10);

            _txHistory.Verify(tx => tx.HandleWithdrawal(10.00M), Times.Once);
        }

        [Test]
        public void call_the_statement_printer_with_the_tx_history_when_printing()
        {
            _account.PrintStatement();

            _statementPrinter.Verify(statementPrinter => statementPrinter.PrintStatement(_txHistory.Object), Times.Once);
        }
    }
}
