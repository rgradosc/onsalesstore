using OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.DeleteCustomerCommand;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllCustomerQuery;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetAllWithPaginationQuery;
using OnSalesStore.ECommerce.Application.UseCases.Customers.Queries.GetCustomerQuery;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnSalesStore.ECommerce.Services.WebAPI.Controllers.v3
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("3.0")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Get/{customerId}")]
        public async Task<IActionResult> Get([FromRoute] string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new GetCustomerQuery() { CustomerId = customerId });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCustomerQuery());

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _mediator.Send(
                new GetAllWithPaginationQuery() { PageNumber = pageNumber, PageSize = pageSize });

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CreateCustomerCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpPut("Update/{customerId}")]
        public async Task<IActionResult> Update(string customerId, [FromBody] UpdateCustomerCommand command)
        {
            var persistentCustomer = await _mediator.Send(new GetCustomerQuery() { CustomerId = customerId });
            if (persistentCustomer.Data == null)
            {
                return NotFound(persistentCustomer.Message);
            }

            if (command == null)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        [HttpDelete("Delete/{customerId}")]
        public async Task<IActionResult> Delete([FromRoute] string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new DeleteCustomerCommand() { CustomerId = customerId });
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }
    }
}
