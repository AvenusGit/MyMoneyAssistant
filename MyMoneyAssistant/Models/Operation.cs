using Microsoft.EntityFrameworkCore;
using MyMoneyAssistant.Models.Wallets;
using System.ComponentModel.DataAnnotations;

namespace MyMoneyAssistant.Models
{
    /// <summary>
    /// Операция над финансами
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Идентификатор операции
        /// </summary>
        [Key]
        public long Id { get; set; }
        /// <summary>
        /// Значение операции, если вычитание, то отрицательное
        /// </summary>
        [Required]
        public double Value { get; set; }
        /// <summary>
        /// Описание или комментарий к операции
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Идентификатор группы операций
        /// </summary>
        public long OperationGroupId { get; set; }
        /// <summary>
        /// Привязка к группе операций
        /// </summary>
        [Required]
        public OperationGroup? Group { get; set; }
        /// <summary>
        /// Идентификатор кошелька
        /// </summary>
        public long BaseWalletId { get; set; }
        /// <summary>
        /// Привязка к кошельку
        /// </summary>
        [Required]
        public BaseWallet? Wallet { get; set; }
        /// <summary>
        /// Дата/время совершения операции, если null, то будет установлено текущее время сервера
        /// </summary>
        public DateTimeOffset? DateTime { get; set; }
    }
}
