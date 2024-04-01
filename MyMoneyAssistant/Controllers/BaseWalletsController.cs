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
    [Route("api/[controller]")]
    [ApiController]
    public class BaseWalletsController : ControllerBase
    {
        private readonly ApplicationDatabaseContext _context;

        public BaseWalletsController(ApplicationDatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получить все кошельки
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BaseWallet>>> GetAllWallets()
        {
            return await _context.AllWallets.ToListAsync();
        }

        /// <summary>
        /// Получить кошелек по ID
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns>Кошелек если он существует</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BaseWallet>> GetBaseWallet(long id)
        {
            var baseWallet = await _context.AllWallets.FindAsync(id);

            if (baseWallet == null)
            {
                return NotFound();
            }

            return baseWallet;
        }

        // PUT: api/BaseWallets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBaseWallet(long id, BaseWallet baseWallet)
        {
            if (id != baseWallet.Id)
            {
                return BadRequest();
            }

            _context.Entry(baseWallet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BaseWalletExists(id))
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

        // POST: api/BaseWallets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BaseWallet>> PostBaseWallet(BaseWallet baseWallet)
        {
            _context.AllWallets.Add(baseWallet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBaseWallet", new { id = baseWallet.Id }, baseWallet);
        }

        // DELETE: api/BaseWallets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBaseWallet(long id)
        {
            var baseWallet = await _context.AllWallets.FindAsync(id);
            if (baseWallet == null)
            {
                return NotFound();
            }

            _context.AllWallets.Remove(baseWallet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BaseWalletExists(long id)
        {
            return _context.AllWallets.Any(e => e.Id == id);
        }
    }
}
