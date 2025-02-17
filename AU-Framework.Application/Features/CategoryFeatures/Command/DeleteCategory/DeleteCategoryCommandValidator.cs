using FluentValidation;

namespace AU_Framework.Application.Features.CategoryFeatures.Command.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kategori ID'si boş olamaz!");
            
        }

        
    }
} 