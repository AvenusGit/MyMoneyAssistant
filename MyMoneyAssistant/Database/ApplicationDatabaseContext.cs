using Microsoft.EntityFrameworkCore;
using MyMoneyAssistant.Models.Wallets;

namespace MyMoneyAssistant.Database
{
    /// <summary>
    /// Класс контекста БД
    /// </summary>
    public class ApplicationDatabaseContext : DbContext
    {
        /// <summary>
        /// Контекст кошельков
        /// </summary>
        public DbSet<BaseWallet> AllWallets { get; set; } = null!;
        /// <summary>
        /// Контекст всех карт - кошельков
        /// </summary>
        public DbSet<CardWallet> CardWallets { get; set; } = null!;
        /// <summary>
        /// Контекст всех физических кошельков
        /// </summary>
        public DbSet<Wallet> Wallets { get; set; } = null!;
        /// <summary>
        /// Контекст кошельков - дебетовых карт
        /// </summary>
        public DbSet<DebitCardWallet> DebitCardWallets { get; set; } = null!;
        /// <summary>
        /// Контекст кошельков - кредитных карт
        /// </summary>
        public DbSet<CreditCardWallet> CreditCardWallets { get; set; } = null!;

        public ApplicationDatabaseContext() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=postgres");
        }
    }

    public interface IApplicationDatabaseContext
    {

    }
}
