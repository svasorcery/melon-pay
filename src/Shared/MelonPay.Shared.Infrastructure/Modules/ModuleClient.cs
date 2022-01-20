using MelonPay.Shared.Abstractions.Modules;
using MelonPay.Shared.Infrastructure.Modules.Registry;
using MelonPay.Shared.Infrastructure.Modules.Requests;

namespace MelonPay.Shared.Infrastructure.Modules
{
    internal class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }

        public async Task<TResult> SendAsync<TResult>(string path, object request, CancellationToken cancellationToken = default) where TResult : class
        {
            var registration = _moduleRegistry.GetRequestRegistration(path);
            if (registration is null)
            {
                throw new InvalidOperationException($"No action has been defined for path: '{path}'.");
            }

            var receiverRequest = TranslateType(request, registration.RequestType);
            var result = await registration.Action(receiverRequest, cancellationToken);

            return result is null ? null : TranslateType<TResult>(result);
        }

        public async Task PublishAsync(object message, CancellationToken cancellationToken = default)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry
                .GetBroadcastRegistrations(key)
                .Where(x => x.ReceiverType != message.GetType());

            var tasks = new List<Task>();
            foreach (var registration in registrations)
            {
                var receiverMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(registration.Action(receiverMessage, cancellationToken));
            }

            await Task.WhenAll(tasks);
        }

        private T TranslateType<T>(object value)
            => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));

        private object TranslateType(object value, Type type)
            => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
    }
}
