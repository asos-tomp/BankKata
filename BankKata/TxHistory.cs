using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankKata
{
    public interface ITxHistory
    {
        void HandleDeposit(decimal amount);
        void HandleWithdrawal(decimal @decimal);
        IEnumerable<TxRecord> Transactions();
    }

    public class TxHistory : ITxHistory
    {
        private readonly Calendar _calendarObject;
        private decimal _depositAmount;
        private DateTime _date;
        private decimal _withdrawalAmount;
        private List<TxRecord> _transactions;
        private decimal _balance;

        public TxHistory(Calendar calendarObject)
        {
            _calendarObject = calendarObject;
            _transactions = new List<TxRecord>();
            _balance = 0m;
        }

        protected bool Equals(TxHistory other)
        {
            var thisFirstTx = this._transactions.FirstOrDefault();
            var otherFirstTx = other._transactions.FirstOrDefault();

            return thisFirstTx == otherFirstTx;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TxHistory) obj);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void HandleDeposit(decimal amount)
        {
            _transactions.Add(new TxRecord(TxType.Deposit, amount, _calendarObject.GetDate(), _balance));
        }

        public void HandleWithdrawal(decimal amount)
        {
            _transactions.Add(new TxRecord(TxType.Withdrawal, amount, _calendarObject.GetDate(), _balance));
        }

        public IEnumerable<TxRecord> Transactions()
        {
            return _transactions.AsReadOnly();
        }
    }
}