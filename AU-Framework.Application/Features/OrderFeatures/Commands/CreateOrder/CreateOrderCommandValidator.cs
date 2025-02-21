using FluentValidation;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        

        RuleFor(x => x.OrderStatusId)
            .NotEmpty().WithMessage("Sipariş durumu boş olamaz");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalıdır");

        RuleFor(x => x.OrderDetails)
            .NotEmpty().WithMessage("Sipariş detayları boş olamaz")
            .Must(x => x.Count > 0).WithMessage("En az bir ürün eklenmelidir");

        RuleForEach(x => x.OrderDetails).ChildRules(detail =>
        {
            detail.RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün ID boş olamaz");

            detail.RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır");

            detail.RuleFor(x => x.UnitPrice)
                .GreaterThan(0).WithMessage("Birim fiyat 0'dan büyük olmalıdır");
        });
    }
} 