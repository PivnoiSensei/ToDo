using FluentValidation;
using MediatR;
using ToDoAPI.Infrastructure.Exceptions;

namespace ToDoAPI.Infrastructure.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest req, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!_validators.Any()) {
                return await next();
            }

            var context = new ValidationContext<TRequest>(req);

            var failures = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, ct)));

            var errors = failures
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();
            if (errors.Count() != 0) {
                throw new RequestValidationException(
                     errors.Select(x => x.ErrorMessage));
            }
            return await next();
        }
    }
}
