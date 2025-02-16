using FluentValidation;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kategori ID'si boş olamaz!");

            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Kategori adı boş olamaz!")
                .MinimumLength(3)
                .WithMessage("Kategori adı en az 3 karakter olmalıdır!")
                .MaximumLength(50)
                .WithMessage("Kategori adı en fazla 50 karakter olabilir!");
        }
    }
} 