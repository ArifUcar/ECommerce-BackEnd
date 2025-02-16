using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace AU_Framework.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Eğer doğrulayıcı yoksa, doğrudan bir sonraki aşamaya geç
            if (!_validators.Any())
            {
                return await next();
            }

            // Doğrulama işlemi için bir context oluştur
            var context = new ValidationContext<TRequest>(request);

            // Tüm doğrulayıcıları çalıştır ve hataları topla
            var validationFailures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(error => error != null)
                .ToList();

            // Eğer doğrulama hatası varsa, hata fırlat
            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }

            return await next();
        }
    }
}
