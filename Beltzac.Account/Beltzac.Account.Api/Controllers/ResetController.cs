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
    public class ResetController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;

        public ResetController(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _accountHandler.Reset();
            return Ok();
        }
    }
}
