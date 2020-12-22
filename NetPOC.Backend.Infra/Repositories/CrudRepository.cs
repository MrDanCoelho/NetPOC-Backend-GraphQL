using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetPOC.Backend.Domain.Interfaces;
using NetPOC.Backend.Domain.Interfaces.IRepositories;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NetPOC.Backend.Infra.Repositories
{
    /// <inheritdoc/>
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : class
    {
        private readonly ILogger<CrudRepository<T>> _logger;
        public DataContext _context;
        public DbSet<T> _table;
        
        /// <summary>
        /// Repositório abstrato de operações CRUD
        /// </summary>
        /// <param name="logger">Logger do tipo <see cref="ILogger{CrudRepository{T}}"/></param>
        /// <param name="context"><see cref="DataContext"/> da aplicação</param>
        public CrudRepository(ILogger<CrudRepository<T>> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
            _table = _context.Set<T>();
        }
        
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(GetAll)} ({typeof(T).Name})");
                
                var result = await _table.ToListAsync();
                
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
                
                var result = await _table.FindAsync(id);
                
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
                
                await _table.AddAsync(obj);
                
                _logger.LogInformation($"End - {nameof(Insert)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Insert)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public void Update(T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Update)} ({typeof(T).Name})");
                
                _table.Update(obj);
                
                _logger.LogInformation($"End - {nameof(Update)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public void Delete(T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Delete)} ({typeof(T).Name})");
                
                _table.Remove(obj);
                
                _logger.LogInformation($"End - {nameof(Delete)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Delete)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        public async Task Save()
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Save)} ({typeof(T).Name})");
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation($"End - {nameof(Save)} ({typeof(T).Name})");
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Save)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
    }
}