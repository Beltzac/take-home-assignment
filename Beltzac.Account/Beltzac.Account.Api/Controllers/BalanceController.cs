using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beltzac.Account.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beltzac.Account.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IRepository<Domain.Account> _accounts;

        public BalanceController(IRepository<Domain.Account> accountRepository)
        {
            _accounts = accountRepository;
        }

        [HttpGet()]
        public IActionResult Get([FromQuery(Name = "account_id")] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var account = _accounts.Get(id);

            if (account == null)
                return NotFound(0);

            return Ok(account.Balance);
        }
    }
}
