using OnSalesStore.ECommerce.Application.DTO;
using OnSalesStore.ECommerce.Application.Interfaces.Persistence;
using OnSalesStore.ECommerce.Application.Interfaces.UseCases;
using OnSalesStore.ECommerce.Transversal.Common;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Categories
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryApplication> _logger;
        private readonly IDistributedCache _cache;

        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<CategoryApplication> logger, IDistributedCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Response<IEnumerable<CategoryDTO>>> GetAll()
        {
            var response = new Response<IEnumerable<CategoryDTO>>();
            var cacheKey = "categoriesList";

            try
            {
                var redisCategories = await _cache.GetAsync(cacheKey);
                if (redisCategories != null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<CategoryDTO>>(redisCategories);
                }
                else
                {
                    var categories = await _unitOfWork.Categories.SelectAllAsync();
                    response.Data = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                    if (response.Data != null)
                    {
                        var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                        await _cache.SetAsync(cacheKey, serializedCategories, options);
                    }
                }

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta exitosa";
                    _logger.LogInformation(response.Message);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.LogError(response.Message);
            }

            return response;
        }
    }
}
