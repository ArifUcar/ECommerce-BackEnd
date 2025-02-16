using FluentValidation;

namespace AU_Framework.Application.Features.AuthFeatures.Commands.Register;

public sealed class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad boş olamaz")
            .MaximumLength(100).WithMessage("Ad 100 karakterden uzun olamaz");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad boş olamaz")
            .MaximumLength(100).WithMessage("Soyad 100 karakterden uzun olamaz");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
            .MaximumLength(150).WithMessage("Email 150 karakterden uzun olamaz");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre boş olamaz")
            .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
            .MaximumLength(20).WithMessage("Şifre 20 karakterden uzun olamaz");

        RuleFor(x => x.Phone)
            .MaximumLength(20).WithMessage("Telefon numarası 20 karakterden uzun olamaz");

        RuleFor(x => x.Address)
            .MaximumLength(300).WithMessage("Adres 300 karakterden uzun olamaz");
    }
} 