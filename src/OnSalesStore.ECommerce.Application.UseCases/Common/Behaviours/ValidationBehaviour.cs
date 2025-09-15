using OnSalesStore.ECommerce.Application.UseCases.Common.Exceptions;
using OnSalesStore.ECommerce.Transversal.Common;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OnSalesStore.ECommerce.Application.UseCases.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.Where(r => r.Errors.Any()).SelectMany(r => r.Errors)
                    .Select(r => new BaseError() { PropertyMessage = r.PropertyName, ErrorMessage = r.ErrorMessage })
                    .ToList();
                if (failures.Count != 0)
                {
                    throw new ValidationExceptionCustom(failures);
                }
            }
            return await next(cancellationToken);
        }
    }
}
