using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using NetPOC.Backend.Domain.Models;

namespace NetPOC.Backend.Infra.Repositories
{
    /// <inheritdoc cref="IUserRepository" />
    public class UserRepository : CrudRepository<UserModel>, IUserRepository
    {
        /// <summary>
        /// Repositório do <see cref="UserModel"/>
        /// </summary>
        /// <param name="logger">Logger do tipo <see cref="ILogger{UsuarioRepository}"/></param>
        /// <param name="context"><see cref="DataContext"/> da aplicação</param>
        public UserRepository(ILogger<UserRepository> logger, DataContext context) 
            : base(logger, context)
        {
            
        }
    }
}