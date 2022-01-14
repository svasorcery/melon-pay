namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidEmailException : MelonPayException
    {
        public string Email { get; }

        public InvalidEmailException(string email) : base($"Email: '{email}' is invalid.")
        {
            Email = email;
        }
    }
}
