using AU_Framework.Application.Repository;
using AU_Framework.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Application.Features.OrderFeatures.Queries.GetOrderCount;

public sealed class GetOrderCountQueryHandler : IRequestHandler<GetOrderCountQuery, int>
{
    private readonly IRepository<Order> _orderRepository;

    public GetOrderCountQueryHandler(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(GetOrderCountQuery request, CancellationToken cancellationToken)
    {
        var query = await _orderRepository.GetAllAsync(cancellationToken);
        return await query.Where(o => !o.IsDeleted).CountAsync(cancellationToken);
    }
} 