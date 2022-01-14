namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidCurrencyException : MelonPayException
    {
        public string Currency { get; }

        public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
        {
            Currency = currency;
        }
    }
}
