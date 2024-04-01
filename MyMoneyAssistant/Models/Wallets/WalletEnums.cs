namespace MyMoneyAssistant.Models.Wallets
{
    /// <summary>
    /// Типы кошельков
    /// </summary>
    public enum WalletTypes
    {
        /// <summary>
        /// Физический кошелек с наличными
        /// </summary>
        WALLET,
        /// <summary>
        /// Дебетовая карта
        /// </summary>
        DebitCard,
        /// <summary>
        /// Кредитная карта
        /// </summary>
        CreditCard,
    }
    /// <summary>
    /// Виды валют
    /// </summary>
    public enum Currency
    {
        /// <summary>
        /// Биткойн
        /// </summary>
        BTC,
        /// <summary>
        /// Рубли
        /// </summary>
        Rub,
        /// <summary>
        /// Доллары
        /// </summary>
        Usd,
        /// <summary>
        /// Евро
        /// </summary>
        Eur
    }
}
