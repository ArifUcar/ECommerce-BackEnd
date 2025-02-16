using FluentValidation;

namespace AU_Framework.Application.Features.ProductFeatures.Queries.GetProductById;

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Ürün ID'si boş olamaz!")
            .Must(BeValidGuid).WithMessage("Geçersiz ürün ID'si formatı!");
    }

    private bool BeValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
} 