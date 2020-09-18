using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beltzac.Account.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beltzac.Account.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;

        public BalanceController(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        [HttpGet()]
        public IActionResult Get([FromQuery(Name = "account_id")] int? id)
        {
            if (id == null)
                return BadRequest();

            var account = _accountHandler.GetAccount(id.Value);

            if (account == null)
                return NotFound(0);

            return Ok(account.Balance);
        }
    }
}
