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
    /// Контроллер абстрактных кошельков
    /// Для действий с конкретными видами кошельков используй отедльные контроллеры по видам
    /// </summary>
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
        [HttpGet("/all")]
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

        /// <summary>
        /// Получить список кошельков - карточек
        /// </summary>
        /// <returns></returns>
        [HttpGet("onlyCards")]
        public async Task<ActionResult<IEnumerable<CardWallet>>> GetOnlyCardWallets()
        {
            return await _context.CardWallets.ToListAsync();
        }

        /// <summary>
        /// Удалить кошелек
        /// </summary>
        /// <param name="id">Идентификатор кошелька</param>
        /// <returns></returns>
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
    }
}
