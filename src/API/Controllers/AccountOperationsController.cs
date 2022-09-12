using Application.AccountOperations.Commands;
using Application.AccountOperations.Dtos;
using Application.AccountOperations.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/v1/accountOperations")]
    [ApiController]
    public class AccountOperationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountOperationsController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}/status")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(StatusDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<StatusDto>> GetStatus(int id)
        {
            var statusDto = await _mediator.Send(new GetStatusQuery(id));

            if (statusDto is null)
            {
                return NotFound($"Account {id} not found.");
            }

            return Ok(statusDto);
        }

        [HttpGet("{id}/balance", Name = "GetBalance")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(BalanceDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BalanceDto>> GetBalance(int id)
        {
            var balanceDto = await _mediator.Send(new GetBalanceQuery(id));

            if (balanceDto is null)
            {
                return NotFound($"Account {id} not found.");
            }

            return Ok(balanceDto);
        }

        //TODO: Idea for refactoring. More AccountId to url and update route {id}/operation
        [HttpPost("operation")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BalanceDto>> AddOperation(AddOperationCommand command)
        {
            var account = await _mediator.Send(command);

            return CreatedAtRoute("GetBalance", new { id = command.AccountId }, value: new BalanceDto(account.Id, account.Currency, account.Balance, account.Status));
        }

        [HttpPut("upgradeAccount")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpgradeAccount(UpgradeAccountCommand command)
        {
            await _mediator.Send(command);

            return Ok();
        }
    }
}
