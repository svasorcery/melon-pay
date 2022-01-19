namespace MelonPay.Shared.Infrastructure.Modules.Registry
{
    internal interface IModuleRegistry
    {
        ModuleRequestRegistration GetRequestRegistration(string path);
        void AddRequestAction(string path, ModuleRequestRegistration registration);
    }
}
