namespace MyMoneyAssistant.Models.Wallets
{
    /// <summary>
    /// Кошелек - карта, абстрактный
    /// </summary>
    public class CardWallet : BaseWallet
    {
        public string BankName { get; set; }
        public string CardNumber { get; set; }
    }
}
