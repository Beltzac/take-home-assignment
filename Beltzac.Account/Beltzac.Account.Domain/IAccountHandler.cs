namespace Beltzac.Account.Domain
{
    public interface IAccountHandler
    {
        Account CreateAccount(int id);
        void DeleteAccount(int id);
        void Deposit(int idDestination, decimal amount);
        Account GetAccount(int id);
        void Reset();
        void Transfer(int idOrigin, int idDestination, decimal amount);
        void Withdraw(int idOrigin, decimal amount);
    }
}