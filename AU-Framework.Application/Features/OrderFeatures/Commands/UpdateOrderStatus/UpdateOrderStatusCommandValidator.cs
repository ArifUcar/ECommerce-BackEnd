using FluentValidation;

namespace AU_Framework.Application.Features.OrderFeatures.Commands.UpdateOrderStatus;

public sealed class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("Sipariş ID'si boş olamaz!");

        RuleFor(x => x.OrderStatusId)
            .NotEmpty().WithMessage("Sipariş durumu seçilmelidir!");
    }
} 