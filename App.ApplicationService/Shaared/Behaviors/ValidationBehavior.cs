using App.ApplicationService.Shaared.Attributes;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.Shaared.Behaviors
{
    [ServiceMark]
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TRequest>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TRequest> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TRequest> next)
        {
            var context = new ValidationContext(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}