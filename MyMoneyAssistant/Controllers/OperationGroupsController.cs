using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMoneyAssistant.Database;
using MyMoneyAssistant.Models;

namespace MyMoneyAssistant.Controllers
{
    /// <summary>
    /// Контроллер групп операций
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperationGroupsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public OperationGroupsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все группы операций
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperationGroup>>> GetOperationGroups()
        {
            return await _context.OperationGroups.ToListAsync();
        }

        /// <summary>
        /// Получить группу операций по ID
        /// </summary>
        /// <param name="id">Идентификатор группы операций</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OperationGroup>> GetOperationGroup(long id)
        {
            var operationGroup = await _context.OperationGroups.FindAsync(id);

            if (operationGroup == null)
            {
                return NotFound();
            }

            return operationGroup;
        }

        /// <summary>
        /// Запрос всех операций этой группы
        /// </summary>
        /// <param name="id">Идентификатор группы</param>
        /// <returns></returns>
        [HttpGet("{id}/operations")]
        public async Task<ActionResult<List<Operation>>> GetGroupsOperations(long id)
        {
            OperationGroup? group = await _context.OperationGroups
                .Include(group => group.Operations)
                    .ThenInclude(oper => oper.Wallet)
                .Where(group => group.Id == id)
                .FirstOrDefaultAsync();
            if(group is not null && group.Operations is not null)
                return group.Operations;
            else return NotFound();
        }

        /// <summary>
        /// Изменение имени группы операций
        /// </summary>
        /// <param name="id">Идентификатор группы</param>
        /// <param name="newName">Новое имя группы</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperationGroup(long id, string newName)
        {

            OperationGroup? group =  _context.OperationGroups.Find(id);
            if(group is not null)
            {
                group.Name = newName;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Создание группы операций
        /// </summary>
        /// <param name="name">Имя новой группы операций</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OperationGroup>> PostOperationGroup(string name)
        {
            _context.OperationGroups.Add(new OperationGroup() { Name = name});
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Удаление группы операций
        /// </summary>
        /// <param name="id">Идентификатор группы</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperationGroup(long id)
        {
            var operationGroup = await _context.OperationGroups.FindAsync(id);
            if (operationGroup == null)
            {
                return NotFound();
            }

            _context.OperationGroups.Remove(operationGroup);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationGroupExists(long id)
        {
            return _context.OperationGroups.Any(e => e.Id == id);
        }
    }
}
