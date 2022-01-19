namespace MelonPay.Shared.Infrastructure.Modules.Registry
{
    internal interface IModuleRegistry
    {
        IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key);
        ModuleRequestRegistration GetRequestRegistration(string path);
        void AddBroadcastAction(ModuleBroadcastRegistration registration);
        void AddRequestAction(string path, ModuleRequestRegistration registration);
    }
}
