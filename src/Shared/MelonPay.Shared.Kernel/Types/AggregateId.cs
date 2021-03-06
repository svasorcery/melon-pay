using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Shared.Kernel.Types
{
    public record AggregateId<T>(T Value);


    public sealed record AggregateId
    {
        public Guid Value { get; }

        public AggregateId() : this(Guid.NewGuid())
        {
        }

        public AggregateId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new InvalidAggregateIdException(value);
            }

            Value = value;
        }


        public static implicit operator Guid(AggregateId id) => id.Value;

        public static implicit operator AggregateId(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}
