using Asp.Versioning;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace OnSalesStore.ECommerce.Services.WebAPI.Controllers.v2
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountApplication _discountApplication;

        public DiscountsController(IDiscountApplication discountApplication)
        {
            _discountApplication = discountApplication;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscountDTO discountDTO)
        {
            if (discountDTO == null)
            {
                return BadRequest();
            }

            var response = await _discountApplication.Add(discountDTO);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DiscountDTO discountDTO)
        {
            var persistentCustomer = await _discountApplication.Get(id); ;
            if (persistentCustomer.Data == null)
            {
                return NotFound(persistentCustomer.Message);
            }
            if (discountDTO == null)
            {
                return BadRequest();
            }

            var response = await _discountApplication.Edit(discountDTO);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _discountApplication.Remove(id);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("Get/{id}")]
        [RequestTimeout("CustomPolicy")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _discountApplication.Get(id, HttpContext.RequestAborted);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _discountApplication.GetAll(HttpContext.RequestAborted);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _discountApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

    }
}
