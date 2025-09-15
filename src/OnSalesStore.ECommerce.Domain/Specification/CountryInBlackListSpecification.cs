using OnSalesStore.ECommerce.Domain.Common;
using OnSalesStore.ECommerce.Domain.Entities;
using System.Collections.Generic;

namespace OnSalesStore.ECommerce.Domain.Specification
{
    public class CountryInBlackListSpecification : ISpecification<Customer>
    {
        readonly List<string> countriesInBlackList =
        [
            "Argentina",
            "Brasil",
            "Chile",
            "Colombia",
            "México",
            "España",
            "Portugal",
            "Estados Unidos",
            "Canada",
            "Alemania"
        ];

        public bool IsSatisfiedBy(Customer entity)
        {
            return !countriesInBlackList.Contains(entity.Country);
        }
    }
}
