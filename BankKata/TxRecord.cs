using System;

namespace BankKata
{
    public enum TxType
    {
        Withdrawal,
        Deposit
    }

    public interface IPrintable
    {
        void Accept<T>(IPrinter<TxRecord> printer) where T : IPrintable;
    }

    public class TxRecord : IPrintable
    {
        protected bool Equals(TxRecord other)
        {
            if ( other == null)
                return false;

            return TxType == other.TxType && 
                Amount == other.Amount && 
                Date.Equals(other.Date);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TxRecord) obj);
        }

        public void Accept<T>(IPrinter<TxRecord> printer) where T : IPrintable
        {
            printer.Print(this);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) TxType;
                hashCode = (hashCode*397) ^ Amount.GetHashCode();
                hashCode = (hashCode*397) ^ Date.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(TxRecord left, TxRecord right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TxRecord left, TxRecord right)
        {
            return !Equals(left, right);
        }

        public TxType TxType { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }

        public TxRecord(TxType txType, decimal amount, DateTime date)
        {
            TxType = txType;
            Amount = amount;
            Date = date;
        }
    }
}