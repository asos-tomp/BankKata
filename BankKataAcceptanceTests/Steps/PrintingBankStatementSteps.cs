using System;
using System.Globalization;
using BankKata;
using Moq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Calendar = BankKata.Calendar;

namespace BankKataAcceptanceTests.Steps
{
    [Binding]
    public class PrintingBankStatementSteps
    {
        private Account _account;
        private Mock<BankConsole> _console;
        private TxHistory _txHistory;
        private Mock<Calendar> _calendar;
        private StatementPrinter _statementPrinter;
        private TxRecordPrinter _txRecordPrinter;

        [BeforeScenario]
        public void SetUp()
        {
            _calendar = new Mock<Calendar>();
            _console = new Mock<BankConsole>();
            _txHistory = new TxHistory(_calendar.Object);
            _txRecordPrinter = new TxRecordPrinter(_console.Object, new CultureInfo("en-GB"));
            _statementPrinter = new StatementPrinter(_console.Object, _txRecordPrinter, _calendar.Object);
            _account = new Account(_txHistory, _statementPrinter);
        }

        [Given(@"A deposit of (.*) on (.*)")]
        public void GivenADepositOfOn(decimal deposit, string date)
        {
            _calendar.Setup(c => c.GetDate()).Returns(DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture));
            _account.MakeDeposit(deposit);
        }
        
        [Given(@"A withdrawal of (.*) on (.*)")]
        public void GivenAWithdrawalOfOn(decimal withdrawal, string date)
        {
            _calendar.Setup(c => c.GetDate()).Returns(DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture));
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
            var lines = statementText.Split(new [] { "\r\n" }, StringSplitOptions.None);
            var callOrder = 0;
            _console.Setup(x => x.PrintLine(lines[0])).Callback(() => Assert.That(callOrder++, Is.EqualTo(0)));
            _console.Setup(x => x.PrintLine(lines[1])).Callback(() => Assert.That(callOrder++, Is.EqualTo(1)));
            _console.Setup(x => x.PrintLine(lines[2])).Callback(() => Assert.That(callOrder++, Is.EqualTo(2)));
            _console.Setup(x => x.PrintLine(lines[3])).Callback(() => Assert.That(callOrder++, Is.EqualTo(3)));

            _console.Verify(c => c.PrintLine(It.IsAny<string>()), Times.Exactly(lines.Length));
        }
    }
}
