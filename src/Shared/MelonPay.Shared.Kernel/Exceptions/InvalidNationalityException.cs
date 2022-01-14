namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidNationalityException : MelonPayException
    {
        public string Nationality { get; }

        public InvalidNationalityException(string nationality) : base($"Nationality: '{nationality}' is invalid.")
        {
            Nationality = nationality;
        }
    }
}
