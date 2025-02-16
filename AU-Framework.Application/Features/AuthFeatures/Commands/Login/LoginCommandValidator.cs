using FluentValidation;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Login;

public sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(150).WithMessage("Email 150 karakterden uzun olamaz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
            .MaximumLength(20).WithMessage("Şifre 20 karakterden uzun olamaz");
    }
} 