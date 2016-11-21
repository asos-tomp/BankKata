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

        public TxHistory(Calendar calendarObject)
        {
            _calendarObject = calendarObject;
            _transactions = new List<TxRecord>();
        }

        protected bool Equals(TxHistory other)
        {
            var thisFirstTx = this._transactions.FirstOrDefault();
            var otherFirstTx = other._transactions.FirstOrDefault();

            if (thisFirstTx == null && otherFirstTx != null)
                return false;
            if (thisFirstTx != null && otherFirstTx == null)
                return false;
            if (thisFirstTx == null && otherFirstTx == null)
                return true;

            return thisFirstTx.Amount == otherFirstTx.Amount &&
                   thisFirstTx.Date == otherFirstTx.Date &&
                   thisFirstTx.TxType == otherFirstTx.TxType;
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
            _transactions.Add(new TxRecord(TxType.Deposit, amount, _calendarObject.GetDate()));
        }

        public void HandleWithdrawal(decimal amount)
        {
            _transactions.Add(new TxRecord(TxType.Withdrawal, amount, _calendarObject.GetDate()));
        }

        public IEnumerable<TxRecord> Transactions()
        {
            return _transactions.AsReadOnly();
        }
    }
}