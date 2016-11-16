using BankKata;
using Moq;
using NUnit.Framework;

namespace BankKataTests
{
    [TestFixture]
    public class AccountShould
    {
        [Test]
        public void add_deposit_to_tx_history_given_deposit_of_amount_10()
        {
            var txHistory = new Mock<ITxHistory>();
            var account = new Account(txHistory.Object);

            account.MakeDeposit(10);

            txHistory.Verify(tx => tx.HandleDeposit(10.00M), Times.Once);
        }

        [Test]
        public void add_withdrawal_to_tx_history_given_withdrawal_of_amount_10()
        {
            var txHistory = new Mock<ITxHistory>();
            var account = new Account(txHistory.Object);

            account.MakeWithdrawal(10);

            txHistory.Verify(tx => tx.HandleWithdrawal(10.00M), Times.Once);
        }
    }
}
