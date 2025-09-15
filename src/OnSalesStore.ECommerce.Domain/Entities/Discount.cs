using OnSalesStore.ECommerce.Domain.Common;
using OnSalesStore.ECommerce.Domain.Enums;

namespace OnSalesStore.ECommerce.Domain.Entities
{
    public class Discount : BaseAuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Percent { get; set; }

        public DiscountStatus Status { get; set; }
    }
}
