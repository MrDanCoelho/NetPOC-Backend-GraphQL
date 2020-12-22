using Microsoft.Extensions.Logging;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using NetPOC.Backend.Domain.Interfaces.IServices;
using NetPOC.Backend.Domain.Models;

namespace NetPOC.Backend.Application.Services
{
    /// <inheritdoc cref="IUserService" />
    public class UserService : CrudService<UserModel>, IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        
        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
            : base(logger, userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
    }
}