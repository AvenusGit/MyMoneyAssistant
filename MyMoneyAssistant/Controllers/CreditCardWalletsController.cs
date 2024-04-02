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
    /// Контроллер кошельков - кредитных карточек
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardWalletsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public CreditCardWalletsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить список кошельков - кредитных карточек
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCardWallet>>> GetCreditCardWallets()
        {
            return await _context.CreditCardWallets.ToListAsync();
        }

        /// <summary>
        /// Получить кошелек - кредитную карточку по ID 
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCardWallet>> GetCreditCardWallet(long id)
        {
            var creditCardWallet = await _context.CreditCardWallets.FindAsync(id);

            if (creditCardWallet == null)
            {
                return NotFound();
            }

            return creditCardWallet;
        }

        /// <summary>
        /// Изменить кошелек - кредитную карту
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <param name="creditCardWallet">Измененный кошелек</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCardWallet(long id, CreditCardWallet creditCardWallet)
        {
            if (id != creditCardWallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(creditCardWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardWalletExists(id))
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
        /// Создать кошелек - кредитную карточку
        /// </summary>
        /// <param name="creditCardWallet">Новый кошелек</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreditCardWallet>> PostCreditCardWallet(CreditCardWallet creditCardWallet)
        {
            _context.CreditCardWallets.Add(creditCardWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditCardWallet", new { id = creditCardWallet.Id }, creditCardWallet);
        }

        /// <summary>
        /// Удалить кошелек - кредитную карточку
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCardWallet(long id)
        {
            var creditCardWallet = await _context.CreditCardWallets.FindAsync(id);
            if (creditCardWallet == null)
            {
                return NotFound();
            }

            _context.CreditCardWallets.Remove(creditCardWallet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditCardWalletExists(long id)
        {
            return _context.CreditCardWallets.Any(e => e.Id == id);
        }
    }
}
