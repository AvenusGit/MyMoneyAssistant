using System.ComponentModel.DataAnnotations;

namespace MyMoneyAssistant.Models.Wallets
{
    /// <summary>
    /// Базовый класс для кошельков
    /// </summary>
    public abstract class BaseWallet
    {
        /// <summary>
        /// Идентификатор кошелька
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Имя кошелька
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Тип кошелька
        /// </summary>
        [Required]
        public WalletTypes WalletType { get; set; }
        /// <summary>
        /// Валюта кошелька
        /// </summary>
        [Required]
        public Currency Currency { get; set; }
        /// <summary>
        /// Текущее значение кошелька
        /// </summary>
        [Required]
        public long CurrentValue { get; set; }
    }
}