using System;
using BankKata;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BankKataAcceptanceTests.Steps
{
    [Binding]
    public class PrintingBankStatementSteps
    {
        private Account _account;
        private Mock<BankConsole> _console;
        private TxHistory _txHistory;

        [BeforeScenario]
        public void SetUp()
        {
            _console = new Mock<BankConsole>();
            _txHistory = new TxHistory();
            _account = new Account(_txHistory);
        }

        [Given(@"A deposit of (.*) on (.*)")]
        public void GivenADepositOfOn(decimal deposit, string date)
        {
            _account.MakeDeposit(deposit);
        }
        
        [Given(@"A withdrawal of (.*) on (.*)")]
        public void GivenAWithdrawalOfOn(decimal withdrawal, string date)
        {
            _account.MakeWithdrawal(withdrawal);
        }
        
        [When(@"I print my bank statement")]
        public void WhenIPrintMyBankStatement()
        {    
            _account.PrintStatement();
        }
        
        [Then(@"My statement is")]
        public void ThenMyStatementIs(string statementText)
        {
            var lines = statementText.Split(Convert.ToChar(Environment.NewLine));
            var callOrder = 0;
            _console.Setup(x => x.PrintLine(lines[0])).Callback(() => Assert.That(callOrder++, Is.EqualTo(0)));
            _console.Setup(x => x.PrintLine(lines[1])).Callback(() => Assert.That(callOrder++, Is.EqualTo(1)));
            _console.Setup(x => x.PrintLine(lines[2])).Callback(() => Assert.That(callOrder++, Is.EqualTo(2)));
            _console.Setup(x => x.PrintLine(lines[3])).Callback(() => Assert.That(callOrder++, Is.EqualTo(3)));

            _console.Verify(c => c.PrintLine(It.IsAny<string>()), Times.Exactly(lines.Length));
        }
    }
}
