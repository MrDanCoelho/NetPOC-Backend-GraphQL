using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetPOC.Backend.Application.Services;
using NetPOC.Backend.Domain.Interfaces.IServices;

namespace NetPOC.Backend.API.Controllers.v1
{
    public abstract class CrudQueryBase<T> where T : class
    {
        private readonly ILogger<CrudQueryBase<T>> _logger;
        private readonly ICrudService<T> _crudService;

        protected CrudQueryBase(ILogger<CrudQueryBase<T>> logger, ICrudService<T> crudService)
        {
            _logger = logger;
            _crudService = crudService;
        }
        
        /// <summary>
        /// Busca todos os objetos
        /// </summary>
        /// <returns>Lista de objetos/></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(GetAll)} ({typeof(T).Name})");

                var result = await _crudService.GetAll();
                
                _logger.LogInformation($"End - {nameof(GetAll)} ({typeof(T).Name})");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetAll)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        /// <summary>
        /// Busca um objeto de acordo com o ID fornecido
        /// </summary>
        /// <param name="id">ID do objeto a ser buscado</param>
        /// <returns>Objeto achado</returns>
        [HttpGet("{id}")]
        public async Task<T> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(GetById)} ({typeof(T).Name})");
        
                var result = await _crudService.GetById(id);
                
                _logger.LogInformation($"End - {nameof(GetById)} ({typeof(T).Name})");
        
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(GetById)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        /// <summary>
        /// Insere um objeto
        /// </summary>
        /// <param name="obj">Objeto a ser inserido</param>
        /// <returns><see cref="ActionResult"/> da operação</returns>
        [HttpPost]
        public async Task<string> Insert([FromBody] T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Insert)} ({typeof(T).Name})");
                
                await _crudService.Insert(obj);
                
                _logger.LogInformation($"End - {nameof(Insert)} ({typeof(T).Name})");
        
                return "Objeto inserido com sucesso";
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Insert)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        /// <summary>
        /// Atualiza objeto
        /// </summary>
        /// <param name="obj">Objeto a ser atualizado</param>
        /// <returns><see cref="ActionResult"/> da operação</returns>
        [HttpPut]
        public async Task<string> Update([FromBody] T obj)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Update)} ({typeof(T).Name})");
        
                await _crudService.Update(obj);
        
                _logger.LogInformation($"End - {nameof(Update)} ({typeof(T).Name})");
        
                return "Objeto atualizado com sucesso";
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Update)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
        
        /// <summary>
        /// Apaga objeto de acordo com o ID fornecido
        /// </summary>
        /// <param name="id">ID do objeto a ser apagado</param>
        /// <returns><see cref="ActionResult"/> da operação</returns>
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(Delete)} ({typeof(T).Name})");
                
                await _crudService.Delete(id);
                
                _logger.LogInformation($"End - {nameof(Delete)} ({typeof(T).Name})");
        
                return "Objeto apagado com sucesso";
            }
            catch (Exception e)
            {
                _logger.LogError($"{nameof(Delete)} ({typeof(T).Name}): {e}");
                throw;
            }
        }
    }
}