namespace MelonPay.Shared.Kernel.Exceptions
{
    public abstract class MelonPayException : Exception
    {
        protected MelonPayException(string message) : base(message)
        {
        }
    }
}
