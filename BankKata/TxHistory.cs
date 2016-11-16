using System;

namespace BankKata
{
    public interface ITxHistory
    {
        void HandleDeposit(decimal amount);
    }

    public class TxHistory : ITxHistory
    {
        private readonly Calendar _calendarObject;
        private decimal _amount;
        private DateTime _date;

        public TxHistory(Calendar calendarObject)
        {
            _calendarObject = calendarObject;
        }

        protected bool Equals(TxHistory other)
        {
            return this._amount == other._amount && this._date == other._date;
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
            _amount = amount;
            _date = _calendarObject.GetDate();
        }
    }
}