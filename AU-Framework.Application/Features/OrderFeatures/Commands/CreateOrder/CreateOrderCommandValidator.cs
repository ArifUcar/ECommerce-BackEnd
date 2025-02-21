using FluentValidation;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.CreateOrder;

public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderStatusId)
            .NotEmpty().WithMessage("Sipariş durumu seçilmelidir!");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalıdır!");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Müşteri adı boş olamaz!")
            .MaximumLength(100).WithMessage("Müşteri adı 100 karakterden uzun olamaz!");

        RuleFor(x => x.CustomerPhone)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz!")
            .MaximumLength(20).WithMessage("Telefon numarası 20 karakterden uzun olamaz!")
            .Matches(@"^[0-9\+\-\(\)]*$").WithMessage("Geçersiz telefon numarası formatı!");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Teslimat adresi boş olamaz!")
            .MaximumLength(500).WithMessage("Teslimat adresi 500 karakterden uzun olamaz!");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şehir boş olamaz!")
            .MaximumLength(50).WithMessage("Şehir 50 karakterden uzun olamaz!");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("İlçe boş olamaz!")
            .MaximumLength(50).WithMessage("İlçe 50 karakterden uzun olamaz!");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Posta kodu boş olamaz!")
            .MaximumLength(10).WithMessage("Posta kodu 10 karakterden uzun olamaz!")
            .Matches(@"^[0-9]*$").WithMessage("Posta kodu sadece rakamlardan oluşmalıdır!");

        RuleFor(x => x.OrderDetails)
            .NotEmpty().WithMessage("Sipariş detayları boş olamaz!")
            .Must(x => x.Count > 0).WithMessage("En az bir ürün eklenmelidir!");
    }
} 