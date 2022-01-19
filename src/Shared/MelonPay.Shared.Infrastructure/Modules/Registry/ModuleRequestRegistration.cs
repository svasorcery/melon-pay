namespace MelonPay.Shared.Infrastructure.Modules.Registry
{
    internal record ModuleRequestRegistration(
        Type RequestType,
        Type ResponseType,
        Func<object, CancellationToken, Task<object>> Action
        );
}
