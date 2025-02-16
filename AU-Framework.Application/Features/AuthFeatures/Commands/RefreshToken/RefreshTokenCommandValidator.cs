using FluentValidation;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.RefreshToken;

public sealed class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("RefreshToken boş olamaz");
    }
} 