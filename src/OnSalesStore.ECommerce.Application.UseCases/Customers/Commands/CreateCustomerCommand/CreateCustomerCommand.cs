﻿using OnSalesStore.ECommerce.Transversal.Common;
using MediatR;

namespace OnSalesStore.ECommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public sealed record CreateCustomerCommand : IRequest<Response<bool>>
    {
        public string CustomerId { get; set; }

        public string CompanyName { get; set; }

        public string ContactName { get; set; }

        public string ContactTitle { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Region { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }
    }
}
