using Beltzac.Account.Api.Models;
using Beltzac.Account.Kernel;

namespace Beltzac.Account.Api.Services
{
    public interface IAccountService
    {
        Result<decimal> GetBalance(string id);
        Result<ProcessEventModel.Response> ProcessEvent(ProcessEventModel.Request eventModel);
        Result ResetAccounts();
    }
}