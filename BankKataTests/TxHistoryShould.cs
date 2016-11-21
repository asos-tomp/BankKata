using System;
using BankKata;
using Moq;
using NUnit.Framework;

namespace BankKataTests
{
    [TestFixture]
    public class TxHistoryShould
    {
        private Mock<Calendar> _calendar;
        private TxHistory _otherTxHistory;
        private TxHistory _txHistory;

        [SetUp]
        public void setup()
        {
            _calendar = new Mock<Calendar>();
            _otherTxHistory = new TxHistory(_calendar.Object);
            _txHistory = new TxHistory(_calendar.Object);
        }

        [Test]
        public void equal_new_tx_history_given_no_transactions()
        {
            Assert.That(_txHistory, Is.EqualTo(_otherTxHistory));
        }

        [Test]
        public void not_equal_new_tx_history_given_a_deposit_of_10()
        {
            _txHistory.HandleDeposit(10);

            Assert.That(_txHistory, Is.Not.EqualTo(_otherTxHistory));
        }

        [Test]
        public void not_equal_tx_history_of_different_deposit_transaction()
        {
            _txHistory.HandleDeposit(10);
            _otherTxHistory.HandleDeposit(20);

            Assert.That(_txHistory, Is.Not.EqualTo(_otherTxHistory));
        }

        [Test]
        public void not_equal_tx_history_of_same_deposit_amount_on_different_date()
        {
            _calendar.SetupSequence(c => c.GetDate())
                .Returns(new DateTime(2000, 1, 1))
                .Returns(new DateTime(2000, 1, 2));
            var otherTxHistory = new TxHistory(_calendar.Object);
            var txHistory = new TxHistory(_calendar.Object);

            txHistory.HandleDeposit(10);
            otherTxHistory.HandleDeposit(10);

            Assert.That(txHistory, Is.Not.EqualTo(otherTxHistory));
        }

        [Test]
        public void not_equal_new_tx_history_given_a_withdrawal_of_10()
        {
            _txHistory.HandleWithdrawal(10);

            Assert.That(_txHistory, Is.Not.EqualTo(_otherTxHistory));
        }

        [Test]
        public void not_equal_tx_history_of_different_withdrawal_transaction()
        {
            _txHistory.HandleWithdrawal(10);
            _otherTxHistory.HandleWithdrawal(20);

            Assert.That(_txHistory, Is.Not.EqualTo(_otherTxHistory));
        }

        [Test]
        public void not_equal_tx_history_of_same_withdrawal_amount_on_different_date()
        {
            _calendar.SetupSequence(c => c.GetDate())
                .Returns(new DateTime(2000, 1, 1))
                .Returns(new DateTime(2000, 1, 2));
            var otherTxHistory = new TxHistory(_calendar.Object);
            var txHistory = new TxHistory(_calendar.Object);

            txHistory.HandleWithdrawal(10);
            otherTxHistory.HandleWithdrawal(10);

            Assert.That(txHistory, Is.Not.EqualTo(otherTxHistory));
        }
    }
}
