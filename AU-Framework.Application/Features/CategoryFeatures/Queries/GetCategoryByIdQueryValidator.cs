using FluentValidation;

namespace AU_Framework.Application.Features.CategoryFeatures.Queries
{
    public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Kategori ID'si boş olamaz!");
        }
    }
} 