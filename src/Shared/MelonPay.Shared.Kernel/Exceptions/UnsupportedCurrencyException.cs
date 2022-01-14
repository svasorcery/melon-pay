namespace MelonPay.Shared.Kernel.Exceptions
{
    internal class UnsupportedCurrencyException : MelonPayException
    {
        public string Currency { get; }

        public UnsupportedCurrencyException(string currency) : base($"Currency: '{currency}' is unsupported.")
        {
            Currency = currency;
        }
    }
}
