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
    /// Контроллер физических кошельков
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public WalletsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Поолучить список физических кошельков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wallet>>> GetWallets()
        {
            return await _context.Wallets.ToListAsync();
        }

        /// <summary>
        /// Получить физический кошелек по ID
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(long id)
        {
            var wallet = await _context.Wallets.FindAsync(id);

            if (wallet == null)
            {
                return NotFound();
            }

            return wallet;
        }

        /// <summary>
        /// Изменить физический кошелек
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <param name="wallet">Измененный кошелек</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWallet(long id, Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(wallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WalletExists(id))
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
        /// Создать физический кошелек
        /// </summary>
        /// <param name="wallet">Новый физический кошелек</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Wallet>> PostWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWallet", new { id = wallet.Id }, wallet);
        }

        /// <summary>
        /// Удалить физический кошелек
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(long id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }

            _context.Wallets.Remove(wallet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WalletExists(long id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }
    }
}
