using MelonPay.Shared.Kernel.Exceptions;

namespace MelonPay.Shared.Kernel.ValueObjects
{
    public record Nationality
    {
        private static readonly HashSet<string> AllowedValues = new()
        {
            "RU",
            "UK",
            "US"
        };

        public string Value { get; }

        public Nationality(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 2)
            {
                throw new InvalidNationalityException(value);
            }

            value = value.ToUpperInvariant();

            if (!AllowedValues.Contains(value))
            {
                throw new UnsupportedNationalityException(value);
            }

            Value = value;
        }

        public static implicit operator Nationality?(string value) => value is null ? null : new(value);

        public static implicit operator string?(Nationality value) => value?.Value;

        public override string ToString() => Value;
    }
}
