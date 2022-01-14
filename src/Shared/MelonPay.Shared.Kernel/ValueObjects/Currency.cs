using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Shared.Kernel.ValueObjects
{
    public record Currency
    {
        private static readonly HashSet<string> AllowedValues = new()
        {
            "RUB",
            "EUR",
            "USD"
        };

        public string Value { get; }

        public Currency(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 3)
            {
                throw new InvalidCurrencyException(value);
            }

            value = value.ToUpperInvariant();

            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedCurrencyException(value);
            }

            Value = value;
        }

        public static implicit operator Currency(string value) => new(value);

        public static implicit operator string(Currency value) => value.Value;

        public override string ToString() => Value;
    }
}
