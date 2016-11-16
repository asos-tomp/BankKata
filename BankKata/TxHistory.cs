namespace BankKata
{
    public interface ITxHistory
    {
        void HandleDeposit(decimal amount);
    }

    public class TxHistory : ITxHistory
    {
        public void HandleDeposit(decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}