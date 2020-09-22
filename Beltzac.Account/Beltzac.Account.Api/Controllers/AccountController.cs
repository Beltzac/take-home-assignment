using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beltzac.Account.Api.Models;
using Beltzac.Account.Api.Services;
using Beltzac.Account.Domain;
using Beltzac.Account.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Beltzac.Account.Api.Controllers
{    
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("reset")]
        public IActionResult ResetAccounts()
        {
            _accountService.ResetAccounts();
            return Content("OK");
        }

        [HttpPost("event")]
        public IActionResult ProcessEvent([FromBody] ProcessEventModel.Request request)
        {
            var result = _accountService.ProcessEvent(request);

            if (result.Errors.OfType<AccountNotFoundError>().Any())
                return NotFound(0);

            return Created(string.Empty, result.Data);
        }

        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery(Name = "account_id")] string id)
        {
            var result = _accountService.GetBalance(id);

            if (result.Errors.OfType<AccountNotFoundError>().Any())
                return NotFound(0);

            return Ok(result.Data);
        }
    }
}
