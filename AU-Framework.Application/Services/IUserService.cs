using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AU_Framework.Application.Services
{
    public interface IUserService
    {
        Task<int> GetUserCountAsync(CancellationToken cancellationToken);
        // ... diğer metodlar
    }
} 