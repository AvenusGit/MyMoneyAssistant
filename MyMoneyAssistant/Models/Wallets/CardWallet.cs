namespace MyMoneyAssistant.Models.Wallets
{
    /// <summary>
    /// Кошелек - карта, абстрактный
    /// </summary>
    public abstract class CardWallet : BaseWallet
    {
        public string BankName { get; set; }
    }
}
