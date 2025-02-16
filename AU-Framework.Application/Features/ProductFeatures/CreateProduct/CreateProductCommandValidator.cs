using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AU_Framework.Application.Features.ProductFeatures.CreateProduct
{
    public sealed class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş olamaz")
                .MaximumLength(100).WithMessage("Ürün adı 100 karakteri geçemez");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Açıklama 500 karakteri geçemez");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır");

            RuleFor(p => p.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı sıfır veya daha büyük olmalıdır");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("Kategori ID boş olamaz")
                .Must(ExistCategory).WithMessage("Geçerli bir kategori ID'si girin");
        }
        private bool ExistCategory(Guid categoryId)
        {
           
            return categoryId != Guid.Empty;  // Geçerli bir kategori ID'si olup olmadığını kontrol ediyoruz.
        }
    }
}
