using OnSalesStore.ECommerce.Transversal.Common;
using System;
using System.Collections.Generic;

namespace OnSalesStore.ECommerce.Application.UseCases.Common.Exceptions
{
    public class ValidationExceptionCustom : Exception
    {
        public ValidationExceptionCustom() : base("One or more validation failures")
        {
            Errors = new List<BaseError>();
        }

        public ValidationExceptionCustom(IEnumerable<BaseError> errors) : base()
        {
            Errors = errors;
        }

        public IEnumerable<BaseError> Errors { get; }
    }
}
