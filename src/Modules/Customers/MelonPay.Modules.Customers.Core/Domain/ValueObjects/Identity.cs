using MelonPay.Modules.Customers.Core.Exceptions;

namespace MelonPay.Modules.Customers.Core.Domain.ValueObjects
{
    internal record Identity
    {
        private static readonly HashSet<string> AllowedTypes = new()
        {
            "passport",
            "id_card",
            "drivers_license"
        };

        public string Type { get; }
        public string Series { get; }

        public Identity(string type, string series)
        {
            if (string.IsNullOrWhiteSpace(type) || series.Length > 20)
            {
                throw new InvalidIdentityException(type, series);
            }

            type = type.ToLowerInvariant();
            if (!AllowedTypes.Contains(type))
            {
                throw new InvalidIdentityException(type, series);
            }

            if (string.IsNullOrWhiteSpace(series) || series.Length > 20)
            {
                throw new InvalidIdentityException(type, series);
            }

            Type = type;
            Series = series;
        }

        public static implicit operator Identity?(string value) => value is null ? null : From(value);

        public static implicit operator string?(Identity value) => value is null ? null : $"{value.Type},{value.Series}";

        public override string ToString() => $"{Type},{Series}";

        public static Identity From(string value)
        {
            var (type, series) = Split(value);
            return new(type, series);
        }

        private static (string type, string series) Split(string value)
        {
            var values = value.Split(",");
            return (values[0], values[1]);
        }
    }
}
