using FluentValidation;

namespace AU_Framework.Application.Features.CategoryFeatures.CreateCategory;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(p => p.CategoryName)
            .NotEmpty().WithMessage("Kategori adı boş olamaz");
        RuleFor(p => p.CategoryName)
           .MinimumLength(3).WithMessage("Kategori adı en az 3 karakterli olmalıdır");
        RuleFor(p => p.CategoryName)
    .Matches("^[a-zA-Z0-9]*$").WithMessage("Kategori adı yalnızca harf ve rakam içerebilir");

    }
}
