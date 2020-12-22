using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using NetPOC.Backend.Domain.Interfaces.IServices;

namespace NetPOC.Backend.Application.Services
{
    /// <inheritdoc/>
    public abstract class CrudService<T> : ICrudService<T> where T : class
    {
        private readonly ILogger<CrudService<T>> _logger;
        private readonly ICrudRepository<T> _crudRepository;

        /// <summary>
        /// Serviço abstrato de operações CRUD
        /// </summary>
        /// <param name="logger">Logger do tipo <see cref="ILogger{CrudService{T}}"/></param>
        /// <param name="crudRepository">Repositório com as funções de CRUD que implemente <see cref="ICrudRepository{T}"/></param>
        public CrudService(ILogger<CrudService<T>> logger, ICrudRepository<T> crudRepository)
        {
            _logger = logger;
            _crudRepository = crudRepository;
        }
        
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(GetAll)} ({typeof(T).Name})");

                var result = await _crudRepository.GetAll();
                
                _logger.LogInformation($"End - {nameof(GetAll)} ({typeof(T).Name})");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetAll)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public async Task<T> GetById(object id)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(GetById)} ({typeof(T).Name})");
                
                var result = await _crudRepository.GetById(id);
                
                _logger.LogInformation($"End - {nameof(GetById)} ({typeof(T).Name})");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetById)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public async Task Insert(T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Insert)} ({typeof(T).Name})");
                
                await _crudRepository.Insert(obj);
                await _crudRepository.Save();
                
                _logger.LogInformation($"End - {nameof(Insert)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Insert)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public async Task Update(T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Update)} ({typeof(T).Name})");
                
                _crudRepository.Update(obj);
                await _crudRepository.Save();
                
                _logger.LogInformation($"End - {nameof(Update)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public async Task Delete(object id)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Delete)} ({typeof(T).Name})");

                var obj = await _crudRepository.GetById(id);
                _crudRepository.Delete(obj);
                await _crudRepository.Save();
                
                _logger.LogInformation($"End - {nameof(Delete)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Delete)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
    }
}