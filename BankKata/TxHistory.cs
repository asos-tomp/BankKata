using System;

namespace BankKata
{
    public interface ITxHistory
    {
        void HandleDeposit(decimal amount);
        void HandleWithdrawal(decimal @decimal);
    }

    public class TxHistory : ITxHistory
    {
        private readonly Calendar _calendarObject;
        private decimal _depositAmount;
        private DateTime _date;
        private decimal _withdrawalAmount;

        public TxHistory(Calendar calendarObject)
        {
            _calendarObject = calendarObject;
        }

        protected bool Equals(TxHistory other)
        {
            return this._depositAmount == other._depositAmount && 
                this._date == other._date &&
                this._withdrawalAmount == other._withdrawalAmount;
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
            _depositAmount = amount;
            _date = _calendarObject.GetDate();
        }

        public void HandleWithdrawal(decimal amount)
        {
            _withdrawalAmount = amount;
            _date = _calendarObject.GetDate();
        }
    }
}