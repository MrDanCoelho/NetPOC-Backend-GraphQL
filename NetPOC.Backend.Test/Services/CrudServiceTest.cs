using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NetPOC.Backend.Application.Services;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using NetPOC.Backend.Domain.Models;
using NetPOC.Backend.Infra;
using NetPOC.Backend.Infra.Repositories;
using Xunit;

namespace NetPOC.Backend.Test.Services
{
    public class CrudServiceTest
    {
        private readonly Mock<ILogger<UserService>> _logger;
        private readonly Mock<IUserRepository> _crudRepository;
        
        public CrudServiceTest()
        {
            _logger = new Mock<ILogger<UserService>>();
            _crudRepository = new Mock<IUserRepository>();
        }
        
        [Fact]
        public async Task GetAll()
        {
            // Arrange
            var usuarios = new UserModel[]
            {
                new UserModel()
            };
            _crudRepository.Setup(x => x.GetAll())
                .Returns(Task.FromResult<IEnumerable<UserModel>>(usuarios));
            
            // Act
            var service = new UserService(_logger.Object, _crudRepository.Object);
            var result = await service.GetAll();
            
            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetById()
        {
            // Arrange
            _crudRepository.Setup(x => x.GetById(1))
                .Returns(Task.FromResult(new UserModel()));
            
            // Act
            var service = new UserService(_logger.Object, _crudRepository.Object);
            var result = await service.GetById(1);
            
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Insert()
        {
            // Arrange
            var usuario = new UserModel();
            
            // Act
            var service = new UserService(_logger.Object, _crudRepository.Object);
            var result = await Record.ExceptionAsync(async () => await service.Insert(usuario));
            
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update()
        {
            // Arrange
            var usuario = new UserModel();

            // Act
            var service = new UserService(_logger.Object, _crudRepository.Object);
            var result = await Record.ExceptionAsync(() => service.Update(usuario));
            
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete()
        {
            // Arrange
            var usuario = new UserModel();

            // Act
            var service = new UserService(_logger.Object, _crudRepository.Object);
            var result = await Record.ExceptionAsync(() => service.Delete(usuario));
            
            // Assert
            Assert.Null(result);
        }
    }
}