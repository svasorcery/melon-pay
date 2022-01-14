namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidAmountException : MelonPayException
    {
        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) : base($"Amount: '{amount}' is invalid.")
        {
            Amount = amount;
        }
    }
}
