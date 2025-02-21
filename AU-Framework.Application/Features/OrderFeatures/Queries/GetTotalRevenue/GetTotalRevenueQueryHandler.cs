using AU_Framework.Application.Repository;
using AU_Framework.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetTotalRevenue;

public sealed class GetTotalRevenueQueryHandler : IRequestHandler<GetTotalRevenueQuery, decimal>
{
    private readonly IRepository<Order> _orderRepository;

    public GetTotalRevenueQueryHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<decimal> Handle(GetTotalRevenueQuery request, CancellationToken cancellationToken)
    {
        var query = await _orderRepository.GetAllWithIncludeAsync(
            include => include
                .Include(o => o.OrderStatus)
                .Where(o => !o.IsDeleted && o.OrderStatus.Name != "İptal Edildi"),
            cancellationToken);

        return await query.SumAsync(o => o.TotalAmount, cancellationToken);
    }
} 