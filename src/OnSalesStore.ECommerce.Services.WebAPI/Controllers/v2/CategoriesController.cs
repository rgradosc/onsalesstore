using Asp.Versioning;
using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Swashbuckle.AspNetCore.Annotations;

namespace OnSalesStore.ECommerce.Services.WebAPI.Controllers.v2
{
    [Authorize]
    [EnableRateLimiting("fixedWindow")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [SwaggerTag("Get Categories of Products")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoriesController(ICategoryApplication categoryApplication)
        {
            _categoryApplication = categoryApplication;
        }

        [HttpGet("GetAll")]
        [SwaggerOperation(
            Summary = "Get Categories",
            Description = "This endpoint will return all categories",
            OperationId = "GetAll",
            Tags = new string[] { "GetAll" })]
        [SwaggerResponse(200, "List of Categories", typeof(Response<IEnumerable<CategoryDTO>>))]
        [SwaggerResponse(404, "Notfound Categories")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoryApplication.GetAll();
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

    }
}
