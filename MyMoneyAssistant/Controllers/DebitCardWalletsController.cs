using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMoneyAssistant.Database;
using MyMoneyAssistant.Models.Wallets;

namespace MyMoneyAssistant.Controllers
{
    /// <summary>
    /// Контроллер кошельков - дебитовых карт
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DebitCardWalletsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public DebitCardWalletsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список кошельков - дебитовых карт
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DebitCardWallet>>> GetDebitCardWallets()
        {
            return await _context.DebitCardWallets.ToListAsync();
        }

        /// <summary>
        /// Получить по ИД кошелек - дебитовую карту
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DebitCardWallet>> GetDebitCardWallet(long id)
        {
            var debitCardWallet = await _context.DebitCardWallets.FindAsync(id);

            if (debitCardWallet == null)
            {
                return NotFound();
            }

            return debitCardWallet;
        }

        /// <summary>
        /// Изменить кошелек - дебетовую карту
        /// </summary>
        /// <param name="id">Идентфикатор кошелька</param>
        /// <param name="debitCardWallet">Измененный кошелек</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDebitCardWallet(long id, DebitCardWallet debitCardWallet)
        {
            if (id != debitCardWallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(debitCardWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DebitCardWalletExists(id))
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
        /// Создать кошелек - дебетовую карту
        /// </summary>
        /// <param name="debitCardWallet">Новый кошелек</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DebitCardWallet>> PostDebitCardWallet(DebitCardWallet debitCardWallet)
        {
            _context.DebitCardWallets.Add(debitCardWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDebitCardWallet", new { id = debitCardWallet.Id }, debitCardWallet);
        }

        /// <summary>
        /// Удалить кошелек - дебетовую карту
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDebitCardWallet(long id)
        {
            var debitCardWallet = await _context.DebitCardWallets.FindAsync(id);
            if (debitCardWallet == null)
            {
                return NotFound();
            }

            _context.DebitCardWallets.Remove(debitCardWallet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DebitCardWalletExists(long id)
        {
            return _context.DebitCardWallets.Any(e => e.Id == id);
        }
    }
}
