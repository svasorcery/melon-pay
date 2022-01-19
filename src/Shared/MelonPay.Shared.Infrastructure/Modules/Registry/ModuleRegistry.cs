namespace MelonPay.Shared.Infrastructure.Modules.Registry
{
    internal class ModuleRegistry : IModuleRegistry
    {
        private readonly Dictionary<string, ModuleRequestRegistration> _requestRegistration = new();

        public ModuleRequestRegistration GetRequestRegistration(string path)
            => _requestRegistration.TryGetValue(path, out var registration) ? registration : null;

        public void AddRequestAction(string path, ModuleRequestRegistration registration)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new InvalidOperationException("Request path cannot be null.");
            }

            if (registration.GetType().Namespace is null)
            {
                throw new InvalidOperationException("Namespace cannot be null.");
            }

            _requestRegistration.Add(path, registration);
        }
    }
}
