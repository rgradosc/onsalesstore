using System;

namespace OnSalesStore.ECommerce.Application.DTO
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
