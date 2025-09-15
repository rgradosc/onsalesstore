using OnSalesStore.ECommerce.Application.Interfaces.Presentation;
using OnSalesStore.ECommerce.Application.UseCases.Common.Constants;
using System.Security.Claims;

namespace OnSalesStore.ECommerce.Services.WebAPI.Services
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("userId") ?? GlobalConstant.DefaultUserId;
       
        public string UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue("userName") ?? GlobalConstant.DefaultUserName;
    }

}
