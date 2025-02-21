using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AU_Framework.Application.Repository;
using AU_Framework.Domain.Entities;
using AU_Framework.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace AU_Framework.Persistance.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogService _logger;

        public UserService(
            IRepository<User> userRepository,
            ILogService logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<int> GetUserCountAsync(CancellationToken cancellationToken)
        {
            try
            {
                var query = await _userRepository.GetAllAsync(cancellationToken);
                return await query.Where(u => !u.IsDeleted).CountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _logger.LogError(ex, "Error getting user count");
                throw;
            }
        }

        // ... diğer metodlar
    }
} 