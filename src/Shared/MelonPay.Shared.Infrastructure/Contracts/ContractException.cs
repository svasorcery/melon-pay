namespace MelonPay.Shared.Infrastructure.Contracts
{
    internal class ContractException : Exception
    {
        public ContractException(string message) : base(message)
        {
        }
    }
}
