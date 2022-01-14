namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidAggregateIdException : MelonPayException
    {
        public Guid Id { get; }

        public InvalidAggregateIdException(Guid id) : base($"Invalid aggregate ID: '{id}'.")
        {
            Id = id;
        }
    }
}
