using HotChocolate;
using Microsoft.Extensions.Logging;
using NetPOC.Backend.Domain.Interfaces.IServices;
using NetPOC.Backend.Domain.Models;

namespace NetPOC.Backend.API.Controllers.v1
{
    public class UserQuery : CrudQueryBase<UserModel>
    {
        private readonly ILogger<UserQuery> _logger;
        private readonly IUserService _userService;
        
        public UserQuery([Service] ILogger<UserQuery> logger, [Service] IUserService userService)
            : base(logger, userService)
        {
            _logger = logger;
            _userService = userService;
        }
    }
}