using System.Globalization;
using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Shared.Kernel.ValueObjects
{
    public record Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (value is < 0 or > 1000000)
            {
                throw new InvalidAmountException(value);
            }

            Value = value;
        }

        public static Amount Zero => new(0m);

        public static implicit operator Amount(decimal value) => new(value);

        public static implicit operator decimal(Amount value) => value.Value;

        public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
    }
}
