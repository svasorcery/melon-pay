namespace MelonPay.Shared.Kernel.Exceptions
{
    public class InvalidFullNameException : MelonPayException
    {
        public string FullName { get; }

        public InvalidFullNameException(string fullName) : base($"Full name: '{fullName}' is invalid.")
        {
            FullName = fullName;
        }
    }
}
