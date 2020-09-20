﻿using System;
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
        private readonly IRepository<Domain.Account> _accounts;

        public EventController(IRepository<Domain.Account> accountRepository)
        {
            _accounts = accountRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] EventRequestModel eventModel)
        {
            return eventModel.Type switch
            {
                EventRequestModel.EventType.Deposit => Deposit(eventModel),
                EventRequestModel.EventType.Withdraw => Withdraw(eventModel),
                EventRequestModel.EventType.Transfer => Transfer(eventModel),
                _ => BadRequest(),
            };
        }

        private IActionResult Deposit(EventRequestModel eventModel)
        {
            Domain.Account destination = eventModel.Destination == null ? null : _accounts.Get(eventModel.Destination.Value);

            if (destination == null)
            {
                destination = new Domain.Account(eventModel.Destination.Value);
                _accounts.Add(destination);
            }

            destination.Deposit(eventModel.Amount);

            var response = new EventResponseModel
            {

                Destination = new AccountModel { Id = destination.Id, Balance = destination.Balance }
            };

            return Created(string.Empty, response);
        }

        private IActionResult Withdraw(EventRequestModel eventModel)
        {
            Domain.Account origin = eventModel.Origin == null ? null : _accounts.Get(eventModel.Origin.Value);

            if (origin == null)
                return NotFound(0);

            origin.Withdraw(eventModel.Amount);

            var response = new EventResponseModel
            {
                Origin = new AccountModel { Id = origin.Id, Balance = origin.Balance }
            };

            return Created(string.Empty, response);
        }

        private IActionResult Transfer(EventRequestModel eventModel)
        {
            Domain.Account origin = eventModel.Origin == null ? null : _accounts.Get(eventModel.Origin.Value);
            Domain.Account destination = eventModel.Destination == null ? null : _accounts.Get(eventModel.Destination.Value);

            if (origin == null)
                return NotFound(0);

            if (destination == null)
            {
                destination = new Domain.Account(eventModel.Destination.Value);
                _accounts.Add(destination);
            }

            origin.Transfer(destination, eventModel.Amount);

            var response = new EventResponseModel
            {
                Origin = new AccountModel { Id = origin.Id, Balance = origin.Balance },
                Destination = new AccountModel { Id = destination.Id, Balance = destination.Balance }
            };

            return Created(string.Empty, response);
        }
    }
}
