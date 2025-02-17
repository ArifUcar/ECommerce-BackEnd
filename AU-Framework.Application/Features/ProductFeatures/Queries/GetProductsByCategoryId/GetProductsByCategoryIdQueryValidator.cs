using FluentValidation;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductsByCategoryId;

public sealed class GetProductsByCategoryIdQueryValidator : AbstractValidator<GetProductsByCategoryIdQuery>
{
    public GetProductsByCategoryIdQueryValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Kategori ID'si boş olamaz!");
    }
} 