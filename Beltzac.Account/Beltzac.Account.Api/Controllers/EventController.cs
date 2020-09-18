using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beltzac.Account.Api.Models;
using Beltzac.Account.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Beltzac.Account.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;

        public EventController(IAccountHandler accountHandler)
        {
            _accountHandler = accountHandler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EventRequestModel eventModel)
        {
            Domain.Account origin = null;
            Domain.Account destination = null;

            switch (eventModel.Type)
            {
                case EventRequestModel.EventType.Deposit:              
                    destination = _accountHandler.GetAccount(eventModel.Destination);

                    if (destination == null)
                        return NotFound(0);

                    _accountHandler.Deposit(eventModel.Destination, eventModel.Amount);
                    break;

                case EventRequestModel.EventType.Withdraw:
                    origin = _accountHandler.GetAccount(eventModel.Origin);

                    if (origin == null)
                        return NotFound(0);

                    _accountHandler.Withdraw(eventModel.Origin, eventModel.Amount);
                    break;

                case EventRequestModel.EventType.Transfer:
                    origin = _accountHandler.GetAccount(eventModel.Origin);
                    destination = _accountHandler.GetAccount(eventModel.Destination);

                    if (origin == null || destination == null)
                        return NotFound(0);

                    _accountHandler.Transfer(eventModel.Origin, eventModel.Destination, eventModel.Amount);
                    break;

                default:
                    return BadRequest();                    
            }            

            var response = new EventResponseModel();

            if(origin != null)
                response.Origin = new AccountModel { Id = origin.Id, Balance = origin.Balance };

            if(destination != null)
                response.Destination = new AccountModel { Id = destination.Id, Balance = destination.Balance };

            return Created(string.Empty, response);
        }
    }
}
