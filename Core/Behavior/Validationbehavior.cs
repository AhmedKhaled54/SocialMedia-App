using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Behavior
{
    public class Validationbehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validator;

        public Validationbehavior(IEnumerable<IValidator<TRequest>> validator )
        {
            this.validator = validator;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var result = await Task.WhenAll(validator.Select(c => c.ValidateAsync(context)));
                var failures = result.SelectMany(c => c.Errors).Where(c => c != null).ToList();
                if (failures.Count != 0)
                {
                    var message = failures.Select(c => c.PropertyName + " " + c.ErrorMessage).FirstOrDefault();
                    throw new ValidationException(message);
                }

         
            }
            return await next();
        }
    }
}
