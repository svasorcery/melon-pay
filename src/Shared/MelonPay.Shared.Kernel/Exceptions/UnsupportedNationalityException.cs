namespace MelonPay.Shared.Kernel.Exceptions
{
    public class UnsupportedNationalityException : MelonPayException
    {
        public string Nationality { get; }

        public UnsupportedNationalityException(string nationality) : base($"Nationality: '{nationality}' is unsupported.")
        {
            Nationality = nationality;
        }
    }
}
