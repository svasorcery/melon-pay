using MelonPay.Modules.Customers.Core.Exceptions;

namespace MelonPay.Modules.Customers.Core.Domain.ValueObjects
{
    internal record Address
    {
        public string Value { get; }

        public Address(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length is > 200 or < 3)
            {
                throw new InvalidAddressException(value);
            }

            Value = value.Trim();
        }

        public static implicit operator Address(string value) => new(value);

        public static implicit operator string(Address value) => value.Value;

        public override string ToString() => Value;
    }
}
