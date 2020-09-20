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
    public class ResetController : ControllerBase
    {
        private readonly IRepository<Domain.Account> _accounts;

        public ResetController(IRepository<Domain.Account> accountRepository)
        {
            _accounts = accountRepository;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _accounts.DeleteAll();
            return Ok();
        }
    }
}
