using FluentValidation;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.DeleteProduct;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
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