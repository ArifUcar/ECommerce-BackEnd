using FluentValidation;

namespace AU_Framework.Application.Features.ProductFeatures.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty().WithMessage("Ürün adı boş olamaz!")
            .MinimumLength(3).WithMessage("Ürün adı en az 3 karakter olmalıdır!")
            .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir!");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir!");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır!");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı 0 veya daha büyük olmalıdır!");

   
    }

    private bool BeValidGuid(string id)
    {
        return Guid.TryParse(id, out _);
    }
} 