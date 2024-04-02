using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMoneyAssistant.Database;
using MyMoneyAssistant.Models;
using MyMoneyAssistant.Models.Wallets;

namespace MyMoneyAssistant.Controllers
{
    /// <summary>
    /// Контроллер операций
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public OperationsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все операции
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperations()
        {
            return await _context.Operations
                .Include(oper => oper.Group) 
                .Include(oper => oper.Wallet)
                .ToListAsync();
        }

        /// <summary>
        /// Получить операцию по ID
        /// </summary>
        /// <param name="id">Идентификатор операции</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Operation>> GetOperation(long id)
        {
            var operation = await _context.Operations.FindAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return operation;
        }

        /// <summary>
        /// Изменить операцию
        /// </summary>
        /// <param name="id">Идентификатор операции</param>
        /// <param name="operation">Измененная операция</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperation(long id, Operation operation)
        {
            if (id != operation.Id)
            {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Создание операции
        /// </summary>
        /// <param name="value">Значение операции, если отрицательное будет вычитание из кошелька</param>
        /// <param name="description">Описание операции</param>
        /// <param name="walletId">Идентификатор кошелька</param>
        /// <param name="groupId">Идентифкатор группы</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Operation>> PostOperation(double value, string description, long walletId, long groupId)
        {
            Operation operation = new Operation()
            {
                Value = value,
                Description = description,
                OperationGroupId = groupId,
                BaseWalletId = walletId
            };

            BaseWallet? wallet = await _context.AllWallets.FindAsync(walletId);
            if(wallet is null)
                return BadRequest("Wallet Id not found");

            // Проверка на вычитание больше кошелька при наличных
            if(value < 0)
            {
                if (wallet.WalletType == WalletTypes.WALLET && Math.Abs(wallet.CurrentValue) < Math.Abs(value))
                    return BadRequest("Попытка вычесть из физического кошелька больше чем там есть");
            }
            
            wallet.CurrentValue += value;
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperation", new { id = operation.Id }, operation);
        }

        /// <summary>
        /// Удаление операции
        /// </summary>
        /// <param name="id">Идентификатор операции</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation(long id)
        {
            var operation = await _context.Operations.Include(oper => oper.Wallet).Where(oper => oper.Id == id).FirstOrDefaultAsync();
            if (operation is null)
            {
                return NotFound();
            }

            // Отмена операции на кошельке
            if(operation.Wallet is not null)
            {
                BaseWallet wallet = await _context.Wallets.FindAsync(operation.Wallet.Id);
                wallet.CurrentValue -= operation.Value;
            }           

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationExists(long id)
        {
            return _context.Operations.Any(e => e.Id == id);
        }
    }
}
